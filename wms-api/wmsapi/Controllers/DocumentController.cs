using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Web.UI;
using Newtonsoft.Json;
using REGAL.Data.DataAccess;
using wmsapi.Models;
using wmsapi.Repositories;
using Newtonsoft.Json.Linq;

namespace wmsapi.Controllers
{
    public class DocumentController : BaseController
    {
        private readonly DocumentRepository _DocumentRepository;

        public DocumentController(DocumentRepository Repository)
        {
            _DocumentRepository = Repository;
        }

        [HttpGet]
        [Route("documents")]
        [APIExceptionFilter]
        public IHttpActionResult GetDocuments()
        {
            string system_id = HttpHeaders["system_id"];
            string fields = HttpHeaders["fields"];
            string sort = HttpHeaders["sort"];
            int page = HttpHeaders["page"] == null ? -1 : int.Parse(HttpHeaders["page"]);
            int limit = HttpHeaders["limit"] == null ? -1 : int.Parse(HttpHeaders["limit"]);

            var result = _DocumentRepository.GetDocuments(fields, sort, page, limit);
            if (result == null || result.Count == 0)
                return Content(HttpStatusCode.NoContent, "");
            return Ok(result);
        }

        [HttpGet]
        [Route("documents/{doctype}")]
        [APIExceptionFilter]
        public IHttpActionResult GetDocument(string doctype)
        {
            string system_id = HttpHeaders["system_id"];
            string fields = HttpHeaders["fields"];

            var result = _DocumentRepository.GetDocumentById(doctype, fields);

            if (result == null)
            {
                return Content(HttpStatusCode.BadRequest,
                    APIUtility.GetErrorResponseJSONMessage("entity does not exist."));
            }

            return Ok(result);
        }

        [HttpGet]
        [APIExceptionFilter]
        [Route("documents/{doctype}/number")]
        public IHttpActionResult GetDocumentNumber(string doctype)
        {
            string system_id = HttpHeaders["system_id"];
            
            var number = _DocumentRepository.GetDocumentNumber(doctype);
            return Ok(number);
        }

        [HttpGet]
        [APIExceptionFilter]
        [Route("documents/categories/{category}")]
        public IHttpActionResult GetDocumentsByCategory(string category)
        {
            string system_id = HttpHeaders["system_id"];
            string fields = HttpHeaders["fields"];

            var result = _DocumentRepository.GetDocumentByCategory(category, fields);

            if (result == null||result.Count==0)
            {
                return Content(HttpStatusCode.BadRequest,
                    APIUtility.GetErrorResponseJSONMessage("entity does not exist."));
            }

            return Ok(result);
        }

        /* [DOCUMENT CATEGORY USED]
         * 
         * MMB	庫存調整單 
         * MMC	庫存盤點單
         * MMD	庫存調撥單
         * MME	進貨單
         * MMF	退貨單
         * MMG	領料單
         * MMH	退料單
         * MMI	生產入庫單
         * MMJ  銷貨單
         * MMK  銷退單
         */

        [HttpGet]
        [APIExceptionFilter]
        [Route("documents/{doctype}/{number}")]
        public IHttpActionResult GetDocument(string doctype, string number)
        {
            //var document = _DocumentRepository.GetDocumentById(doctype, "");
            //if(document == null)
            //{
            //    return Content(HttpStatusCode.BadRequest, 
            //        APIUtility.GetErrorResponseJSONMessage("documnet type doesn't exist."));
            //}

            //string documentCategory = document.MDX004;

            _DocumentRepository.GetDocumentContent(doctype, number);

            dynamic content = null;
            //switch (documentCategory.ToUpper())
            //{
            //    case "MMB":
            //        break;

            //    case "MMC":
            //        break;

            //    case "MMD":
            //        break;

            //    case "MME":                    
            //        break;

            //    case "MMF":
            //        break;

            //    case "MMG":
            //        break;

            //    case "MMH":
            //        break;

            //    case "MMI":
            //        break;

            //    case "MMJ":
            //        break;

            //    case "MMK":
            //        break;

            //    default:
            //        content = null;
            //        break;
            //}

            if (content == null)
                return Content(HttpStatusCode.NoContent, "");

            return Ok(content);
        }
    }
}
