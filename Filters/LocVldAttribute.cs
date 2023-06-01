using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using CompanySystem.DAL;
//using WebApiApp.Data.Models;
using WebApiApp.APIs;

namespace WebApiApp.Filters
{
    public class LocVldAttribute:ActionFilterAttribute
    {
        //filter run before the business logic
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Department? department=context.ActionArguments["department"] as Department;
            var allowedLocation = new string[]
            {
                "USA",
                "UK",
                "Germany",
                "Egypt"
            };
            if (department != null || !allowedLocation.Contains(department?.Location))
            {
                //short circuit with BadRequest
                context.Result = new BadRequestObjectResult(new GeneralResponse("Location is not covered"));
            }
            
        }
    }
}
