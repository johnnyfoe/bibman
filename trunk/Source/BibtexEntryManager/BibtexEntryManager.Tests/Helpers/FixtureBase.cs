using System;
using System.Data.SqlClient;
using BibtexEntryManager.Data;
using BibtexEntryManager.Models;
using BibtexEntryManager.Models.EntryTypes;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NUnit.Framework;

namespace BibtexEntryManager.Tests.Helpers
{
    public class FixtureBase
    {
        protected SessionSource SessionSource { get; set; }
        protected ISession Session { get; private set; }
        protected Configuration Config;

        [SetUp]
        public void SetupContext()
        {
            Config = Fluently.Configure().Database(
                MsSqlConfiguration.MsSql2008.ConnectionString(
                    c => c.Is(DataPersistance.ConnString)))
                .BuildConfiguration();
            var x = Config.Properties;

            var p = new BibPersistenceModel();

            SessionSource = new SessionSource(x,p);
            Session = SessionSource.CreateSession();
            SessionSource.BuildSchema(Session);
        }

        [TearDown]
        public void TearDownContext()
        {
            Session.Close();
            Session.Dispose();
        }
    }
}
