using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NotesZone.Web.Infra.Extensions
{
    using NotesZone.Web.Infra.MediaItem;
    using System.IO;
    using System.Web;
    public static class HttpPostedFileExtension
    {
        public static string CreateImagePathFromStream(this HttpPostedFileBase postedFile, IMediaItemStorage imageStorage, string filePrefix, string itemTitle)
        {
            var imagePath = string.Empty;

            if (postedFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    postedFile.InputStream.CopyTo(memoryStream);

                   // imagePath = imageStorage.StorageImage(memoryStream, postedFile.FileName);
                    var fileName = postedFile.FileName.Split('.');
                    var newFileName = string.Concat(itemTitle, "_", filePrefix, ".", fileName[1]);
                    imagePath = imageStorage.StorageImage(memoryStream, newFileName);
                }
            }

            return imagePath;
        }
        public static string CreateDocumentFilePathFromStream(this HttpPostedFileBase postedFile, IMediaItemStorage imageStorage, string filePrefix, string itemTitle)
        {
            var documentPath = string.Empty;

            if (postedFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    postedFile.InputStream.CopyTo(memoryStream);
                    var fileName = postedFile.FileName.Split('.');
                    var newFileName = string.Concat(itemTitle, "_", filePrefix, ".", fileName[1]);
                    documentPath = imageStorage.StorageDocumentFile(memoryStream, newFileName);
                }
            }

            return documentPath;
        }
    }
}
