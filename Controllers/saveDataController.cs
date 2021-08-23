using donationApi.Common.BL;
using donationApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;

namespace donationApi.Controllers
{
    public class saveDataController : ApiController
    {

        [HttpPost]
        [Route("api/SaveData/saveDataDonation")]
        [ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage saveDataDonation([FromBody]clsRequest data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    clsDonationBl bl = new clsDonationBl();
                    Boolean oRequest = bl.SaveData(data);

                    return Request.CreateResponse(HttpStatusCode.OK, oRequest);
                }
                else
                {
                    //List<ModelError> errors = new List<ModelError>();

                    //foreach (ModelState modelState in ModelState.Values)
                    //{
                    //    foreach (ModelError error in modelState.Errors)
                    //    {
                    //        //DoSomethingWith(error);
                    //        errors.Add(error);
                    //    }
                    //}
                    //return Request.CreateResponse(HttpStatusCode.InternalServerError, errors);
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
