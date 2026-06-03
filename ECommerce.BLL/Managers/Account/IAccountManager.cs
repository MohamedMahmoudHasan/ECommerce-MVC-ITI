using ECommerce.DAL;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BLL
{
    public interface IAccountManager
    {
        Task<ApplicationUser?> FindByEmailAsync(string email);
        Task<IdentityResult> RegisterAsync(RegisterMV model);
    }
}
