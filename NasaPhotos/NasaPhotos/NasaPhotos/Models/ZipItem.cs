using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace NasaPhotos.Models
{
    public class ZipItem
    {
        public string Name { get; set; }
        public Stream Content { get; set; }
        public ZipItem(string name, Stream content)
        {
            this.Name = name;
            this.Content = content;
        }
        public ZipItem(string name, string contentStr, Encoding encoding)
        {
            // convert string to stream
            var byteArray = encoding.GetBytes(contentStr);
            var memoryStream = new MemoryStream(byteArray); this.Name = name;
            this.Content = memoryStream;
        }
    }
}
