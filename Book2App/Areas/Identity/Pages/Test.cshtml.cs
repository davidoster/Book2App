using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace Book2App.Areas.Identity.Pages
{
    [Authorize] // [Authorize(Roles = "SA"]
    public class TestModel : PageModel
    {
        [BindProperty]
        public string TestData { get; set; }

        public IActionResult OnPost()
        {
            return RedirectToAction("TestMethod", "Test", new {data = TestData });
        }
    }
}
