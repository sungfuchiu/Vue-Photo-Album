using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Photo_Album.Controllers
{
    public class PhotosResponse
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime date { get; set; }
        public string description { get; set; }
        public file_location file_location { get; set; }
    }
    public class PhotosCreateResponse : BaseDateTime
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime date { get; set; }
        public string description { get; set; }
        public file_location file_location { get; set; }
    }
}
