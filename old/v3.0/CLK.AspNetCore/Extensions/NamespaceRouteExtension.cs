using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLK.AspNetCore
{
    internal static class NamespaceRouteExtension
    {
        // Methods
        public static void AddNamespaceRoute(this IList<IApplicationModelConvention> conventions)
        {
            #region Contracts

            if (conventions == null) throw new ArgumentException();

            #endregion

            // Attach
            conventions.Add(new NamespaceRouteConvention());
        }


        // Class
        private class NamespaceRouteConvention : IApplicationModelConvention
        {
            // Methods
            public void Apply(ApplicationModel application)
            {
                #region Contracts

                if (application == null) throw new ArgumentException();

                #endregion

                // Apply
                foreach (var controller in application.Controllers)
                {
                    // Require
                    var hasAttributeRouteModels = controller.Selectors.Any(selector => selector.AttributeRouteModel != null);
                    if (hasAttributeRouteModels == true) return;

                    // Attach
                    controller.Selectors[0].AttributeRouteModel = new AttributeRouteModel()
                    {
                        Template = controller.ControllerType.Namespace.Replace('.', '/') + "/[controller]/[action]"
                    };
                }
            }
        }
    }    
}
