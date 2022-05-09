using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.DataAccess.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Identity.DataAccess.EFDesign
{
    public class ContextFactoryWorkaround : IDesignTimeDbContextFactory<IdentityServerContext>
    {
        public IdentityServerContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IdentityServerContext>();
            optionsBuilder.UseSqlite("Data Source=IdentityServer.db");

            return new IdentityServerContext(optionsBuilder.Options);
        }
    }
}
