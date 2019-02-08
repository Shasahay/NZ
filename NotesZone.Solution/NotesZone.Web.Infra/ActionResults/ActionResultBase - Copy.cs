using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesZone.Web.Infra.ActionResults
{
    using NotesZone.Web.Infra.Configurations;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    public abstract class MyActionResult : ActionResult
    {
        //public abstract void EnsureAllInjectInstanceNotNull();
    }
    public abstract class ActionResultBase<TController> : MyActionResult where TController : Controller
    {
        protected readonly Expression<Func<TController, ActionResult>> ViewNameExpression;
        protected readonly IExConfigurationManager ConfigurationManager;

        protected ActionResultBase(Expression<Func<TController, ActionResult>> expression)
            : this(DependencyResolver.Current.GetService<IExConfigurationManager>(), expression)
        {
                
        }

        protected ActionResultBase(IExConfigurationManager configurationManager,
            Expression<Func<TController, ActionResult>> expression)
        {
            this.ViewNameExpression = expression;
            this.ConfigurationManager = configurationManager;
        }

        protected ViewResult GetViewResult<TViewModel>(TViewModel viewModel)
        {
            var v = (MethodCallExpression)this.ViewNameExpression.Body;
            if (v.Method.ReturnType != typeof(ActionResult))
            {
                throw new ArgumentException("Action Method" + v.Method.Name + "does not return Action Result");

            }
            var result = new ViewResult { ViewName = v.Method.Name };
            result.ViewData.Model = viewModel;
            return result;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            //this.EnsureAllInjectInstanceNotNull();
        }

    }
}
