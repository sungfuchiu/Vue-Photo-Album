using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Photo_Album.Controllers
{
    public class BaseFormat
    {
        public string message { get; set; }
        public object result { get; set; }
    }

    public class ErrorFormat
    {
        public Errors errors { get; set; }
    }

    public class Errors
    {
        public string title { get; set; }
    }
    public class BaseDateTime
    {
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
    public class file_location
    {
        public file_location(string url)
        {
            this.url = url;
        }
        public string url { get; set; }
    }
}
