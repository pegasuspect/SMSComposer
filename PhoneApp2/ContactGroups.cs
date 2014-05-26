using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneApp2
{
    public class ContactGroups : INotifyPropertyChanged
    {
        public const string ContactGroupsQueryKey = "ContactGroups";
        public ContactGroups()
        {
            PhoneContactGroupsCollectionField = GetGroups();
        }

        private List<Group> PhoneContactGroupsCollectionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute("PhoneContacts.Collection")]
        [System.Xml.Serialization.XmlArrayItemAttribute("Item", IsNullable = false)]
        public List<Group> PhoneContactGroupsCollection
        {
            get
            {
                return this.PhoneContactGroupsCollectionField;
            }
            set
            {
                this.PhoneContactGroupsCollectionField = value;
                OnPropertyChanged("PhoneContactGroupsCollection");
            }
        }


        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void AddItem(Group item) {
            this.PhoneContactGroupsCollection.Add(item);
            SaveGroups();
        }
        

        public void SaveGroups()
        {
            IsolatedStorageSettings.ApplicationSettings[ContactGroupsQueryKey] = JsonConvert.SerializeObject(this.PhoneContactGroupsCollection);
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        public List<Group> GetGroups()
        {
            string result = "";
            if (!IsolatedStorageSettings.ApplicationSettings.TryGetValue<string>(ContactGroupsQueryKey, out result))
                return new List<Group>();
            return JsonConvert.DeserializeObject<List<Group>>(result);
        }
    }
}
