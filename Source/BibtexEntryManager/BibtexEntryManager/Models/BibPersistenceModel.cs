using BibtexEntryManager.Models.EntryTypes;
using FluentNHibernate;

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
}
