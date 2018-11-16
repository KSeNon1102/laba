using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace labaratorius1
{
    class Program
    {
        private static RepositoryManager _repositoryManager = new RepositoryManager();
        
        static void Main(string[] args)
        {
            int resultSelect;
            string answer = string.Empty;
            do
            {
                GetMenu();
                answer=Console.ReadLine();
            }
            while (!int.TryParse(answer, out  resultSelect) && !(resultSelect>0 && resultSelect<5));
            var path = GetPath();
            var files = _repositoryManager.GetImageFile(path);
            var dirInfo = _repositoryManager.CreateTempFolder(path);
            switch (resultSelect)
            {
                case (1):
                    {                       
                        
                        foreach (var file in files)
                        {
                            using (var image = Image.FromFile(file.FullName))
                            {
                                ParserManager parseManager = new ParserManager
                                {
                                    Image = image
                                };
                                //побалывался:)
                                var dateFile = Task.Run(() => parseManager.PhotoCreateAsync());

                                _repositoryManager.CopyFileToTempFolder(file.FullName, dirInfo.FullName, dateFile.Result + ".jpg");
                            }                           

                        }

                        break;
                    }
                case (2):
                    {
                        
                        foreach (var file in files)
                        {
                            using (var image = Image.FromFile(file.FullName))
                            {
                                ParserManager parseManager = new ParserManager
                                {
                                    Image = image
                                };
                                
                                var dateFile = parseManager.PhotoCreate();


                                using (var graphics = Graphics.FromImage(image))
                                {
                                    var textBounds = graphics.VisibleClipBounds;
                                    textBounds.Inflate(-100, -100);


                                    graphics.DrawString(
                                        $"{dateFile}",
                                        new Font(FontFamily.GenericSerif, 60),
                                        Brushes.Yellow,
                                        textBounds
                                    );
                                    graphics.Save();
                                }
                                

                                _repositoryManager.CopyFileToTempFolder(image, dirInfo.FullName, dateFile + ".jpg");
                                
                            }
                        }
                        break;
                    }
                case (3):
                    {
                        foreach (var file in files)
                        {
                            using (var image = Image.FromFile(file.FullName))
                            {
                                ParserManager parseManager = new ParserManager
                                {
                                    Image = image
                                };
                                //побалывался:)
                                var dateFile = parseManager.PhotoYearCreate();

                                _repositoryManager.CopyFileToTempFolderWithFolder(file.FullName, dateFile, dirInfo.FullName, file.Name);
                            }

                        }
                        break;
                    }
                case (4):
                    {
                        // var jobj = JObject.Parse(responseFromServer);
                        //var responsejson = jobj.GetValue("response");
                        //var geoObjectCollection = ((JObject)responsejson).GetValue("GeoObjectCollection");
                        //var featureMember = ((JObject)geoObjectCollection).GetValue("featureMember");
                        //var geoObjects = featureMember.Values();
                        //List<JToken> geoObjectList = new List<JToken>();
                        //foreach (var geoObject in geoObjects)
                        //{
                        //    geoObjectList.Add(geoObject);
                        //}
                        //var firstgeoObject = geoObjectList[0].First.First.First.First.Values();



                        //reader.Close();
                        //dataStream.Close();
                        //response.Close();

                        //var coordinate = parseManager.GpsCoordinate();
                        //var webService = new WebServiceManager();
                        //string geocode = $"{coordinate.Value.ToString().Replace(',', '.')},{coordinate.Key.ToString().Replace(',', '.')}";
                        //var responseStream = webService.GetAddressesByGeocode(geocode);
                        //var responseFromServer = parseManager.GetAddressFromJSON(responseStream);

                        //Console.WriteLine(responseFromServer.ToString());

                        //var dirInfo = _repositoryManager.CreateTempFolder(path);
                        //repositoryManager.CopyFileToTempFolder(file.FullName, dirInfo.FullName, dateFile + ".jpg");
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            Console.ReadLine();
        }

        static string GetPath()
        {
            Console.Clear();
            Console.WriteLine("Please take path folder with photo...");
            return Console.ReadLine();
        }
        
        static void GetMenu()
        {
            Console.WriteLine("1. Image rename using date ");
            Console.WriteLine("2. Add date on photo");
            Console.WriteLine("3. Sort photo by year");
            Console.WriteLine("4. Sort photo by place ");

        }
    }

    //public class PhotoData
    //{
    //    private string data;
    //    private str
    //}
}
