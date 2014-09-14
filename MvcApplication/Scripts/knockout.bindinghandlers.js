﻿ko.bindingHandlers.datepicker = {
    init: function(element, valueAccessor, allBindingsAccessor) {
        //initialize datepicker with some optional options
        var options = allBindingsAccessor().datepickerOptions || {},
            $el = $(element);

        $el.datepicker(options);

        //handle the field changing
        ko.utils.registerEventHandler(element, "change", function () {
            var observable = valueAccessor();
            var current = $el.datepicker("getDate");
            if (current instanceof Date && isFinite(current)) {
                var value = observable();
                var dateValue = new Date(value);
                if (dateValue - current !== 0) {
                    observable($el.datepicker("getDate"));
                }
            }
        });

        //handle disposal (if KO removes by the template binding)
        ko.utils.domNodeDisposal.addDisposeCallback(element, function() {
            $el.datepicker("destroy");
        });

    },
    update: function(element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor()),
            $el = $(element);

        //handle date data coming via json from Microsoft
        if (String(value).indexOf('/Date(') == 0) {
            value = new Date(parseInt(value.replace(/\/Date\((.*?)\)\//gi, "$1")));
        }

        var dateValue = new Date(value);

        var current = $el.datepicker("getDate");

        if (dateValue - current !== 0) {
            $el.datepicker("setDate", dateValue);
        }
    }
};