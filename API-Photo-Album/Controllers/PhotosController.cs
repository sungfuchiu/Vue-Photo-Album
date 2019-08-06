using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Data_Photo_Album;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace API_Photo_Album.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private AlbumContext _albumContext;
        private readonly string _folder;
        public PhotosController(AlbumContext db, IHostingEnvironment env)
        {
            _albumContext = db ?? throw new ArgumentNullException("albumContext");
            _folder = $@"{env.WebRootPath}\UploadFolder";
        }
        // GET api/Photos/
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(new
            {
                data = _albumContext.Set<Photo>().Select(a => new PhotosResponse()
                {
                    id = a.Id,
                    date = a.Date,
                    description = a.Description,
                    file_location = new file_location(a.FileLocation),
                    title = a.Title
                }).ToList()
            });
        }
        // GET api/Photos/
        [HttpGet]
        public ActionResult<PhotosResponse> Get(int id)
        {
            try
            {
                return Ok(
                    _albumContext.Set<Photo>().Select(a => new PhotosResponse()
                    {
                        id = a.Id,
                        date = a.Date,
                        description = a.Description,
                        file_location = new file_location(a.FileLocation),
                        title = a.Title
                    }).Single(a => a.id == id));
            }
            catch (Exception ex)
            {
                return Ok(
                    new
                    {
                        error = "You need to sign in or sign up before continuing."
                    });
            }
        }
        // Post api/Photos/
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]PhotosRequest request)
        {
            try
            {
                var path = $@"{_folder}\{request.file_location.FileName}";

                if (request.file_location.Length > 0)
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await request.file_location.CopyToAsync(stream);
                    }
                }

                var newPhoto = new Photo()
                {
                    Title = request.title,
                    Date = request.date,
                    Description = request.description,
                    FileLocation = path,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                _albumContext.Set<Photo>().Add(newPhoto);
                return Ok(new
                {
                    message = "",
                    result = new
                    {
                        id = newPhoto.Id,
                        title = newPhoto.Title,
                        data = newPhoto.Date,
                        description = newPhoto.Description,
                        file_location = new
                        {
                            url = path
                        },
                        created_at = newPhoto.CreatedAt,
                        updated_at = newPhoto.UpdatedAt
                    }
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    errors = new
                    {
                        title = "can't be blank"
                    }
                });
            }
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm]PhotosRequest request)
        {
            try
            {
                var oldPhoto = _albumContext.Set<Photo>().Single(a => a.Id == id);

                System.IO.File.Delete(oldPhoto.FileLocation);
                if (request.file_location.Length > 0)
                {
                    using (var stream = new FileStream(oldPhoto.FileLocation, FileMode.Create))
                    {
                        await request.file_location.CopyToAsync(stream);
                    }
                }

                oldPhoto.Title = request.title;
                oldPhoto.Description = request.description;
                oldPhoto.Date = request.date;
                oldPhoto.UpdatedAt = DateTime.UtcNow;

                _albumContext.SaveChanges();
                return Ok(new
                {
                    message = "",
                    result = new
                    {
                        id = oldPhoto.Id,
                        title = oldPhoto.Title,
                        data = oldPhoto.Date,
                        description = oldPhoto.Description,
                        file_location = new
                        {
                            url = oldPhoto.FileLocation
                        },
                        created_at = oldPhoto.CreatedAt,
                        updated_at = oldPhoto.UpdatedAt
                    }
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    errors = new
                    {
                        title = "can't be blank"
                    }
                });
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id, string auth_token)
        {
            try
            {
                var photo = _albumContext.Set<Photo>().Single(a => a.Id == id);
                _albumContext.Set<Photo>().Remove(photo);
                return Ok(new
                    {
                        message = "Photo destroy successfully!"
                    });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}