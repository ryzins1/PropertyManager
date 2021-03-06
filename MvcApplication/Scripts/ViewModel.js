﻿var propertyManager = propertyManager || {};

propertyManager.ViewModel = function (apiUrl, newObjectDefaults) {

    var that = {};

    that.items = ko.observableArray([]);

    that.item = {};

    that.get = function(callback) {
	    $.getJSON(apiUrl, function (items) {
	        if (items instanceof Array) {
	            ko.mapping.fromJS(items, {}, that.items);
	            ko.utils.arrayForEach(that.items(), function(item) {
	                ko.watch(item, function() { that.update(item); });
	            });
	        } else {
	            ko.mapping.fromJS(items, {}, that.item);
	            ko.watch(that.item, function() { that.update(that.item); });
	        }
            if (callback) {
                callback();
            }
		});
	};

    that.search = function (query) {
        that.items.removeAll();
        $.getJSON(apiUrl + "?query=" + query, function (items) {
	        ko.mapping.fromJS(items, {}, that.items);
            ko.utils.arrayForEach(that.items(), function(item) {
                ko.watch(item, function() { that.update(item); });
            });
		});
	};

	that.add = function() {
		$.ajax({
			url: apiUrl,
			type: "POST",
			data: JSON.stringify(newObjectDefaults),
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: function (result) {
			    var item = ko.mapping.fromJS(result);
				ko.watch(item, function() { that.update(item); });
			    that.items.push(item);
			},
			error: function (jqXhr) {
				alert(jqXhr.responseText);
				// TODO
			}
		});
	};

	that.update = function (item) {
		$.ajax({
			url: item.url(),
			type: "PUT",
			data: ko.toJSON(item),
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: function () {
			},
			error: function (jqXhr) {
				alert(jqXhr.responseText);
				// TODO
			}
		});
	};

	that.remove = function(item) {
		$.ajax({
			url: item.url(),
			type: "DELETE",
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: function () {
				that.items.remove(item);
			},
			error: function (jqXhr) {
				alert(jqXhr.responseText);
				// TODO
			}
		});
	};

	return that;
};