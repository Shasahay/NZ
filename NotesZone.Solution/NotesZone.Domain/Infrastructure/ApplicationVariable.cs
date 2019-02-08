using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Domain.Infrastructure
{
    public static class ApplicationVariable
    {   
        
        public enum SubscriptionType
        {
            Trial = 3,
            Upload = 2,
            Download = 1,
            Paid= 4
        }
        /// <summary>
        ///  use the same order to insert the reocrd in DB
        ///  Presently I am hardcoding ( same ID it should save in DB) later will change and make it dynamic
        /// </summary>
        public static Dictionary<String, int> dictonarySubscriptionType = new Dictionary<string, int>(){
                                                                                            {"Download", 1},
                                                                                            {"Upload", 2},
                                                                                            {"Trial", 3},
                                                                                            {"paid", 4},
                                                                                       };

    }
}
