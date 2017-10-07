﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace BreadPlayer.Helpers
{
    public class UserInfoHelper
    {
        private static async Task<User> GetCurrentUserAsync()
        {
            IReadOnlyList<User> users = await User.FindAllAsync();

            return users.Where(p => p.AuthenticationStatus == UserAuthenticationStatus.LocallyAuthenticated &&
                                         p.Type == UserType.LocalUser).FirstOrDefault();
        }
        public static async Task<string> GetUsernameAsync()
        {
            var current = await GetCurrentUserAsync();
            if (current == null)
                return DeviceInfoHelper.DeviceModel;
            // user may have username
            var data = await current.GetPropertyAsync(KnownUserProperties.AccountName);
            string displayName = (string)data;

            //or may be authinticated using hotmail 
            if (String.IsNullOrEmpty(displayName))
            {
                string a = (string)await current.GetPropertyAsync(KnownUserProperties.FirstName);
                string b = (string)await current.GetPropertyAsync(KnownUserProperties.LastName);
                displayName = string.Format("{0} {1}", a, b);
            }

            return displayName;
        }
    }
}
