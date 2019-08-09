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
        private AlbumContext _db;
        private readonly string _baseFolder;
        private readonly string _folder = "UploadFolder";
        public PhotosController(AlbumContext db, IHostingEnvironment env)
        {
            _db = db ?? throw new ArgumentNullException("albumContext");
            _baseFolder = env.WebRootPath;
        }
        // GET api/Photos/
        [HttpGet("{id?}")]
        public ActionResult<PhotosResponse> Get(int? id)
        {
            try
            {
                if (id == null)
                {
                    return Ok(new
                    {
                        data = _db.Set<Photo>().Select(a => new PhotosResponse()
                        {
                            id = a.Id,
                            date = a.Date,
                            description = a.Description,
                            file_location = new file_location(a.FileLocation.Replace('\\','/')),
                            title = a.Title
                        }).ToList()
                    });
                }
                        return Ok(
                        _db.Set<Photo>().Select(a => new PhotosResponse()
                        {
                            id = a.Id,
                            date = a.Date,
                            description = a.Description,
                            file_location = new file_location(a.FileLocation.Replace('\\','/')),
                            title = a.Title
                        }).Single(a => a.id == id));
                throw new Exception();
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
                var path = $@"\{_folder}\{request.file_location.FileName}";

                if (request.file_location.Length > 0)
                {
                    using (var stream = new FileStream(System.IO.Directory.GetCurrentDirectory() + @"/wwwroot/" + path, FileMode.Create))
                    {
                        await request.file_location.CopyToAsync(stream);
                    }
                }

                var urlPath = $@"{_folder}\{request.file_location.FileName}";
                var newPhoto = new Photo()
                {
                    Title = request.title,
                    Date = request.date,
                    Description = request.description,
                    FileLocation = urlPath,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                _db.Set<Photo>().Add(newPhoto);
                _db.SaveChanges();
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
                            url = urlPath
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
                if (!_db.Set<User>().Any(a => a.AuthToken == request.auth_token))
                    return BadRequest();
                var oldPhoto = _db.Set<Photo>().Single(a => a.Id == id);

                System.IO.File.Delete(@"wwwroot/" + oldPhoto.FileLocation);
                var path = $@"\{_folder}\{request.file_location.FileName}";
                if (request.file_location.Length > 0)
                {
                    using (var stream = new FileStream(System.IO.Directory.GetCurrentDirectory() + @"/wwwroot/" + path, FileMode.Create))
                    {
                        await request.file_location.CopyToAsync(stream);
                    }
                }

                oldPhoto.Title = request.title;
                oldPhoto.Description = request.description;
                oldPhoto.FileLocation = path;
                oldPhoto.Date = request.date;
                oldPhoto.UpdatedAt = DateTime.UtcNow;

                _db.SaveChanges();
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
                if (!_db.Set<User>().Any(a => a.AuthToken == auth_token))
                    return BadRequest();
                var photo = _db.Set<Photo>().Single(a => a.Id == id);
                _db.Set<Photo>().Remove(photo);
                _db.SaveChanges();
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