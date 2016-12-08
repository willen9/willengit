using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Newtonsoft.Json;
using REGAL.WMS.Communication;
using REGAL.WMS.ServiceReference;
using wmsapi.Models;
using wmsapi.Repositories;

namespace wmsapi.Controllers
{
    public class TransactionController : BaseController
    {
        private readonly TransactionRepository _TransactionRepository;

        public TransactionController(TransactionRepository Repository)
        {
            _TransactionRepository = Repository;
        }

        [HttpPost]
        [Route("transactions/mmb")]
        [APIExceptionFilter]
        public IHttpActionResult CreateMMBTransaction(MMB DocObject)
        {
            if (DocObject == null)
                return BadRequest();

            var header = DocObject as MMDCA;
            var details = DocObject.Items as List<MMDCB>;

            _TransactionRepository.CreateMMBTransaction(header, details);

            return Ok();
        }

        [HttpPost]
        [Route("transactions/mmc")]
        [APIExceptionFilter]
        public IHttpActionResult CreateMMCTransaction()
        {
            return Ok();
        }

        [HttpPost]
        [Route("transactions/mmd")]
        [APIExceptionFilter]
        public IHttpActionResult CreateMMDTransaction(MMD DocObject)
        {
            if (DocObject == null)
                return BadRequest();

            var header = DocObject as MMDEA;
            var details = DocObject.Items as List<MMDEB>;

            _TransactionRepository.CreateMMDTransaction(header, details);

            return Ok();
        }

        [HttpPost]
        [Route("transactions/mme")]
        [APIExceptionFilter]
        public IHttpActionResult CreateMMETransaction(MME DocObject)
        {
            if (DocObject == null)
                return BadRequest();

            var header = DocObject as MMDAA;
            var details = DocObject.Items as List<MMDAB>;

            _TransactionRepository.CreateMMETransaction(header, details);

            return Ok();
        }

        [HttpPost]
        [Route("transactions/mmf")]
        [APIExceptionFilter]
        public IHttpActionResult CreateMMFTransaction(MMF DocObject)
        {
            if (DocObject == null)
                return BadRequest();

            var header = DocObject as MMDLA;
            var details = DocObject.Items as List<MMDLB>;

            _TransactionRepository.CreateMMFTransaction(header, details);

            return Ok();
        }

        [HttpPost]
        [Route("transactions/mmg")]
        [APIExceptionFilter]
        public IHttpActionResult CreateMMGTransaction(MMG DocObject)
        {
            if (DocObject == null)
                return BadRequest();

            var header = DocObject as MMDGA;
            var details = DocObject.Items as List<MMDGB>;

            _TransactionRepository.CreateMMGTransaction(header, details);

            return Ok();
        }

        [HttpPost]
        [Route("transactions/mmh")]
        [APIExceptionFilter]
        public IHttpActionResult CreateMMHTransaction(MMH DocObject)
        {
            if (DocObject == null)
                return BadRequest();

            var header = DocObject as MMDHA;
            var details = DocObject.Items as List<MMDHB>;

            _TransactionRepository.CreateMMHTransaction(header, details);

            return Ok();
        }

        [HttpPost]
        [Route("transactions/mmi")]
        [APIExceptionFilter]
        public IHttpActionResult CreateMMITransaction(MMI DocObject)
        {
            if (DocObject == null)
                return BadRequest();

            var header = DocObject as MMDKA;
            var details = DocObject.Items as List<MMDKB>;

            _TransactionRepository.CreateMMITransaction(header, details);

            return Ok();
        }

        [HttpPost]
        [Route("transactions/mmj")]
        [APIExceptionFilter]
        public IHttpActionResult CreateMMJTransaction(MMJ DocObject)
        {
            if (DocObject == null)
                return BadRequest();

            var header = DocObject as MMDMA;
            var details = DocObject.Items as List<MMDMB>;

            _TransactionRepository.CreateMMJTransaction(header, details);

            return Ok();
        }

        [HttpPost]
        [Route("transactions/mmk")]
        [APIExceptionFilter]
        public IHttpActionResult CreateMMKTransaction(MMK DocObject)
        {
            if (DocObject == null)
                return BadRequest();

            var header = DocObject as MMDNA;
            var details = DocObject.Items as List<MMDNB>;

            _TransactionRepository.CreateMMKTransaction(header, details);

            return Ok();
        }

        [Route("cookrecipe/{recipe}"), HttpPost]
        public HttpResponseMessage GetCookRecipe(string recipe, CookRecipePostParameter posts)
        {
            try
            {
                var result = new HttpResponseMessage();
                var httpRequest = System.Web.HttpContext.Current.Request;

                object[] Ingredients = posts.Ingredients.Split(',');

                WCFDynamicProxy.Create<IDataTransferService>(
                                ServiceURLBuilder.Generate(System.Configuration.ConfigurationManager.AppSettings["CommunicationService"], "DataTransferService"))
                                .CookingRecipe(recipe, Ingredients);

                result.StatusCode = HttpStatusCode.OK;
                result.Content = new StringContent("");
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                return result;
            }
            catch (Exception ex)
            {
                return APIUtility.GetErrorResponseMessage(ex.Message);
            }            
        }

        public class CookRecipePostParameter
        {
            public string Ingredients { get; set; }
        }
    }
}
