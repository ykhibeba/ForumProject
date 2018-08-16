using Forum.WEB.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Forum.WEB.Controllers
{
    /// <summary>
    /// Controller for register user
    /// </summary>
    public class AccountController : ApiController
    {
        /// <summary>
        /// Register user into forum
        /// </summary>
        /// <param name="model">Account model</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("api/user/register")]
        [HttpPost]
        public IdentityResult Register(AccountModel model)
        {
            var userstore = new UserStore<ApplicationUser>(new ApplicationDbContext());

            var manager = new UserManager<ApplicationUser>(userstore);

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireDigit = true
            };

            IdentityResult result = manager.Create(user, model.Password);
            return result;
        }

        /// <summary>
        /// Get user info by token.
        /// </summary>
        /// <returns>UserInfo model: UserName, FullName</returns>
        [HttpGet]
        [Route("api/userinfo")]
        [Authorize]
        public UserInfo GetUserInfo()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> info = identityClaims.Claims;

            UserInfo model = new UserInfo()
            {
                UserName = identityClaims.FindFirst("Username").Value,
                FullName = String.Format($"{identityClaims.FindFirst("FirstName").Value} {identityClaims.FindFirst("LastName").Value} ")
            };

            return model;
        }
    }
}
