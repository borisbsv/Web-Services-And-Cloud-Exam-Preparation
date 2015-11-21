using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BullsAndCows.Data;
using BullsAndCows.Data.Migrations;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BullsAndCows.Web.Api.Startup))]

namespace BullsAndCows.Web.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //Database.SetInitializer<BullsAndCowsDbContext>(new MigrateDatabaseToLatestVersion<BullsAndCowsDbContext, Configuration>());
            // Database.SetInitializer(new DropCreateDatabaseAlways<BullsAndCowsDbContext>());

            AutoMapperConfig.RegisterMappings("BullsAndCows.Web.Api");
            ConfigureAuth(app);
        }
    }
}
