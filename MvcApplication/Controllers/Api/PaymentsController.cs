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
	[RoutePrefix("api/companies/{companyid}/leases/{leaseid}/payments")]
    public class PaymentsController : ApiController
    {
	    private readonly Repository _repository;

	    public PaymentsController(Repository repository)
	    {
	        _repository = repository;
	    }

	    [Route(Name = "payments")]
		public IHttpActionResult Get(string companyid, string leaseid)
	    {
	        var payments = _repository.Payments.AsQueryable().Where(p => p.LeaseId.Equals(leaseid)).ToList();
			return Ok(payments);
	    }

		[Route("{id}", Name = "payment")]
		public IHttpActionResult Get(string companyid, string leaseid, string id)
		{
		    var payment = _repository.Payments.AsQueryable().FirstOrDefault(x => x.LeaseId.Equals(leaseid) && x.Id.Equals(id));
		    if (payment == null)
		        return NotFound();
		    return Ok(payment);
		}

		[Route]
		public IHttpActionResult Post(string companyid, string leaseid, [FromBody]Payment payment)
		{
			var urlHelper = new UrlHelper(Request);

			payment.Id = ObjectId.GenerateNewId().ToString();
            payment.Url = urlHelper.Link("payment", new { companyid, leaseid,  id = payment.Id });
		    payment.PaymentDate = DateTime.Now;

			_repository.Payments.Insert(payment);
			return Created(payment.Url, payment);
		}

		[Route("{id}")]
		public IHttpActionResult Put(string id, string leaseid, [FromBody]Payment payment)
	    {
			_repository.Payments.Save(payment);
		    return Ok(payment);
	    }

		[Route("{id}")]
		public IHttpActionResult Delete(string id)
		{
			_repository.Payments.Remove(Query.EQ("_id", id));
		    return Ok("");
	    }
    }
}
