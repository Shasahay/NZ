using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotesZone.Web.Controllers
{
    using NotesZone.Web.Infra;
    using NotesZone.Web.Infra.ActionResults.Client;
    using System.ComponentModel;
    using System.Configuration;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    public class SubscriptionController : BaseController
    {
        // GET: Subscription
        public ActionResult Index()
        {
            return new SubscriptionViewModelActionResult<SubscriptionController>(x => x.Index());
            
        }
        #region Working code in async mode.
        /// <summary>
        /// not sure task ans wait is preperly woking need to validate from expert
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        //public async Task<ActionResult>  Download( string fileName)
        //{
        //    try
        //    {
        //        var virtualPath = ConfigurationManager.AppSettings["DocumentItemPath"].ToString(CultureInfo.InvariantCulture);
        //        var physicalPath = System.Web.HttpContext.Current.Request.MapPath(virtualPath);
        //        physicalPath = string.Concat(physicalPath, fileName, ".pdf");
        //        using (var webClient = new WebClient())
        //        {
        //            //WebClient webClient = new WebClient();
        //            //webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
        //            //webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
        //            var destinationFileName = fileName == null ? fileName : fileName.Split('_')[0];
        //            var destinationPath = string.Concat(@"C:\Users\Shashank.Sahay\Desktop\Order\", destinationFileName, ".pdf");  // need to set as per user user selection or download
        //            //await webClient.DownloadFileAsync(new Uri(physicalPath), destinationPath);
        //            await Task.Run(() => webClient.DownloadFileAsync(new Uri(physicalPath), destinationPath));
        //            //webClient.Dispose();
        //        }


        //    }
        //    catch(Exception ex)
        //    {

        //    }
        //    return null;
        //} 
        #endregion


        public FileResult Download(string fileName)
        {
            var virtualPath = ConfigurationManager.AppSettings["DocumentItemPath"].ToString(CultureInfo.InvariantCulture);
            var physicalPath = System.Web.HttpContext.Current.Request.MapPath(virtualPath);
            var currentFile = string.Concat(fileName, ".pdf");
            physicalPath = string.Concat(physicalPath, currentFile);
            
            string contentType = "application/pdf";
            return File(physicalPath, contentType, currentFile);
        }
    }
}