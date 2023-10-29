using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline
{
    public sealed class Test1Middleware
    {
        //private readonly delegate _next; // check if using events will help instead of calling next delegate -> parent.ExecuteNext event

        ///*
        //if using event: 
        //1. then pass pipeline reference and maybe use pipeline.Context property 
        //or
        //2. pass context which contains reference to pipeline ?
        //*/

        //public async Task InvokeAsync(HttpContext context)
        //{
        //    var cultureQuery = context.Request.Query["culture"];
        //    if (!string.IsNullOrWhiteSpace(cultureQuery))
        //    {
        //        var culture = new CultureInfo(cultureQuery);

        //        CultureInfo.CurrentCulture = culture;
        //        CultureInfo.CurrentUICulture = culture;
        //    }

        //    // Call the next delegate/middleware in the pipeline.
        //    await _next(context);
        //}
    }
}
