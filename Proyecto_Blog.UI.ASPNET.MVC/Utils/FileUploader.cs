using Proyecto_Blog.BE.Models;

namespace Proyecto_Blog.UI.ASPNET.MVC.Utils
{
    public static class FileUploader
    {
        public static string updloadFile(IWebHostEnvironment webHostEnvironment, IFormFileCollection files, string folder)
        {
            string imageUrl = string.Empty;

            try
            {
                if (files.Count() > 0)
                {
                    string nameFile = Guid.NewGuid().ToString();//Guid genera un id generico de caracteres
                    var upload = Path.Combine(webHostEnvironment.WebRootPath, $@"imagenes\{folder}");
                    var exten = Path.GetExtension(files[0].FileName);
                    IsFolderExist(upload);

                    using (var fileStream = new FileStream(Path.Combine(upload, nameFile + exten), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    imageUrl = $@"\imagenes\{folder}\{nameFile + exten}";

                }
                else
                {
                    imageUrl = Constants.NO_HAVE_FILE;
                }
            }
            catch (Exception e)
            {
               
                throw (new Exception(Constants.ERROR_UPLOAD_FILE, e));

            }

            return imageUrl;
        }

        private static void IsFolderExist(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
