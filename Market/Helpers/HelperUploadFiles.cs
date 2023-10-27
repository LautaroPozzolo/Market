using Market.Providers;
using System;


namespace Market.Helpers
{
    public class HelperUploadFiles
    {
        private readonly PathProvider PathProvider;

        public HelperUploadFiles(PathProvider pathProvider)
        {
            PathProvider = pathProvider;
        }

        public async Task<string> UploadFileAsync(IFormFile formFile, string imageName, Folders folder)
        {
            string path = this.PathProvider.MapPath(imageName, folder);

            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            return path;
        }
    }
}
