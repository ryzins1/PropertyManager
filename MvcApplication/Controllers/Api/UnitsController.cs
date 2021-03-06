﻿using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using MvcApplication.Models;
using MvcApplication.Services;

namespace MvcApplication.Controllers.Api
{

	[RoutePrefix("api/companies/{companyid}/buildings/{buildingid}/units")]
    public class UnitsController : ApiController
    {
		private readonly Repository _repository;

	    public UnitsController(Repository repository)
	    {
	        _repository = repository;
	    }

	    [Route("", Name = "units")]
		public IHttpActionResult Get(string buildingid)
	    {
	        var units = _repository.Units.AsQueryable().Where(x => x.BuildingId.Equals(buildingid)).ToList();
	        return Ok(units);
	    }

		[Route("{id}", Name = "unit")]
		public IHttpActionResult Get(string buildingid, string id)
		{
		    var unit = _repository.Units.AsQueryable().FirstOrDefault(x => x.BuildingId.Equals(buildingid) && x.Id.Equals(id));
		    if (unit == null)
		        return NotFound();
		    return Ok(unit);
		}

		[Route]
		public IHttpActionResult Post(string buildingid, [FromBody]Unit unit)
		{
			var urlHelper = new UrlHelper(Request);

			unit.BuildingId = buildingid;
			unit.Id = ObjectId.GenerateNewId().ToString();
			unit.Url = urlHelper.Link("unit", new { buildingid, id = unit.Id });
			_repository.Units.Insert(unit);
			return Created(unit.Url, unit);
		}

		[Route("{id}")]
		public IHttpActionResult Put(string id, [FromBody]Unit unit)
	    {
			_repository.Units.Save(unit);
		    return Ok(unit);
	    }

		[Route("{id}")]
		public IHttpActionResult Delete(string id)
	    {
			_repository.Units.Remove(Query.EQ("_id", id));
		    return Ok("");
	    }
    }
}