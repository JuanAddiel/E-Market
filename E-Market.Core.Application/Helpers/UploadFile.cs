using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Helpers
{
    public static class UploadFile
    {
        public static List<string> UploadFiles(List<IFormFile> files, int id, bool isEditMode = false, List<string> imagePaths = null)
        {
            if (isEditMode && (files == null || files.Count == 0))
            {
                return imagePaths;
            }

            string basePath = $"/Image/Anuncio/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            // Creamos el folder si no existe
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> uploadedFilePaths = new List<string>();

            foreach (var file in files)
            {
                Guid guid = Guid.NewGuid();
                FileInfo fileInfo = new FileInfo(file.FileName);
                string fileName = guid + fileInfo.Extension;
                string fileNameWithPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                uploadedFilePaths.Add($"{basePath}/{fileName}");
            }

            if (isEditMode && imagePaths != null && imagePaths.Count > 0)
            {
                foreach (var imagePath in imagePaths)
                {
                    string[] oldImagePath = imagePath.Split("/");
                    string oldImageName = oldImagePath[^1];
                    string completeImageOld = Path.Combine(path, oldImageName);

                    if (System.IO.File.Exists(completeImageOld))
                    {
                        System.IO.File.Delete(completeImageOld);
                    }
                }
            }

            return uploadedFilePaths;
        }

    }
}
