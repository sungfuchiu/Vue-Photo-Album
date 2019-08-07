using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data_Photo_Album;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace API_Photo_Album.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private AlbumContext _albumContext;
        public LoginController(AlbumContext db)
        {
            _albumContext = db ?? throw new ArgumentNullException("AlbumContext");
        }

        [HttpPost]
        public ActionResult Post([FromBody]UserRequest request)
        {
            var user = _albumContext.Set<User>().SingleOrDefault(a => a.Email == request.email && a.Password == request.password);
            if (user != null)
            {
                user.AuthToken = user.GetAuthToken();
                _albumContext.SaveChanges();
                return Ok(new
                {
                    message = "Ok",
                    auth_token = user.AuthToken,
                    user_id = user.Id
                });
            }
            else
            {
                return Ok(new
                {
                    message = "Email or Password is wrong"
                });
            }
        }
    }
}