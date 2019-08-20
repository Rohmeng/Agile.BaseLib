using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Agile.BaseLib.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace Agile.BaseLib.Razors
{
    public class RazorViewToStringRenderer
    {
        private readonly IRazorViewEngine _viewEngine;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITempDataProvider _tempDataProvider;

        public RazorViewToStringRenderer(IRazorViewEngine viewEngine, IServiceProvider serviceProvider, ITempDataProvider dataProvider)
        {
            _viewEngine = viewEngine;
            _serviceProvider = serviceProvider;
            _tempDataProvider = dataProvider;
        }

        public async Task<string> RenderViewToStringAsync<TModel>(string viewLocation, TModel model)
        {
            var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
            //HttpContext httpContext = _accessor.HttpContext;
            //httpContext.RequestServices = _serviceProvider;

            ActionContext actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            IView view = FindView(actionContext, viewLocation);

            using (StringWriter stringWriter = new StringWriter())
            {
                ViewDataDictionary<TModel> viewDataDictionary = new ViewDataDictionary<TModel>(
                    new EmptyModelMetadataProvider(),
                    new ModelStateDictionary());

                viewDataDictionary.Model = model;

                TempDataDictionary tempDataDictionary = new TempDataDictionary(
                    actionContext.HttpContext,
                    _tempDataProvider);

                HtmlHelperOptions htmlHelperOptions = new HtmlHelperOptions();

                ViewContext viewContext = new ViewContext(
                    actionContext,
                    view,
                    viewDataDictionary,
                    tempDataDictionary,
                    stringWriter,
                    htmlHelperOptions);
                //viewContext.RouteData = _accessor.HttpContext.GetRouteData();
                await view.RenderAsync(viewContext);
                return stringWriter.ToString();
            }
        }

        private IView FindView(ActionContext actionContext, string viewLocation)
        {
            ViewEngineResult getViewResult = _viewEngine.GetView(null, viewLocation, true);

            if (getViewResult.Success)
            {
                return getViewResult.View;
            }

            ViewEngineResult findViewResult = _viewEngine.FindView(actionContext, viewLocation, true);

            if (findViewResult.Success)
            {
                return findViewResult.View;
            }

            IEnumerable<string> searchedLocations = getViewResult.SearchedLocations.Concat(findViewResult.SearchedLocations);

            string message = string.Join(
                Environment.NewLine,
                new[] { $"Unable to find view '{viewLocation}'. The following locations were searched:" }.Concat(searchedLocations)); ;

            throw new Exception(message);
        }
    }
}
