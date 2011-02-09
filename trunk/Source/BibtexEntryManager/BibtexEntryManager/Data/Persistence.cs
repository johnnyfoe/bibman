using System.Collections.Generic;
using System.Linq;
using BibtexEntryManager.Models;
using BibtexEntryManager.Models.EntryTypes;
using BibtexEntryManager.Models.Mapping;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using NHibernate.Tool.hbm2ddl;

namespace BibtexEntryManager.Data
{
    public static class DataPersistence
    {
        private static Configuration _config;

        public const string ConnString =
            "Data Source=(local);Initial Catalog=Bibtex;Integrated Security=True;Pooling=False;";

        private static ISessionFactory SessionFactory { get; set; }

        public static ISession GetSession()
        {
            return SessionFactory.OpenSession();
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
            _config = GetConfig().BuildConfiguration();
            SessionFactory = _config.BuildSessionFactory();
        }

        public static FluentConfiguration GetConfig()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                              .ConnectionString(c => c.Is(ConnString)).ShowSql)
                .Mappings(m => m.AutoMappings.Add(CreatePublicationAutomappings()));
            //.ExposeConfiguration(BuildSchema);
        }
        private static void BuildSchema(Configuration config)
        {
            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(config).Create(false, true);
        }


        static AutoPersistenceModel CreatePublicationAutomappings()
        {
            // This is the actual automapping - use AutoMap to start automapping,
            // then pick one of the static methods to specify what to map (in this case
            // all the classes in the assembly that contains Employee), and then either
            // use the Setup and Where methods to restrict that behaviour, or (preferably)
            // supply a configuration instance of your definition to control the automapper.
            return AutoMap.AssemblyOf<Publication>(new BibtexAutomappingConfiguration())
                .Conventions.Add<CascadeConvention>();
        }

    }
}