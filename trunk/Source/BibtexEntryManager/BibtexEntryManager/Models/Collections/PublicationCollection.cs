using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BibtexEntryManager.Models.EntryTypes;
using BibtexEntryManager.Models.Enums;
using Wintellect.PowerCollections;

namespace BibtexEntryManager.Models.Collections
{
    /**Publication Collection class maintains a collection of publication objects
     * This class follows Singleton pattern*/

    public class PublicationCollection
    {
        //DUPLICATE indicator string
        private const String DUPLICATE = "_DUP";

        //A Map of all the publication objects
        private Dictionary<String, Publication> publicationList;

        //A Set containing searchable Fields
        private Set<Field> searchFields;

        //A static PublicationCollection object 
        private static PublicationCollection pubCollection;

        // A reference to last inserted publication
        private Publication lastAddedPublication;

        // A map of duplicates
        private Dictionary<String, List<Publication>> dupMap;

        // A map of publications organised by citekey
        private Dictionary<String, List<Publication>> citeMap;

        // A reference to last deleted publication
        private Publication lastDeletedPublication;

        // Integer indicating progress for loading publications into the collection
        volatile private int loadProgress;

        private bool _hasChanged;

        /**Returns an instance of Publication Collection object
         * @return Publication Collection object*/
        public static PublicationCollection getInstance()
        {
            //Singleton pattern
            if (pubCollection == null)
            {
                pubCollection = new PublicationCollection();
            }
            return pubCollection;
        }

        //Constructs the PublicationCollection object
        private PublicationCollection()
        {
            //Instantiate map containing publications
            publicationList = new Dictionary<String, Publication>();

            // Instantiate cite map
            citeMap = new Dictionary<String, List<Publication>>();

            // Instantiate duplicate map
            dupMap = new Dictionary<String, List<Publication>>();

            //Instantiate searchFields
            searchFields = new Set<Field>();

            //Populate searchFields
            searchFields.Add(Field.Citekey);
            searchFields.Add(Field.Author);
            searchFields.Add(Field.Year);
            searchFields.Add(Field.Title);

            _hasChanged = false;
        }



        /**Adds all publications to the collection of publications 
         * this method runs on a separate thread
         * @param collection Collecition of publications to be added*/
        // no 'final' for incoming parameters - need to be careful not to 
        // overwrite it here.
        // todo reintroduce threading.
        public void bulkAddPublications(Collection<Publication> collection)
        {
            lock (typeof(PublicationCollection))
            {
                // Set the changed status of the object
                SetChanged();

                // Notify observers
                NotifyObservers(PublicationEvent.BULK_ADD_STARTED);

                // Counter variable
                Double i = 0.00;

                // Size
                var size = collection.Count;

                lock (publicationList)
                {
                    // todo not sure that this works - keep an eye on it for parallelism.
                    Parallel.ForEach(collection, j =>
                                                     {
                                                         loadProgress = ((int)(i++ / size * 100));
                                                         addSilentPublication(j);

                                                         SetChanged();

                                                         NotifyObservers(PublicationEvent.BULK_ADD_PROGRESS);
                                                     });

                } // End of publicationList lock

                SetChanged();

                NotifyObservers(PublicationEvent.BULK_ADD_FINISHED);
            }
        }

        /**Adds all publications to the collection of publications
         * @param collection Collecition of publications to be added*/
        public void addAllPublications(Collection<Publication> collection)
        {
            foreach (var p in collection)
            {
                addSilentPublication(p);
            }

            // TODO notify
        }

        /**Add a publication object
         * @param pub Publication object to be added*/
        public void addPublication(Publication pub)
        {
            lock (typeof(PublicationCollection))
            {
                //Add the publication
                if (addSilentPublication(pub))
                {
                    //Set the changed status of the object
                    SetChanged();

                    //Notify observers of change		
                    NotifyObservers(PublicationEvent.PUBLICATION_ADDED);
                }
            }
        }

        /**Adds a publication object without notifying the listener and returns boolean indicating success or failure of the operation
         * @param boolean indicating whether the operation was successful or not
         * false may also be returned if an identical publication already exists*/
        public bool addSilentPublication(Publication pub)
        {
            lock (typeof(PublicationCollection))
            {
                //Get the id for the publication
                String id = pub.GetValueForField(Field.Id).ToUpper();

                // Check if an identical publication already exists
                if (alreadyExists(pub))
                {
                    return false;
                }

                //Repeat while a publication with the id already exists
                while (publicationList.ContainsKey(id))
                {
                    //If id already exists then append DUPLICATE constant to it 
                    id += DUPLICATE;
                }

                pub.SetValueForField(Field.Id, id);

                // Adding a publication to the publication list
                publicationList.Add(id, pub);

                // Add to citemap and duplicate map
                addToCiteAndDupMap(pub);

                // Update the last inserted publication
                lastAddedPublication = pub;

                return true;
            }
        }

