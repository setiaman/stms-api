using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using stms_api.Models;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;

namespace stms_api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Course>("Courses");
            builder.EntitySet<Student>("Students");
            builder.EntitySet<KeyPool>("KeyPools");
            builder.EntitySet<Class>("Classes");
            builder.EntitySet<Trainer>("Trainers");
            builder.EntitySet<Company>("Companies");
            builder.EntitySet<RegistrationHeader>("RegistrationHeaders");
            builder.EntitySet<RegistrationItem>("RegistrationItems");


            //ActionConfiguration getKey = builder.Entity<KeyPool>().Action("getKey");
            //getKey.Returns<int>();

            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
         
        }
    }
}

