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
        protected ISession Session { get; private set; }
        protected Configuration Config;

        [SetUp]
        public void SetupContext()
        {
            Config = DataPersistence.GetConfig().BuildConfiguration();
            Session = Config.BuildSessionFactory().OpenSession();
        }

        [TearDown]
        public void TearDownContext()
        {
            Session.Close();
            Session.Dispose();
        }
    }
}
