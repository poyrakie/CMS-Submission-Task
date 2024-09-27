using Microsoft.AspNetCore.Mvc;
using SubmissionTask.Models;
using SubmissionTask.Services;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace SubmissionTask.Controlllers
{
    public class ContactSurfaceController : SurfaceController
    {
        public ContactSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
        }

        public async Task<IActionResult> HandleSubmit(ContactFormModel form)
        {

            if (!ModelState.IsValid) 
            {
                TempData["name"] = form.Name;
                TempData["phone"] = form.Phone;
                TempData["email"] = form.Email;
                TempData["dropdown"] = form.Dropdown;

                TempData["error_name"] = string.IsNullOrEmpty(form.Name);
                TempData["error_phone"] = string.IsNullOrEmpty(form.Phone);
                TempData["error_email"] = string.IsNullOrEmpty(form.Email);
                TempData["error_dropdown"] = string.IsNullOrEmpty(form.Dropdown);

                return CurrentUmbracoPage();
            }
            EmailService emailService = new EmailService();
            var result = await emailService.SendEmailAsync(form.Email, form.Name);

            if (result)
            {
                TempData["success"] = "Your message has been sent";
            }
            else
            {
                TempData["success"] = "Something went wrong. Please try again later";
            }
            return RedirectToCurrentUmbracoPage();
        }
    }
}
