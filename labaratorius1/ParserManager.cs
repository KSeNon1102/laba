using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using System.Threading.Tasks;

namespace labaratorius1
{
    public class ParserManager
    {
        private const int _DTOrig = 36867;
        private const int _GpsLatitude = 2;
        private const int _GpsLongitude = 4;
        
        private Image _image;

        public Image Image
        {
            get
            {
                return _image;
            } 
            set
            {
                _image = value;
            }
        }

        public ParserManager(FileInfo image)
        {
            _image = Image.FromFile(image.FullName);
        }
        public ParserManager() { }

        private byte[] PhotoGpsLatitude()
        {
            return _image.GetPropertyItem(_GpsLatitude).Value;
        }

        private byte[] PhotoLongitude()
        {
            return _image.GetPropertyItem(_GpsLongitude).Value;
        }

        private int[] ConverterFromByteArray(byte[] byteArray)
        {
            int[] item = new int[3];
            var code = Encoding.Unicode.GetChars(byteArray);

            item[0] = Convert.ToInt32(code[0]);
            item[1] = Convert.ToInt32(code[4]);            
            item[2] = Convert.ToInt32(code[8]);
            return item;
        }

        public Task<string> PhotoCreateAsync()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach(var item in _image.GetPropertyItem(_DTOrig).Value)
            {
                stringBuilder.Append(Convert.ToChar(item));
            }
            
            return Task.FromResult(stringBuilder.Remove(stringBuilder.Length - 1, 1).ToString().Replace(':', '.'));

        }

        public string PhotoCreate()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in _image.GetPropertyItem(_DTOrig).Value)
            {
                stringBuilder.Append(Convert.ToChar(item));
            }

            return stringBuilder.Remove(stringBuilder.Length - 1, 1).ToString().Replace(':', '.');

        }

        public string PhotoYearCreate()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in _image.GetPropertyItem(_DTOrig).Value)
            {
                stringBuilder.Append(Convert.ToChar(item));
            }
            string datestr = stringBuilder.ToString().Substring(0, 10 ).Replace(':','.');
            if (DateTime.TryParse(datestr, out DateTime result))
            {
                return result.Year.ToString();
            }
            return "SexyPhoto";
        }

        public KeyValuePair<double,double> GpsCoordinate()
        {
            var lat = ConverterFromByteArray(PhotoGpsLatitude());
            var lon = ConverterFromByteArray(PhotoLongitude());

            return new KeyValuePair<double, double>
                (double.Parse($"{lat[0]},{lat[1]}{lat[2]}"), 
                double.Parse($"{lon[0]},{lon[1]}{lon[2]}"));
        }

        public IList<Xml2CSharp.Ymaps> GetAddressFromJSON(Stream json)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Xml2CSharp.GeoObjectCollection));
            //if(xmlSerializer.CanDeserialize(json) )
            //var reader = new StreamReader(json);
            object o = xmlSerializer.Deserialize(json);
            return (List < Xml2CSharp.Ymaps>)o;
        }

       
    }
}
