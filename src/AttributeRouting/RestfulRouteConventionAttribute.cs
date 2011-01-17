﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AttributeRouting.Extensions;

namespace AttributeRouting
{
    /// <summary>
    /// Automatically generates RESTful-style routes for controller actions matching 
    /// Index, New, Create, Show, Edit, Update, Delete, and Destroy.
    /// </summary>
    public class RestfulRouteConventionAttribute : RouteConventionAttribute
    {
        // Setup conventions
        private static readonly List<RestfulRouteConventionInfo> Conventions = new List<RestfulRouteConventionInfo>
        {
            new RestfulRouteConventionInfo("Index", "GET", ""),
            new RestfulRouteConventionInfo("New", "GET", "New"),
            new RestfulRouteConventionInfo("Create", "POST", ""),
            new RestfulRouteConventionInfo("Show", "GET", "{id}"),
            new RestfulRouteConventionInfo("Edit", "GET", "{id}/Edit"),
            new RestfulRouteConventionInfo("Update", "PUT", "{id}"),
            new RestfulRouteConventionInfo("Delete", "GET", "{id}/Delete"),
            new RestfulRouteConventionInfo("Destroy", "DELETE", "{id}")
        };

        public override IEnumerable<RouteAttribute> GetRouteAttributes(MethodInfo actionMethod)
        {
            var convention = Conventions.SingleOrDefault(c => c.ActionName == actionMethod.Name);
            if (convention != null)
                yield return new RouteAttribute(convention.Url, convention.HttpMethod);
        }

        public override string GetDefaultRoutePrefix(MethodInfo actionMethod)
        {
            return actionMethod.DeclaringType.GetControllerName();
        }

        private class RestfulRouteConventionInfo
        {
            public RestfulRouteConventionInfo(string actionName, string httpMethod, string url)
            {
                ActionName = actionName;
                HttpMethod = httpMethod;
                Url = url;
            }

            public string ActionName { get; private set; }
            public string HttpMethod { get; private set; }
            public string Url { get; private set; }
        }
    }
}