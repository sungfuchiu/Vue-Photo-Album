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
        public ActionResult Post(string email, string password)
        {
            try
            {
                var newUser = new User(email, password);
                _albumContext.Set<User>().Add(newUser);
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
                        email = "has already been taken",
                        password = "can't be blank"
                    }
                });
            }
        }
    }
}