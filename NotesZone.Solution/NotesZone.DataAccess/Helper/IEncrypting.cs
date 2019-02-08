using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.DataAccess.Helper
{
    public interface IEncrypting
    {
        string Encode(string password);
    }
}
