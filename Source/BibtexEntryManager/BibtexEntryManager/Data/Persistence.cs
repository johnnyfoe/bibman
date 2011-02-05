using System.Collections.Generic;
using System.Linq;
using BibtexEntryManager.Models;
using BibtexEntryManager.Models.EntryTypes;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;

namespace BibtexEntryManager.Data
{
    public static class DataPersistance
    {
        private static Configuration _config;

        public const string ConnString =
            "Data Source=(local);Initial Catalog=Bibtex;Integrated Security=True;Pooling=False;";

        private static SessionSource SessionSource { get; set; }

        private static ISessionFactory TheSessionFactory { get; set; }

        public static ISession GetSession()
        {
            return SessionSource.CreateSession();
        }

        public static IList<Publication> GetAllPublications()
        {
            var currentSession = GetSession();
            var a = from article in currentSession.Linq<Publication>()
                    select article;
            return a.ToList();
        }

        public static IList<Publication> GetAllPublicationsMatching(string s)
        {
            var currentSession = GetSession();

            var a = from pub in currentSession.Linq<Publication>()
                    where pub.Address.Contains(s) ||
                          pub.Annote.Contains(s) ||
                          pub.Authors.Contains(s) ||
                          pub.Booktitle.Contains(s) ||
                          pub.Chapter.Contains(s) ||
                          pub.Crossref.Contains(s) ||
                          pub.Edition.Contains(s) ||
                          pub.Editors.Contains(s) ||
                          pub.Howpublished.Contains(s) ||
                          pub.Institution.Contains(s) ||
                          pub.Journal.Contains(s) ||
                          pub.TheKey.Contains(s) ||
                          pub.Month.Contains(s) ||
                          pub.Note.Contains(s) ||
                          pub.Number.Contains(s) ||
                          pub.Organization.Contains(s) ||
                          pub.Pages.Contains(s) ||
                          pub.Publisher.Contains(s) ||
                          pub.School.Contains(s) ||
                          pub.Series.Contains(s) ||
                          pub.Title.Contains(s) ||
                          pub.Type.Contains(s) ||
                          pub.Volume.Contains(s) ||
                          pub.Year.Contains(s)
                    select pub;

            return a.ToList();
        }


        public static void Prepare()
        {
            TheSessionFactory = CreateSessionFactory();
            SetupConfig();
            var x = _config.Properties;
            SessionSource = new SessionSource(x, new BibPersistenceModel());
        }

        private static void SetupConfig()
        {
            if (_config == null)
                _config = Fluently.Configure().Database(
                        MsSqlConfiguration.MsSql2008.ConnectionString(
                            c => c.Is(ConnString)))
                            .BuildConfiguration();
        }

        private static ISessionFactory CreateSessionFactory()
        {
            var d = MsSqlConfiguration.MsSql2008.ConnectionString(c => c.Is(ConnString));

            var p = new AutoPersistenceModel();
            p.FindMapping<Publication>();

            return Fluently.Configure()
               .Database(d)
               .Mappings(m => m.AutoMappings.Add(p))
               .BuildSessionFactory();
        }
    }
}