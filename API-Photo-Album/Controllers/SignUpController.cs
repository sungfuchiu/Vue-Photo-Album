using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data_Photo_Album;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Photo_Album.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private AlbumContext _albumContext;
        public SignUpController(AlbumContext db)
        {
            _albumContext = db ?? throw new ArgumentNullException("AlbumContext");
        }
        
        [HttpPost]
        public ActionResult Post([FromBody]UserRequest request)
        {
            string emailError = string.Empty;
            string passwordError = string.Empty;

            if (string.IsNullOrEmpty(request.password))
                passwordError = "can't be blank";
            try
            {
                if (_albumContext.Set<User>().Any(a => a.Email == request.email))
                {
                    emailError = "has already been taken";
                };
                if(!string.IsNullOrEmpty(passwordError) || !string.IsNullOrEmpty(emailError))
                    throw new Exception();
                var newUser = new User(request.email, request.password);
                _albumContext.Set<User>().Add(newUser);
                _albumContext.SaveChanges();
                return Ok(new
                {
                    user_id = newUser.Id
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    message = "Failed",
                    errors = new
                    {
                        email = emailError,
                        password = passwordError
                    }
                });
            }
        }
    }
}