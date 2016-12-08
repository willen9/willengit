using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using wmsapi.Models;
using wmsapi.Repositories;

namespace wmsapi.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly CustomerRepository _CustomerRepository;
        public CustomerController(CustomerRepository Repository)
        {
            _CustomerRepository = Repository;
        }

        [HttpGet]
        [Route("customers")]
        [APIExceptionFilter]
        public IHttpActionResult GetCustomers()
        {
            string system_id = HttpHeaders["system_id"];
            string fields = HttpHeaders["fields"];
            string sort = HttpHeaders["sort"];
            int page = HttpHeaders["page"] == null ? -1 : int.Parse(HttpHeaders["page"]);
            int limit = HttpHeaders["limit"] == null ? -1 : int.Parse(HttpHeaders["limit"]);

            var result = _CustomerRepository.GetCustomers(fields, sort, page, limit);
            if (result == null || result.Count == 0)
                return Content(HttpStatusCode.NoContent, "");

            return Ok(result);
        }

        [HttpPost]
        [Route("customers")]
        [APIExceptionFilter]
        public IHttpActionResult CreateCustomer(CMMDG body)
        {
            string system_id = HttpHeaders["system_id"];

            if(!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));

                return Content(HttpStatusCode.BadRequest,
                    APIUtility.GetErrorResponseJSONMessage(message));
            }

            string ProcessMessage = "";
            var result = _CustomerRepository.CreateCustomer(body, out ProcessMessage);

            if(!result)
            {
                return Content(HttpStatusCode.BadRequest, 
                    APIUtility.GetErrorResponseJSONMessage(ProcessMessage));
            }
            return Content(HttpStatusCode.Created,"");
        }

        [HttpGet]
        [Route("customers/{id}")]
        [APIExceptionFilter]
        public IHttpActionResult GetCustomerById(string id)
        {
            string system_id = HttpHeaders["system_id"];
            string fields = HttpHeaders["fields"];

            var result = _CustomerRepository.GetCustomerById(id, fields);

            if (result == null)
            {
                return Content(HttpStatusCode.BadRequest,
                    APIUtility.GetErrorResponseJSONMessage("entity does not exist."));
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("customers/{id}")]
        [APIExceptionFilter]
        public IHttpActionResult DeleteCustomer(string id)
        {
            string system_id = HttpHeaders["system_id"];

            var result = _CustomerRepository.DeleteCustomer(id);

            if (!result)
                return new System.Web.Http.Results.StatusCodeResult(HttpStatusCode.NoContent, Request);

            return Ok();
        }

        [HttpPut]
        [Route("customers/{id}")]
        [APIExceptionFilter]
        public IHttpActionResult UpdateCustomer(string id, CMMDG body)
        {
            string system_id = HttpHeaders["system_id"];

            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));

                return Content(HttpStatusCode.BadRequest,
                    APIUtility.GetErrorResponseJSONMessage(message));
            }

            string ProcessMessage = "";
            var result = _CustomerRepository.UpdateCustomers(body, out ProcessMessage);

            if (id != body.MDG001)
            {
                return Content(HttpStatusCode.BadRequest,
                    APIUtility.GetErrorResponseJSONMessage(ProcessMessage));
            }

            if (!result)
            {
                return Content(HttpStatusCode.BadRequest, 
                    APIUtility.GetErrorResponseJSONMessage(ProcessMessage));
            }

            return Ok();
        }

        [HttpGet]
        [Route("customers/categories")]
        [APIExceptionFilter]
        public IHttpActionResult GetCustomerCategories()
        {
            string system_id = HttpHeaders["system_id"];
            string fields = HttpHeaders["fields"];
            string sort = HttpHeaders["sort"];
            int page = HttpHeaders["page"] == null ? -1 : int.Parse(HttpHeaders["page"]);
            int limit = HttpHeaders["limit"] == null ? -1 : int.Parse(HttpHeaders["limit"]);

            var result = _CustomerRepository.GetCategories(fields, sort, page, limit);
            if (result == null || result.Count == 0)
                return Content(HttpStatusCode.NoContent, "");
            return Ok(result);
        }

        [HttpGet]
        [Route("customers/categories/{category}")]
        [APIExceptionFilter]
        public IHttpActionResult GetCustomerCategoryById(string category)
        {
            string system_id = HttpHeaders["system_id"];
            string fields = HttpHeaders["fields"];

            var result = _CustomerRepository.GetCategoriesById(category, fields);

            if (result == null)
            {
                return Content(HttpStatusCode.BadRequest,
                    APIUtility.GetErrorResponseJSONMessage("entity does not exist."));
            }

            return Ok(result);
        }
    }
}
