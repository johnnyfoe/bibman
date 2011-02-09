using System;
using BibtexEntryManager.Data;
using BibtexEntryManager.Models.EntryTypes;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace BibtexEntryManager.Models
{
    public class BibPersistenceModel : PersistenceModel
    {
        public BibPersistenceModel()
        {
            AddMappingsFromAssembly(typeof(Publication).Assembly);


            //AddMappingsFromAssembly(typeof(PublicationGroupMapping).Assembly);
        }
    }

    public class BibPersistenceConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.Namespace == "BibtexEntryManager.Models.EntryTypes";
        }
    }

    public class PublicationMappingOverride : IAutoMappingOverride<Publication>
    {
        public void Override(AutoMapping<Publication> mapping)
        {
            mapping.HasMany(a => a.Authors).Cascade.AllDeleteOrphan();
            mapping.HasMany(a => a.Editors).Cascade.AllDeleteOrphan();
        }
    }
}
