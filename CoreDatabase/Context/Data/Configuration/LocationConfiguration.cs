using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreDatabase.Abstractions;

namespace CoreDatabase.Context.Data.Configuration
{
    public class LocationConfiguration : BusinessEntityTypeConfiguration<Location>
    {
        protected override void ConfigureDbColumnsFromProperties()
        {
        }

        protected override void RelateEntity()
        {
        }
    }
}
