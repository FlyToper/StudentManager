﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace 学生选课信息管理系统
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
