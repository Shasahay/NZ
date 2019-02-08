using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Domain
{
    public class EntityBase
    {
        #region private fields
        DateTime _CreatedDate = DateTime.Now;
        DateTime _lastModifiedDate = DateTime.Now;
        #endregion

        #region Public Properties

        public virtual int Id
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates the last date on which the entity was modified.
        /// </summary>
        public virtual DateTime ModifiedDate { get { return _lastModifiedDate; } set { _lastModifiedDate = value; } }
        /// <summary>
        /// Indicates the date on which the entity was first created.
        /// </summary>
        public virtual DateTime CreatedDate { get { return _CreatedDate; } set { _CreatedDate = value; } }
        /// <summary>
        /// Indicates the user who last modified the entity 
        /// </summary>
        public virtual string ModifiedBy { get; set; }
        /// <summary>
        /// Indicates the user who first created the entity
        /// </summary>
        public virtual string CreatedBy { get; set; }
        #endregion
    }
}
