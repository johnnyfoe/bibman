using BibtexEntryManager.Data;
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
