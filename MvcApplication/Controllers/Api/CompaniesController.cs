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
	[RoutePrefix("api/companies")]
	public class CompaniesController : ApiController
	{
	    private readonly Repository _repository;

	    public CompaniesController(Repository repository)
	    {
	        _repository = repository;
	    }

	    [Route(Name = "companies")]
		public IHttpActionResult Get()
		{
			return Ok(_repository.Companies.FindAll().ToList());
	    }

		[Route("{id}", Name = "company")]
		public IHttpActionResult Get(string id)
		{
		    var company = _repository.Companies.AsQueryable().FirstOrDefault(b => b.Id.Equals(id));
		    if (company == null)
		        return NotFound();
		    return Ok(company);
		}

		[Route]
		public IHttpActionResult Post([FromBody]Company company)
		{
			var urlHelper = new UrlHelper(Request);

			company.Id = ObjectId.GenerateNewId().ToString();
            company.Url = urlHelper.Link("company", new { id = company.Id });

			_repository.Companies.Insert(company);
			return Created(company.Url, company);
		}

		[Route("{id}")]
		public IHttpActionResult Put(string id, [FromBody]Company company)
	    {
			_repository.Companies.Save(company);
		    return Ok(company);
	    }

		[Route("{id}")]
		public IHttpActionResult Delete(string id)
		{
		    foreach (var building in _repository.Buildings.FindAll())
		    {
		        _repository.Units.Remove(Query.EQ("BuildingId", building.Id));
		    }
		    _repository.Buildings.Remove(Query.EQ("CompanyId", id));
			_repository.Companies.Remove(Query.EQ("_id", id));
		    return Ok("");
	    }
	}
}