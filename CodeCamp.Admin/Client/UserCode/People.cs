using System;
using System.Linq;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Framework.Client;
using Microsoft.LightSwitch.Presentation;

namespace CodeCamp.Admin
{
    public partial class People
    {
        partial void Button_Execute()
        {
            var password = Details.Properties.Password.Value;
            if (!string.IsNullOrEmpty(password))
            {
                this.PersonCollection.SelectedItem.PasswordHash = Encryption.ComputePasswordHash(password);
            }
            Details.Properties.Password.Value = string.Empty;
        }
    }
}
