using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace API_Photo_Album.Controllers
{
    public class PhotosRequest
    {
        public string auth_token { get; set; }
        public string title { get; set; }
        public DateTime date { get; set; }
        public string description { get; set; }
        public IFormFile file_location { get; set; }
    }
}
