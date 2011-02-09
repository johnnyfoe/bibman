using System;
using BibtexEntryManager.Models.EntryTypes;
using FluentNHibernate.Automapping;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Mapping;

namespace BibtexEntryManager.Models.Mapping
{
    public class PublicationMapping : ClassMap<Publication>
    {
        public PublicationMapping()
        {
            Not.LazyLoad();
            // Core fields
            Id(c => c.Id).GeneratedBy.Identity();
            Map(c => c.CiteKey).Not.Nullable().Length(Helpers.FieldLength.CiteKeyLength);
            Map(c => c.Owner).Not.Nullable().Length(Helpers.FieldLength.OwnerLength);
            Map(c => c.EntryType).Not.Nullable();
            Map(c => c.Abstract).Nullable().Length(Helpers.FieldLength.AbstractLength);
            // String fields
            Map(c => c.Address).Nullable().Length(Helpers.FieldLength.CiteKeyLength);
            Map(c => c.Annote).Nullable().Length(Helpers.FieldLength.CiteKeyLength);
            Map(c => c.Booktitle).Nullable().Length(Helpers.FieldLength.CiteKeyLength);
            Map(c => c.Chapter).Nullable().Length(Helpers.FieldLength.CiteKeyLength);
            Map(c => c.Crossref).Nullable().Length(Helpers.FieldLength.CiteKeyLength);
            Map(c => c.Edition).Nullable().Length(Helpers.FieldLength.CiteKeyLength);
            Map(c => c.Howpublished).Nullable().Length(Helpers.FieldLength.CiteKeyLength);
            Map(c => c.Institution).Nullable().Length(Helpers.FieldLength.CiteKeyLength);
            Map(c => c.Journal).Nullable().Length(Helpers.FieldLength.CiteKeyLength);
            Map(c => c.TheKey).Nullable().Length(Helpers.FieldLength.CiteKeyLength);
            Map(c => c.Month).Nullable().Length(Helpers.FieldLength.CiteKeyLength);
            Map(c => c.Note).Nullable().Length(Helpers.FieldLength.CiteKeyLength);
            Map(c => c.Number).Nullable().Length(Helpers.FieldLength.CiteKeyLength);
            Map(c => c.Organization).Nullable().Length(Helpers.FieldLength.CiteKeyLength);
            Map(c => c.Pages).Nullable().Length(Helpers.FieldLength.CiteKeyLength);
            Map(c => c.Publisher).Nullable().Length(Helpers.FieldLength.CiteKeyLength);
            Map(c => c.School).Nullable().Length(Helpers.FieldLength.CiteKeyLength);
            Map(c => c.Series).Nullable().Length(Helpers.FieldLength.CiteKeyLength);
            Map(c => c.Title).Nullable().Length(Helpers.FieldLength.CiteKeyLength);
            Map(c => c.Type).Nullable().Length(Helpers.FieldLength.CiteKeyLength);
            Map(c => c.Volume).Nullable().Length(Helpers.FieldLength.CiteKeyLength);
            Map(c => c.Year).Nullable().Length(Helpers.FieldLength.CiteKeyLength);

            // List fields
            HasManyToMany(c => c.Authors).AsList(a => a.Column("authorNameId")).Element("author");
            HasMany(c => c.Editors).AsList(a => a.Column("editorNameId")).Element("editor").Cascade.All();

            
        }
    }

    public class BibtexAutomappingConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.Namespace == "BibtexEntryManager.Models.EntryTypes";
        }
    }

    public class CascadeConvention : IReferenceConvention, IHasManyConvention, IHasManyToManyConvention
    {
        public void Apply(IManyToOneInstance instance)
        {
            instance.Cascade.All();
        }

        public void Apply(IOneToManyCollectionInstance instance)
        {
            instance.Cascade.All();
        }

        public void Apply(IManyToManyCollectionInstance instance)
        {
            instance.Cascade.All();
        }
    }
}