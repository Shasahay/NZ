using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Web.Infra.Configurations
{
    using System.Collections.Specialized;
    using System.Configuration;
    public interface IExConfigurationManager
    {
        object GetSetion(string sectionName);
        ConnectionStringSettingsCollection GetConnectionString();
        NameValueCollection GetAppSettings();
        string GetAppConfigBy(string appConfigName);
    }
}
