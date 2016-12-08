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
    public class SupplierController : BaseController
    {
        private readonly SupplierRepository _SupplierRepository;

        public SupplierController(SupplierRepository Repository)
        {
            _SupplierRepository = Repository;
        }

        [HttpGet]
        [Route("suppliers")]
        [APIExceptionFilter]
        public IHttpActionResult GetSuppliers()
        {
            string system_id = HttpHeaders["system_id"];
            string fields = HttpHeaders["fields"];
            string sort = HttpHeaders["sort"];
            int page = HttpHeaders["page"] == null ? -1 : int.Parse(HttpHeaders["page"]);
            int limit = HttpHeaders["limit"] == null ? -1 : int.Parse(HttpHeaders["limit"]);

            var result = _SupplierRepository.GetSupplier(fields, sort, page, limit);
            if (result == null || result.Count == 0)
                return Content(HttpStatusCode.NoContent, "");
            return Ok(result);
        }

        [HttpPost]
        [Route("suppliers")]
        [APIExceptionFilter]
        public IHttpActionResult CreateSupplier(CMMDI body)
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
            var result = _SupplierRepository.CreateSupplier(body, out ProcessMessage);

            if (!result)
            {
                return Content(HttpStatusCode.BadRequest,
                    APIUtility.GetErrorResponseJSONMessage(ProcessMessage));
            }

            return Content(HttpStatusCode.Created, "");
        }

        [HttpGet]
        [Route("suppliers/{id}")]
        [APIExceptionFilter]
        public IHttpActionResult GetSupplierById(string id)
        {
            string system_id = HttpHeaders["system_id"];
            string fields = HttpHeaders["fields"];

            var result = _SupplierRepository.GetSupplierById(id, fields);

            if (result == null)
            {
                return Content(HttpStatusCode.BadRequest,
                    APIUtility.GetErrorResponseJSONMessage("entity does not exist."));
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("suppliers/{id}")]
        [APIExceptionFilter]
        public IHttpActionResult DeleteSupplier(string id)
        {
            string system_id = HttpHeaders["system_id"];

            var result = _SupplierRepository.DeleteSupplier(id);

            if (!result)
                return new System.Web.Http.Results.StatusCodeResult(HttpStatusCode.NoContent, Request);

            return Ok();
        }

        [HttpPut]
        [Route("suppliers/{id}")]
        [APIExceptionFilter]
        public IHttpActionResult UpdateSupplier(string id, CMMDI body)
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
            var result = _SupplierRepository.UpdateSupplier(body, out ProcessMessage);

            if (id != body.MDI001)
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
        [Route("supplier/categories")]
        [APIExceptionFilter]
        public IHttpActionResult GetSupplierCategories()
        {
            string system_id = HttpHeaders["system_id"];
            string fields = HttpHeaders["fields"];
            string sort = HttpHeaders["sort"];
            int page = HttpHeaders["page"] == null ? -1 : int.Parse(HttpHeaders["page"]);
            int limit = HttpHeaders["limit"] == null ? -1 : int.Parse(HttpHeaders["limit"]);

            var result = _SupplierRepository.GetCategories(fields, sort, page, limit);
            if (result == null || result.Count == 0)
                return Content(HttpStatusCode.NoContent, "");
            return Ok(result);
        }

        [HttpGet]
        [Route("supplier/categories/{category}")]
        [APIExceptionFilter]
        public IHttpActionResult GetSupplierCategoryById(string category)
        {
            string system_id = HttpHeaders["system_id"];
            string fields = HttpHeaders["fields"];

            var result = _SupplierRepository.GetCategoriesById(category, fields);

            if (result == null)
            {
                return Content(HttpStatusCode.BadRequest,
                    APIUtility.GetErrorResponseJSONMessage("entity does not exist."));
            }

            return Ok(result);
        }
    }
}
