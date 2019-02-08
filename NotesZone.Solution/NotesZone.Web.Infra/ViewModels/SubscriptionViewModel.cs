using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Web.Infra.ViewModels
{
    using NotesZone.Domain.User;

    public abstract class SubscriptionViewModelBase
    {
        //public string userEMail { get; set; }
        //public int userId { get; set; }
        public List<UserSubscription> subscriptions { get; set; }
        public SubscriptionViewModelBase ()
	    {
            subscriptions = new List<UserSubscription>();
	    }
    }

    public class SubscriptionViewModel
    {
        public UserSubscriptionViewModel UserSubscriptionViewModel { get; set; }
        public HeaderViewModel Header { get; set; }
        public FooterViewModel Footer { get; set; }
        public DashboardViewModel DashBoard { get; set; }
        public SubscriptionViewModel()
        {
            UserSubscriptionViewModel = new UserSubscriptionViewModel();
        }
    }

    public class UserSubscriptionViewModel
    {
        public TrialSubscriptionViewModel TrialSubscriptionViewModel { get; set; }
        public PaidSubscriptionViewModel PaidSubscriptionViewModel { get; set; }
        public UserSubscriptionViewModel()
        {
            TrialSubscriptionViewModel = new TrialSubscriptionViewModel();
            PaidSubscriptionViewModel = new PaidSubscriptionViewModel();
        }
    }

    public class TrialSubscriptionViewModel : SubscriptionViewModelBase
    {
       
    }

    public class PaidSubscriptionViewModel
    {
        public UploadSubscriptionViewModel UploadSubscriptionViewModel { get; set; }
        public DownloadSubscriptionViewModel DownloadSubscriptionViewModel { get; set; }
        public PaidSubscriptionViewModel()
        {
            UploadSubscriptionViewModel = new UploadSubscriptionViewModel();
            DownloadSubscriptionViewModel = new DownloadSubscriptionViewModel();
        }
    }
    public class UploadSubscriptionViewModel : SubscriptionViewModelBase
    {
       
    }
    public class DownloadSubscriptionViewModel: SubscriptionViewModelBase
    {
       
    }
       

    public class SubscriptionViewModelFactory
    {

    }

}
