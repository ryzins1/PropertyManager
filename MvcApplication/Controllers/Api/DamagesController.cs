using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using MvcApplication.Models;
using MvcApplication.Services;

namespace MvcApplication.Controllers.Api
{
	[RoutePrefix("api/companies/{companyid}/leases/{leaseid}/damages")]
    public class DamagesController : ApiController
    {
	    private readonly Repository _repository;

	    public DamagesController(Repository repository)
	    {
	        _repository = repository;
	    }

	    [Route(Name = "damages")]
		public IHttpActionResult Get(string companyid, string leaseid)
	    {
	        var damages = _repository.Damages.AsQueryable().Where(p => p.LeaseId.Equals(leaseid)).ToList();
			return Ok(damages);
	    }

		[Route("{id}", Name = "damage")]
		public IHttpActionResult Get(string companyid, string leaseid, string id)
		{
		    var damage = _repository.Damages.AsQueryable().FirstOrDefault(x => x.LeaseId.Equals(leaseid) && x.Id.Equals(id));
		    if (damage == null)
		        return NotFound();
		    return Ok(damage);
		}

		[Route]
		public IHttpActionResult Post(string companyid, string leaseid, [FromBody]Damage damage)
		{
			var urlHelper = new UrlHelper(Request);

			damage.Id = ObjectId.GenerateNewId().ToString();
            damage.Url = urlHelper.Link("damage", new { companyid, leaseid,  id = damage.Id });
		    damage.DamageDate = DateTime.Now;

			_repository.Damages.Insert(damage);
			return Created(damage.Url, damage);
		}

		[Route("{id}")]
		public IHttpActionResult Put(string id, string leaseid, [FromBody]Damage damage)
	    {
			_repository.Damages.Save(damage);
		    return Ok(damage);
	    }

		[Route("{id}")]
		public IHttpActionResult Delete(string id)
		{
			_repository.Damages.Remove(Query.EQ("_id", id));
		    return Ok("");
	    }
    }
}