using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Web.Infra.ViewModels
{
    public class HomePageViewModel
    {
        public HeaderViewModel Header { get; set; }
        public FilterViewModel Filter { get; set; }
        public MainPageViewModel MainPage { get; set; }
        public DashboardViewModel DashBoard {get; set;}
        public FooterViewModel Footer {get;set;}
    }
}
