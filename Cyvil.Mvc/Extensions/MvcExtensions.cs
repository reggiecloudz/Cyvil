#nullable disable
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cyvil.Mvc.Extensions
{
    public static class MvcExtensions
    {
        public static string ActiveClass(this IHtmlHelper htmlHelper, string controller = null, string action = null, string cssClass = "active")
        {
            var currentController = htmlHelper?.ViewContext.RouteData.Values["controller"] as string;
            var currentAction = htmlHelper?.ViewContext.RouteData.Values["action"] as string;

            var acceptedController = (controller ?? currentController ?? "").Split(",");
            var acceptedAction = (action ?? currentAction ?? "").Split(",");

            return acceptedController.Contains(currentController) && acceptedAction.Contains(currentAction) ? cssClass : "";
        }
    }
}