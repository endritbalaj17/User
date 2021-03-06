using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using User.Utils;

namespace User.Models
{
    public class UserModel
    {
	  public string Id { get; set; }

	  public string Role { get; set; }

	  public string Email { get; set; }

	  public string Username { get; set; }

	  public string PhoneNumber { get; set; }

	  public List<IdentityUser> Details { get; set; }
	}
}