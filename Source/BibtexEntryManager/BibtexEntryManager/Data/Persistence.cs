﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using BibtexEntryManager.Models.EntryTypes;
using BibtexEntryManager.Models.Exceptions;
using BibtexEntryManager.Models.Mapping;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;

namespace BibtexEntryManager.Data
{
    public static class DataPersistence
    {
        private static Configuration _config;

        private static string _connString =
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
                    orderby article.CiteKey
                    where article.DeletionTime == null
                    select article;
            return a.ToList();
        }

        public static IList<Publication> GetDeletedPublications()
        {
            var currentSession = GetSession();
            var a = from r in currentSession.Linq<Publication>()
                    orderby r.CiteKey
                    where r.DeletionTime != null
                    select r;
            return a.ToList();
        }

        public static IList<Publication> GetActivePublicationsMatching(string s)
        {
            // prepare the SQL string - escape SQL wildcards 
            //         and replace * and ? characters
            s = PrepareSqlString(s);
            
            // Get a session with the database
            var currentSession = GetSession();

            // Put together the search query in LINQ and execute it
            var a = from pub in currentSession.Linq<Publication>()
                    where pub.DeletionTime == null &&
                          (pub.CiteKey.Contains(s) ||
                          pub.Address.Contains(s) ||
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

            // convert the results to a list and return them
            return a.ToList();
        }

        private static string PrepareSqlString(string s)
        {
            // escapes SQL wildcards and inserts SQL wildcards in
            // place of characters * and ?

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
            System.Configuration.ConnectionStringSettings cs;
            System.Configuration.Configuration rootWebConfig = null;
            try
            {
                rootWebConfig = WebConfigurationManager.OpenWebConfiguration("/");
            }
            catch (Exception)
            {
                
            }
            if (rootWebConfig != null && rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
            {
                cs = rootWebConfig.ConnectionStrings.ConnectionStrings["BibtexSettings"];

                if (cs != null) // leaves ConnString as default unless the connection string is set
                    _connString = cs.ConnectionString;
            }


            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                              .ConnectionString(c => c.Is(_connString)).ShowSql)
                .Mappings(m => m.AutoMappings.Add(CreatePublicationAutomappings()));
        }

        static AutoPersistenceModel CreatePublicationAutomappings()
        {
            return AutoMap.AssemblyOf<Publication>(new BibtexAutomappingConfiguration())
                .Conventions.Add<CascadeConvention>();
        }

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
            var pub = (from p in (GetSession().Linq<Publication>())
                       where p.Id == id
                       select p).First();
            if (pub == null)
            {
                throw new PublicationNotFoundException(id);
            }
            pub.DeletionTime = DateTime.Now;
            pub.SaveOrUpdateInDatabase();
        }

        public static void RestorePublication(int id)
        {
            var pub = (from p in (GetSession().Linq<Publication>())
                       where p.Id == id
                       select p).First();
            if (pub == null)
            {
                throw new PublicationNotFoundException(id);
            }
            pub.DeletionTime = null;
            pub.SaveOrUpdateInDatabase();
        }

        public static void DeletePublications(IEnumerable<Publication> publications)
        {
            ISession ses = GetSession();
            ses.BeginTransaction();
            foreach (Publication pub in publications)
            {
                pub.DeletionTime = DateTime.Now;
                ses.Update(pub);
            }
            ses.Transaction.Commit();
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

        public static IList<Publication> GetDuplicatePublications()
        {
            ISession ses = GetSession();

            var t = (from pub in ses.Linq<Publication>()
                     group pub by pub.CiteKey
                         into pubGroup
                         let count = pubGroup.Count()
                         where count > 1
                         select pubGroup.ToList());

            return (IList<Publication>)t.ToList();
        }

        public static IList<string> GetDuplicateCiteKeys()
        {
            ISession ses = GetSession();
            IList<string> retval = new List<string>();

            var activePublications = from pub in ses.Linq<Publication>()
                                     where pub.DeletionTime == null
                                     select pub;

            var t = (from pub in activePublications
                     group pub by pub.CiteKey
                     into pubGroup
                     where pubGroup.Count() > 1
                     select new {CK = pubGroup.Key, Count = pubGroup.Count()});

            



            foreach (var group in t)
            {
                if (group.Count > 1)
                    retval.Add(group.CK);
            }

            return retval;
        }

        public static IList<Publication> GetPublicationsWithCiteKey(string ck)
        {
            ISession ses = GetSession();

            var res = from pubs in ses.Linq<Publication>()
                      where pubs.CiteKey.Equals(ck) &&
                            pubs.DeletionTime == null
                      select pubs;

            return res.ToList();
        }
    }
}