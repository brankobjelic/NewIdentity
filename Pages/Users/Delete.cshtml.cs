using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;
using NewIdentityApp.Data;

namespace NewIdentityApp.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;

        public DeleteModel(UserManager<ApplicationUser> userManager, ApplicationDbContext ctx)
        {
            this.userManager = userManager;
        }

        [BindProperty]
        public ApplicationUser? AppUser { get; set; }
        public async Task<IActionResult> OnGet(string id)
        {
            AppUser = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (AppUser == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            AppUser = await userManager.Users.FirstOrDefaultAsync(x => x.Id == AppUser.Id);
            if (AppUser == null)
            {
                return NotFound();
            }
            AppUser.IsEnabled = false;
            await userManager.UpdateAsync(AppUser);
			TempData["AlertSuccess"] = "Successfully deleted user " + AppUser.UserName;
			return RedirectToPage("Index");
        }
    }
}
