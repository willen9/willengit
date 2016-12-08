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
    public class PlantController : BaseController
    {
        private readonly PlantRepository _PlantRepository;

        public PlantController(PlantRepository Repository)
        {
            _PlantRepository = Repository;
        }

        [HttpGet]
        [Route("plants")]
        [APIExceptionFilter]
        public IHttpActionResult GetPlants()
        {
            string system_id = HttpHeaders["system_id"];
            string fields = HttpHeaders["fields"];
            string sort = HttpHeaders["sort"];
            int page = HttpHeaders["page"] == null ? -1 : int.Parse(HttpHeaders["page"]);
            int limit = HttpHeaders["limit"] == null ? -1 : int.Parse(HttpHeaders["limit"]);

            var result = _PlantRepository.GetPlant(fields, sort, page, limit);
            if (result == null || result.Count == 0)
                return Content(HttpStatusCode.NoContent, "");
            return Ok(result);
        }

        [HttpPost]
        [Route("plants")]
        [APIExceptionFilter]
        public IHttpActionResult CreatePlant(CMMDP body)
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
            var result = _PlantRepository.CreatePlant(body, out ProcessMessage);

            if (!result)
            {
                return Content(HttpStatusCode.BadRequest,
                    APIUtility.GetErrorResponseJSONMessage(ProcessMessage));
            }

            return Content(HttpStatusCode.Created, "");
        }

        [HttpGet]
        [Route("plants/{id}")]
        [APIExceptionFilter]
        public IHttpActionResult GetPlantById(string id)
        {
            string system_id = HttpHeaders["system_id"];
            string fields = HttpHeaders["fields"];

            var result = _PlantRepository.GetPlantById(id, fields);

            if (result == null)
            {
                return Content(HttpStatusCode.BadRequest,
                   APIUtility.GetErrorResponseJSONMessage("entity does not exist."));
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("plants/{id}")]
        [APIExceptionFilter]
        public IHttpActionResult DeletePlant(string id)
        {
            string system_id = HttpHeaders["system_id"];

            var result = _PlantRepository.DeletePlant(id);

            if (!result)
                return new System.Web.Http.Results.StatusCodeResult(HttpStatusCode.NoContent, Request);

            return Ok();
        }

        [HttpPut]
        [Route("plants/{id}")]
        [APIExceptionFilter]
        public IHttpActionResult UpdatePlant(string id,CMMDP body)
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
            var result = _PlantRepository.UpdatePlant(body, out ProcessMessage);

            if (id != body.MDP001)
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
