using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace labaratorius1
{
    public class RepositoryManager
    {
        public IList<FileInfo> GetImageFile(string path)
        {
            DirectoryInfo info = new DirectoryInfo(path);            
            var files = info.GetFiles("*.jpg");
            return files.ToList();
            
        }

        public DirectoryInfo CreateTempFolder(string path)
        {
            return Directory.CreateDirectory(path + "Temp"+ DateTime.Now.ToShortTimeString().Replace(':','_'));
        }

        public void CopyFileToTempFolder(string sourceFilePath, string newFileNamePath, string newName)
        {
            File.Copy(sourceFilePath, newFileNamePath +"\\" + newName);
            
        }

        public void CopyFileToTempFolderWithFolder(string sourceFilePath, string dirName , string newFileNamePath, string newName)
        {
            var result = Directory.CreateDirectory(newFileNamePath + "\\"+ dirName);
            CopyFileToTempFolder(sourceFilePath, result.FullName, newName);
        }
        public void CopyFileToTempFolder(Image image, string newFileNamePath, string newName)
        {
            var path = newFileNamePath + "\\" + newName;

            using (var stream = File.Open(path, FileMode.OpenOrCreate))
            {
                image.Save(stream, ImageFormat.Jpeg);
            }

        }
    }
}
