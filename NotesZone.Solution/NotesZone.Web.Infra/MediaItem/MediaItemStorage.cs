using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Web.Infra.MediaItem
{
    using System.Configuration;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Web;
    public class MediaItemStorage : IMediaItemStorage
    {
        public string StorageImage(MemoryStream stream, string fileName)
        {
            // TODO: should remove dependency on ConfigurationManager
            var virtualPath = ConfigurationManager.AppSettings["ImageItemPath"].ToString(CultureInfo.InvariantCulture);

            var physicalPath = HttpContext.Current.Request.MapPath(virtualPath);

            if (string.IsNullOrEmpty(physicalPath))
                throw new NoNullAllowedException("Physical path should not be null");

            var fullPath = new FileStream((Path.Combine(physicalPath, fileName)), FileMode.Create);

            stream.WriteTo(fullPath);

            return fileName;
        }

        public string StorageDocumentFile(MemoryStream stream, string fileName)
        {
            var virtualPath = ConfigurationManager.AppSettings["DocumentItemPath"].ToString(CultureInfo.InvariantCulture);

            var physicalPath = HttpContext.Current.Request.MapPath(virtualPath);

            if (string.IsNullOrEmpty(physicalPath))
                throw new NoNullAllowedException("Physical path should not be null");

            var fullPath = new FileStream((Path.Combine(physicalPath, fileName)), FileMode.Create);

            stream.WriteTo(fullPath);

            return fileName;
        }
    }
}
