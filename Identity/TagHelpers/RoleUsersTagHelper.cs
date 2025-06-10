using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace Identity.TagHelpers
{
    [HtmlTargetElement("td", Attributes = "asp-role-users")]
    public class RoleUsersTagHelper:TagHelper
    {
        private readonly RoleManager<AppRole> _roleMagager;
        private readonly UserManager<AppUser> _userMagager;
        public RoleUsersTagHelper(RoleManager<AppRole> roleMagager, UserManager<AppUser> userMagager)
        {
            _roleMagager = roleMagager;
            _userMagager = userMagager;
        }

        [HtmlAttributeName("asp-role-users")]
        public string RoleId { get; set; } = null!;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var userNames = new List<string>();

            var role = await _roleMagager.FindByIdAsync(RoleId);

            if(role != null && role.Name != null)
            {
                var users = await _userMagager.Users.ToListAsync();

                foreach (var user in users)
                {
                    if (await _userMagager.IsInRoleAsync(user, role.Name))
                    {
                        userNames.Add(user.UserName ?? "");
                    }
                }
            }
            output.Content.SetContent(userNames.Count == 0 ? "kullanıcı yok" : string.Join(" , ", userNames));
        }
    }
}
