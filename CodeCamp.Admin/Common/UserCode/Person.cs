using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
namespace CodeCamp.Admin
{
    public partial class Person
    {
        partial void FirstName_Changed()
        {
            this.Name = this.FirstName + ' ' + this.LastName;
        }

        partial void LastName_Changed()
        {
            this.Name = this.FirstName + ' ' + this.LastName;
        }

    }
}
