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
    public class StoreBinController : BaseController
    {
        private readonly StoreBinRepository _StoreBinRepository;

        public StoreBinController(StoreBinRepository Repository)
        {
            _StoreBinRepository = Repository;
        }

        [HttpGet]
        [Route("storeBins")]
        [APIExceptionFilter]
        public IHttpActionResult GetStoreBins()
        {
            string system_id = HttpHeaders["system_id"];
            string fields = HttpHeaders["fields"];
            string sort = HttpHeaders["sort"];
            int page = HttpHeaders["page"] == null ? -1 : int.Parse(HttpHeaders["page"]);
            int limit = HttpHeaders["limit"] == null ? -1 : int.Parse(HttpHeaders["limit"]);

            var result = _StoreBinRepository.GetStoreBin(fields, sort, page, limit);
            if (result == null || result.Count == 0)
                return Content(HttpStatusCode.NoContent, "");
            return Ok(result);
        }

        [HttpPost]
        [Route("storeBins")]
        [APIExceptionFilter]
        public IHttpActionResult CreateStoreBin(CMMDR body)
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
            var result = _StoreBinRepository.CreateStoreBin(body, out ProcessMessage);

            if (!result)
            {
                return Content(HttpStatusCode.BadRequest,
                    APIUtility.GetErrorResponseJSONMessage(ProcessMessage));
            }

            return Content(HttpStatusCode.Created, "");
        }

        [HttpGet]
        [Route("storeBins/{pk1}/{pk2}/{pk3}")]
        [APIExceptionFilter]
        public IHttpActionResult GetStoreBinById(string pk1,string pk2,string pk3)
        {
            string system_id = HttpHeaders["system_id"];
            string fields = HttpHeaders["fields"];

            var result = _StoreBinRepository.GetStoreBinById(pk1, pk2,pk3, fields);

            if (result == null)
            {
                return Content(HttpStatusCode.BadRequest,
                   APIUtility.GetErrorResponseJSONMessage("entity does not exist."));
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("storeBins/{pk1}/{pk2}/{pk3}")]
        [APIExceptionFilter]
        public IHttpActionResult DeleteStoreBin(string pk1, string pk2, string pk3)
        {
            string system_id = HttpHeaders["system_id"];

            var result = _StoreBinRepository.DeleteStoreBin(pk1,pk2,pk3);

            if (!result)
                return new System.Web.Http.Results.StatusCodeResult(HttpStatusCode.NoContent, Request);

            return Ok();
        }

        [HttpPut]
        [Route("storeBins/{pk1}/{pk2}/{pk3}")]
        [APIExceptionFilter]
        public IHttpActionResult UpdateStoreBin(string pk1,string pk2,string pk3, CMMDR body)
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
            var result = _StoreBinRepository.UpdateStoreBin(body, out ProcessMessage);

            if (pk1 != body.MDR001||pk2!=body.MDR002||pk3!=body.MDR003)
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
