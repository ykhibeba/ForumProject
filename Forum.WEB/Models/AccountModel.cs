using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.WEB.Models
{
    /// <summary>
    /// Post model for register user
    /// </summary>
    public class AccountModel
    {
        /// <summary>
        /// User name
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// User first name
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// User first name
        /// </summary>
        [Required]
        public string LastName { get; set; }
    }

    /// <summary>
    /// Information about user.
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// User name
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// First name.
        /// </summary>
        public string firstname { get; set; }

        /// <summary>
        /// Last name.
        /// </summary>
        public string lastname { get; set; }
    }
}