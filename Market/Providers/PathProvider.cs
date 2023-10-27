using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Market.Providers
{
    public enum Folders
    {
        Uploads = 0, Images = 1, Documents = 2, Temp = 3
    }

    public class PathProvider
    {
        private IWebHostEnvironment hostEnvironment;
        public PathProvider(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public string MapPath(string fileName, Folders folder)
        {
            string file = "";

            if (folder == Folders.Uploads)
            {
                file = "uploads";
            }
            else if (folder == Folders.Images)
            {
                file = "images";
            }
            else if (folder == Folders.Documents)
            {
                file = "docments";
            }

            string path = Path.Combine(this.hostEnvironment.WebRootPath, file, fileName);

            if (folder == Folders.Temp)
            {
                path = Path.Combine(Path.GetTempPath(), fileName);
            }

            return path;
        }
    }
}
