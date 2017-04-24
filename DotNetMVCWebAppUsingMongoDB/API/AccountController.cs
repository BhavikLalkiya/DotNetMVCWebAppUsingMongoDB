using DotNetMVCWebAppUsingMongoDB.Models;
using DotNetMVCWebAppUsingMongoDB.Models.Account;
using DotNetMVCWebAppUsingMongoDB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace DotNetMVCWebAppUsingMongoDB.API
{
    public class AccountController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }


        [HttpPost]
        public HttpResponseMessage Register(RegisterModel objData)
        {
            AccountServices.RegisterUsers(objData.Name, objData.Email, objData.Password);
            return Request.CreateResponse(HttpStatusCode.OK, new { Data = objData.Name });
        }

        [HttpPost]
        public HttpResponseMessage Login(LoginModel objLogin)
        {
            var user = AccountServices.LoginUsers(objLogin.Email, objLogin.Password).Result; //blogContext.Users.Find(x => x.Email == model.Email).SingleOrDefaultAsync();
            if (user == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Unauthorized));
                //return Request.CreateResponse(HttpStatusCode.Unauthorized, new { message = "Email address has not been registered.", Status = HttpStatusCode.Unauthorized });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { token = user.Id, Status = HttpStatusCode.OK });
        }

    }
}