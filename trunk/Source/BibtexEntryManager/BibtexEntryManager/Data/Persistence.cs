using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BibtexEntryManager.Models;
using BibtexEntryManager.Models.EntryTypes;
using BibtexEntryManager.Models.Exceptions;
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

        public static string ConnString =
            "Data Source=(local);Initial Catalog=Bibtex;Integrated Security=True;Pooling=False;";

        private static ISessionFactory SessionFactory { get; set; }

        public static ISession GetSession()
        {
            return SessionFactory.OpenSession();
        }

        public static IList<Publication> GetActivePublications()
        {
            var currentSession = GetSession();
            var a = from article in currentSession.Linq<Publication>()
                    where article.DeletionTime == null
                    select article;
            return a.ToList();
        }

        public static IList<Publication> GetDeletedPublications()
        {
            var currentSession = GetSession();
            var a = from r in currentSession.Linq<Publication>()
                    where r.DeletionTime != null
                    select r;
            return a.ToList();
        }

        //public static IList<Publication> GetDuplicates()
        //{
        //    ISession session = GetSession();

        //    var a = ((from p in session.Linq<Publication>()
        //             group p by p.CiteKey
        //             into dups
        //             let count = dups.Count()
        //             where count > 1
        //             select dups).AsQueryable());



        //}

        public static IList<Publication> GetActivePublicationsMatching(string s)
        {
            s = PrepareSqlString(s);
            var currentSession = GetSession();

            var a = from pub in currentSession.Linq<Publication>()
                    where pub.DeletionTime == null &&
                          (pub.Address.Contains(s) ||
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
                          pub.Year.Contains(s))
                    select pub;

            return a.ToList();
        }

        public static string PrepareSqlString(string s)
        {
            // check for % signs and replace with \%
            s = Regex.Replace(s, "%", "\\%");
            // Check for * signs and replace with %
            s = Regex.Replace(s, "\\*", "%");
            // Check for _ signs and replace with \_
            s = Regex.Replace(s, "_", "\\_");
            // check for ? signs and replace with _
            s = Regex.Replace(s, "\\?", "_");
            return s;
        }

        public static void Prepare()
        {
            _config = GetConfig().BuildConfiguration();
            SessionFactory = _config.BuildSessionFactory();
        }

        public static FluentConfiguration GetConfig()
        {
            System.Configuration.Configuration rootWebConfig =
                System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/");
            System.Configuration.ConnectionStringSettings cs;

            if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
            {
                cs = rootWebConfig.ConnectionStrings.ConnectionStrings["BibtexSettings"];

                if (cs != null) // leaves ConnString as default unless the connection string is set
                    ConnString = cs.ConnectionString;
            }


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
        // todo figure out how to make a configurable time period
        /// <summary>
        /// Cleans up publications which have deletion times older than the specified time
        /// </summary>
        public static void CleanupExpiredDeletedPublications()
        {
            ISession ses = GetSession();
            ses.BeginTransaction();
            foreach (Publication pub in GetDeletedPublications())
            {
                TimeSpan? age = DateTime.Now - pub.DeletionTime;
                if (age != null)
                {
                    if (age.Value.TotalMilliseconds > (60*1000))
                    {
                        ses.Delete(pub);
                    }
                }
            }
            ses.Transaction.Commit();
        }

        public static void DeletePublication(int id)
        {
            var pub = (from p in (DataPersistence.GetSession().Linq<Publication>())
                       where p.Id == id
                       select p).First();
            if (pub == null)
            {
                throw new PublicationNotFoundException(id);
            }
            pub.DeletionTime = DateTime.Now;
            pub.SaveOrUpdateInDatabase();
        }

        public static void CleanupAllDeletedPublications()
        {
            ISession ses = GetSession();
            ses.BeginTransaction();
            foreach (Publication pub in GetDeletedPublications())
            {
                ses.Delete(pub);
            }
            ses.Transaction.Commit();
        }
    }
}