        /**Adds the publication to citemap and if it is a duplicate then add it to dupmap
         * @param pub Publication to be added*/
        private void addToCiteAndDupMap(Publication pub)
        {
            // Get the citekey
            String citekey = pub.GetValueForField(Field.Citekey).ToUpper();

            // Check if a pulication with citekey already exists 
            if (citeMap.ContainsKey(citekey))
            {
                // Get the list with the publications with the given citekey
                List<Publication> list = citeMap[citekey];

                // Add publication to the citekey
                list.Add(pub);

                // Add it to duplicate map as well because there are more than 1
                dupMap[citekey] = list;
            }
            // Publication with the given citekey does not exist
            else
            {
                // Create a new list
                List<Publication> list = new List<Publication>();

                // Add the publication to the list 
                list.Add(pub);

                // Add the list to citekey
                citeMap[citekey] = list;
            }
        }

        /**Removes the publication from the citemap and the dupmap
         * @param pub publication to be removed*/
        private void removeFromCiteAndDupMap(Publication pub)
        {
            String citekey = pub.GetValueForField(Field.Citekey).ToUpper();

            List<Publication> list = citeMap[citekey];

            foreach (var p in list)
            {
                if (p.IsIdentical(pub))
                {
                    list.Remove(p);
                }
            }

            if (list.Count == 0)
            {
                citeMap.Remove(citekey);
            }

            list = dupMap[citekey];
            if (list != null)
            {
                foreach (var p in list)
                {

                    if (p.IsIdentical(pub))
                    {
                        list.Remove(p);
                    }
                }

                if (list.Count == 1)
                {
                    dupMap.Remove(citekey);
                }
            }
        }

        /**Return true if and only if a publication identical to the given publication already exists in the list
         * @param pub Publication to be checked
         * @return boolean*/
        private bool alreadyExists(Publication pub)
        {
            String citekey = pub.GetValueForField(Field.Citekey).ToUpper();

            // Check if a publication with the citkey already exists
            if (citeMap.ContainsKey(citekey))
            {
                // Get the list of publication with the same citekey
                List<Publication> list = citeMap[citekey];

                // Iterate through the list of publications
                foreach (Publication p in list)
                {
                    // Check if the list contains identical publication
                    if (p.IsIdentical(pub))
                    {
                        return true;
                    }
                }
            }

            // No identical publications found
            return false;
        }


        /**Returns a publication object with the given id
         * @return Publication object*/
        public Publication getPublicationById(String id)
        {
            return publicationList[id];
        }

        /**Deletes all the given publication from the PublicationCollection
         * @param collection Collection of publications to be deleted*/
        public void deleteAllPublications(Collection<Publication> collection)
        {
            foreach (var pub in collection)
            {
                deleteSilentPublication(pub.GetValueForField(Field.Id));
            }

            // TODO notify
        }

        /**Deletes a publication object with the given citekey and return true if and only if the publication was found and deleted 
         * otherwise returns false if publication with the given citekey did not exist
         * @return Boolean*/
        public bool deletePublication(String id)
        {
            bool result = deleteSilentPublication(id);

            if (result)
            {
                SetChanged();

                NotifyObservers(PublicationEvent.PUBLICATION_DELETED);

                return true;
            }
            return false;
        }

        /**Deletes a publication object with the given citekey and return true if and only if the publication was found and deleted
         * this method does send delete notification 
         * otherwise returns false if publication with the given citekey did not exist
         * @return Boolean*/
        public bool deleteSilentPublication(String id)
        {
            //Get and Delete the publication from the map
            Publication pub = publicationList[id];
            publicationList.Remove(id);

            //Check if the publication existed
            if (pub != null)
            {
                // Update last deleted publication
                lastDeletedPublication = pub;

                // remove from cite and dupmap
                removeFromCiteAndDupMap(pub);

                //Return true
                return true;
            }
            //Publication did not exist so nothing to delete

            return false;
        }

        /**Returns a collection containing all publications
         * @return Collection of publications*/
        public ICollection<Publication> getAllPublications()
        {
            return publicationList.Values;
        }

        /**Removes all publications*/
        public void removeAll()
        {
            publicationList.Clear();
            citeMap.Clear();
            dupMap.Clear();

            // Set changed status
            SetChanged();

            // Notify observers
            NotifyObservers(PublicationEvent.PUBLICATION_DELETED);
        }

        /**Return if a given citekey is unique
         * @return True indicates citekey is unique and false indicates its not*/
        public bool isUnique(String citeKey)
        {
            //If the citekey is found then false should be returned that's why the exclamation sign
            return !(publicationList.ContainsKey(citeKey));
        }

        /**Returns a Map containing Publications such that the Key for each element of the Map is the citekey that is common to all the publication that are in the list corresponding to the key*/
        public Dictionary<String, List<Publication>> getDuplicates()
        {
            return dupMap;
        }


        /**Return the number of publications in the collection
         * @return number of publications*/
        public int getCount()
        {
            return publicationList.Count;
        }


        /**Return last added publication
         * @return publication*/
        public Publication getLastAddedPublication()
        {
            return lastAddedPublication;
        }

        /**Return last deleted publication
         * @return publication*/
        public Publication getLastDeletedPublication()
        {
            return lastDeletedPublication;
        }

        /**Returns int indicating the current progress of the Add All operation
         ** Method should be called when BULK_ADD_PROGRESS event occurs
         *@return int*/
        public int getLoadProgress()
        {
            return loadProgress;
        }

        public void Subscribe()
        {
            // todo add ability to subscribe
        }

        private void SetChanged()
        {
            _hasChanged = true;
        }

        private void NotifyObservers(PublicationEvent publicationEvent)
        {
            // todo broadcast to observers.
        }
    }
}