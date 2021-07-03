using Microsoft.AspNetCore.Mvc;
using ReadRawRequestBodyExample.Models;
using System;
using System.Threading.Tasks;

namespace ReadRawRequestBodyExample.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new IndexViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> NoModelBinding()
        {
            var rawRequestBody = await Request.GetRawBodyAsync().ConfigureAwait(false);

            return Content(rawRequestBody);
        }

        [HttpPost]
        public async Task<IActionResult> WithModelBinding(IndexViewModel model)
        {
            var rawRequestBody = await Request.GetRawBodyAsync().ConfigureAwait(false);

            return Content(rawRequestBody + Environment.NewLine + model.Email);
        }
    }
}
