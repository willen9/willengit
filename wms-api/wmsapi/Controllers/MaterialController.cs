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
    public class MaterialController : BaseController
    {
        private readonly MaterialRepository _MaterialRepository;

        public MaterialController(MaterialRepository Repository)
        {
            _MaterialRepository = Repository;
        }

        [HttpGet]
        [Route("materials")]
        [APIExceptionFilter]
        public IHttpActionResult GetMaterials()
        {
            string system_id = HttpHeaders["system_id"];
            string fields = HttpHeaders["fields"];
            string sort = HttpHeaders["sort"];
            int page = HttpHeaders["page"] == null ? -1 : int.Parse(HttpHeaders["page"]);
            int limit = HttpHeaders["limit"] == null ? -1 : int.Parse(HttpHeaders["limit"]);

            var result = _MaterialRepository.GetMaterial(fields, sort, page, limit);
            if (result == null || result.Count == 0)
                return Content(HttpStatusCode.NoContent, "");
            return Ok(result);
        }

        [HttpPost]
        [Route("materials")]
        [APIExceptionFilter]
        public IHttpActionResult CreateMaterials(MMMDB body)
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
            var result = _MaterialRepository.CreateMaterial(body, out ProcessMessage);

            if (!result)
            {
                return Content(HttpStatusCode.BadRequest,
                    APIUtility.GetErrorResponseJSONMessage(ProcessMessage));
            }

            return Content(HttpStatusCode.Created, "");
        }

        [HttpGet]
        [Route("materials/{id}")]
        [APIExceptionFilter]
        public IHttpActionResult GetMaterialById(string id)
        {
            string system_id = HttpHeaders["system_id"];
            string fields = HttpHeaders["fields"];

            var result = _MaterialRepository.GetMaterialById(id, fields);

            if (result == null)
            {
                return Content(HttpStatusCode.BadRequest,
                   APIUtility.GetErrorResponseJSONMessage("entity does not exist."));
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("materials/{id}")]
        [APIExceptionFilter]
        public IHttpActionResult DeleteMaterial(string id)
        {
            string system_id = HttpHeaders["system_id"];

            var result = _MaterialRepository.DeleteMaterial(id);

            if (!result)
                return new System.Web.Http.Results.StatusCodeResult(HttpStatusCode.NoContent, Request);

            return Ok();
        }

        [HttpPut]
        [Route("materials/{id}")]
        [APIExceptionFilter]
        public IHttpActionResult UpdateMaterial(string id, MMMDB body)
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
            var result = _MaterialRepository.UpdateMaterial(body, out ProcessMessage);

            if (id != body.MDB001)
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
