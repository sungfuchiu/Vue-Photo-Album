using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data_Photo_Album;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Photo_Album.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogoutController : Controller
    {
        private AlbumContext _albumContext;
        public LogoutController(AlbumContext db)
        {
            _albumContext = db ?? throw new ArgumentNullException("AlbumContext");
        }

        public class auth
        {
            public string auth_token { get; set; }
        }
        [HttpPost]
        public ActionResult Post([FromBody]auth request)
        {
            try
            {
                var user = _albumContext.Set<User>().Single(a => a.AuthToken == request.auth_token);
                user.AuthToken = string.Empty;
                _albumContext.SaveChanges();
                return Ok(new
                {
                    message = "Ok"
                });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
