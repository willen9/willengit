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
    public class StoreLocationController : BaseController
    {
        private readonly StoreLocationRepository _StoreLocationRepository;

        public StoreLocationController(StoreLocationRepository Repository)
        {
            _StoreLocationRepository = Repository;
        }

        [HttpGet]
        [Route("storeLocations")]
        [APIExceptionFilter]
        public IHttpActionResult GetStoreLocations()
        {
            string system_id = HttpHeaders["system_id"];
            string fields = HttpHeaders["fields"];
            string sort = HttpHeaders["sort"];
            int page = HttpHeaders["page"] == null ? -1 : int.Parse(HttpHeaders["page"]);
            int limit = HttpHeaders["limit"] == null ? -1 : int.Parse(HttpHeaders["limit"]);

            var result = _StoreLocationRepository.GetStoreLocation(fields, sort, page, limit);
            if (result == null || result.Count == 0)
                return Content(HttpStatusCode.NoContent, "");
            return Ok(result);
        }

        [HttpPost]
        [Route("storeLocations")]
        [APIExceptionFilter]
        public IHttpActionResult CreateStoreLocation(CMMDQ body)
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
            var result = _StoreLocationRepository.CreateStoreLocation(body, out ProcessMessage);

            if (!result)
            {
                return Content(HttpStatusCode.BadRequest,
                    APIUtility.GetErrorResponseJSONMessage(ProcessMessage));
            }

            return Content(HttpStatusCode.Created, "");
        }

        [HttpGet]
        [Route("storeLocations/{pk1}/{pk2}")]
        [APIExceptionFilter]
        public IHttpActionResult GetStoreLocationById(string pk1,string pk2)
        {
            string system_id = HttpHeaders["system_id"];
            string fields = HttpHeaders["fields"];

            var result = _StoreLocationRepository.GetStoreLocationById(pk1,pk2, fields);

            if (result == null)
            {
                return Content(HttpStatusCode.BadRequest,
                   APIUtility.GetErrorResponseJSONMessage("entity does not exist."));
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("storeLocations/{pk1}/{pk2}")]
        [APIExceptionFilter]
        public IHttpActionResult DeleteStoreLocation(string pk1,string pk2)
        {
            string system_id = HttpHeaders["system_id"];

            var result = _StoreLocationRepository.DeleteStoreLocation(pk1,pk2);

            if (!result)
                return new System.Web.Http.Results.StatusCodeResult(HttpStatusCode.NoContent, Request);

            return Ok();
        }

        [HttpPut]
        [Route("storeLocations/{pk1}/{pk2}")]
        [APIExceptionFilter]
        public IHttpActionResult UpdateStoreLocation(string pk1,string pk2, CMMDQ body)
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
            var result = _StoreLocationRepository.UpdateStoreLocation(body, out ProcessMessage);

            if (pk1 != body.MDQ001||pk2!=body.MDQ002)
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
    }
}
