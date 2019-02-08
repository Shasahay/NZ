using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Web.Infra.MediaItem
{
    using System.IO;
    public interface IMediaItemStorage
    {
        string StorageImage(MemoryStream stream, string fileName);
        string StorageDocumentFile(MemoryStream stream, string fileName);
    }
}
