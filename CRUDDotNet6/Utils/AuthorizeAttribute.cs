using System;
using CRUDDotNet6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUDDotNet6.Utils
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute: Attribute, IAuthorizationFilter
	{
		public AuthorizeAttribute()
		{
			Console.WriteLine("Authorize");
		}

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Console.WriteLine("OnAuthorization");
            var user = context.HttpContext.Items["User"];
            if (user == null)
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}

