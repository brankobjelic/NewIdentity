using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NewIdentityApp.Data;
using NewIdentityApp.Models;

namespace NewIdentityApp.Pages.Users
{
    [Authorize(Roles ="Admin")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;


        public IndexModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [BindProperty]
        public IList<UserRolesViewModel> Model { get; set; } = new List<UserRolesViewModel>();
        public async Task<IActionResult> OnGetAsync()
        {
            var users = await _userManager.Users.Where(x => x.IsEnabled == true).ToListAsync();

            foreach (ApplicationUser user in users)
            {
                UserRolesViewModel urv = new UserRolesViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = await _userManager.GetRolesAsync(user)
                };

                Model.Add(urv);
            }
            return Page();
        }

        public async Task<IActionResult> OnDeleteAsync(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            user.IsEnabled = false;
            
            return Page();
        }
    }
}
