using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace IdlavbsTodoList_API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class APIController : ControllerBase
    {

        public class classLogin
        {
            public string userMail { get; set; }
            public string userPassword { get; set; }
        }

        [HttpPost("Login")]
        public ActionResult Login([FromBody] classLogin _login)
        {
            DAL _dal = new DAL();
            Users _user = _dal.checkLogin(_login.userMail, _login.userPassword);

            if (_user.UserID == 0)
                return new JsonResult(new { Message = "Invalid UserName or UserPassword" }) { StatusCode = StatusCodes.Status401Unauthorized };

            var cookieOptions = new CookieOptions()
            {
                Path = "/",
                Expires = DateTimeOffset.UtcNow.AddMinutes(30),
                IsEssential = true,
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            };
            Response.Cookies.Append("UserCode", _user.UserID.ToString(), cookieOptions);

            bool result = _dal.updateLastLogin(_user.UserID.ToString());
            return new JsonResult(new { UserID = _user.UserID, UserName = _user.UserName }) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpGet("listWork")]
        public ActionResult listWork()
        {
            DAL _dal = new DAL();

            List<Work> _list = _dal.listWork(HttpContext.Request.Cookies["UserCode"].ToString());


            return Ok(JsonConvert.SerializeObject(_list));
        }

        [HttpPost("postWork")]
        public ActionResult postWork([FromBody]Work _work)
        {
            DAL _dal = new DAL();
            _work.UserID = Convert.ToInt32(Request.Cookies["UserCode"].ToString());
            bool result = _dal.postWork(_work);

            if (!result)
                return Unauthorized(new { Message = "Error" });

            return new JsonResult(new { Message = "Successed" }) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpPost("DeleteWork")]
        public ActionResult deletWork([FromBody] Work _work)
        {
            DAL _dal = new DAL();
            _work.UserID = Convert.ToInt32(Request.Cookies["UserCode"].ToString());
            bool result = _dal.deletWork(_work.WorkID);

            if (!result)
                return Unauthorized(new { Message = "Error" });

            return new JsonResult(new { Message = "Successed" }) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
