using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Domain.Common
{
    using System.ComponentModel.DataAnnotations.Schema;
    public class ItemRecord : EntityBase
    {
        public virtual int ItemContentId { get; set; }
        public virtual int Rating { get; set; }
        public virtual long NumOfView { get; set; }
        public virtual long NumOfDownload { get; set; }
        [ForeignKey("ItemContentId")]
        public virtual ItemContent ItemContent { get; set; }
    }
}
