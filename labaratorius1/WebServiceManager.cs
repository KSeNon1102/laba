using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace labaratorius1
{
    public class WebServiceManager
    {
        private const string _ApiKey = "4fa832ed-420b-4fad-a9a4-1a5496271bda";
        public Stream GetAddressesByGeocode(string geocode)
        {
            var request = WebRequest.Create($"https://geocode-maps.yandex.ru/1.x/?apikey={_ApiKey}&geocode={geocode}");
            var response = (HttpWebResponse)request.GetResponse();
            //var dataStream =
            //response.GetResponseStream();
            //var reader = new StreamReader(dataStream);
            //var responseFromServer = reader.ReadToEnd();
            //XMLObj 
            //return responseFromServer;
            return /*var dataStream =*/ response.GetResponseStream();
        }
    }
}
