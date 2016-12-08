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
    public class DepartmentController : BaseController
    {
        private readonly DepartmentRepository _DepartmentRepository;
        public DepartmentController(DepartmentRepository Repository)
        {
            _DepartmentRepository = Repository;
        }

        [HttpGet]
        [Route("departments")]
        [APIExceptionFilter]
        public IHttpActionResult GetDepartments()
        {
            string system_id = HttpHeaders["system_id"];
            string fields = HttpHeaders["fields"];
            string sort = HttpHeaders["sort"];
            int page = HttpHeaders["page"] == null ? -1 : int.Parse(HttpHeaders["page"]);
            int limit = HttpHeaders["limit"] == null ? -1 : int.Parse(HttpHeaders["limit"]);

            var result = _DepartmentRepository.GetDepartments(fields, sort, page, limit);
            if (result == null || result.Count == 0)
                return Content(HttpStatusCode.NoContent, "");

            return Ok(result);
        }

        [HttpPost]
        [Route("departments")]
        [APIExceptionFilter]
        public IHttpActionResult CreateDepartment(CMMDC body)
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
            var result = _DepartmentRepository.CreateDepartment(body, out ProcessMessage);

            if (!result)
            {
                return Content(HttpStatusCode.BadRequest,
                    APIUtility.GetErrorResponseJSONMessage(ProcessMessage));
            }

            return Content(HttpStatusCode.Created, "");
        }

        [HttpGet]
        [Route("departments/{id}")]
        [APIExceptionFilter]
        public IHttpActionResult GetDepartmentById(string id)
        {
            string system_id = HttpHeaders["system_id"];
            string fields = HttpHeaders["fields"];

            var result = _DepartmentRepository.GetDepartmentById(id, fields);

            if (result == null)
            {
                return Content(HttpStatusCode.BadRequest,
                    APIUtility.GetErrorResponseJSONMessage("entity does not exist."));
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("departments/{id}")]
        [APIExceptionFilter]
        public IHttpActionResult DeleteDepartment(string id)
        {
            string system_id = HttpHeaders["system_id"];

            var result = _DepartmentRepository.DeleteDepartment(id);

            if (!result)
                return new System.Web.Http.Results.StatusCodeResult(HttpStatusCode.NoContent, Request);

            return Ok();
        }

        [HttpPut]
        [Route("departments/{id}")]
        [APIExceptionFilter]
        public IHttpActionResult UpdateDepartment(string id, CMMDC body)
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
            var result = _DepartmentRepository.UpdateDepartment(body, out ProcessMessage);

            if (id != body.MDC001)
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
