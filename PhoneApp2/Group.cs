using Microsoft.Phone.UserData;
using PhoneApp2.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneApp2
{
    public partial class Group : INotifyPropertyChanged
    {
        private string GroupNameField;
        private ContactFilter FiltersField;
        private List<MyContact> ContactListField;

        public string GroupName
        {
            get
            {
                return this.GroupNameField;
            }
            set
            {
                this.GroupNameField = value;
                OnPropertyChanged("GroupName");
            }
        }
        public ContactFilter Filters
        {
            get
            {
                return this.FiltersField;
            }
            set
            {
                this.FiltersField = value;
                OnPropertyChanged("Filters");
            }
        }
        public List<MyContact> ContactsList
        {
            get
            {
                if(ContactListField != null)
                    return this.ContactListField;
                return this.ContactListField = new List<MyContact>();
            }
            set
            {
                this.ContactListField = value;
                OnPropertyChanged("PhoneContactsCollection");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }

    public partial class ContactFilter
    {
        private string StartsWithField;

        private string EndsWithField;

        private bool IsInternationalField;

        private string ContainsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string StartsWith
        {
            get
            {
                return this.StartsWithField;
            }
            set
            {
                this.StartsWithField = value;
                OnPropertyChanged("StartsWith");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string EndsWith
        {
            get
            {
                return this.EndsWithField;
            }
            set
            {
                this.EndsWithField = value;
                OnPropertyChanged("EndsWith");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool IsInternational
        {
            get
            {
                return this.IsInternationalField;
            }
            set
            {
                this.IsInternationalField = value;
                OnPropertyChanged("IsInternational");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Contains
        {
            get
            {
                return this.ContainsField;
            }
            set
            {
                this.ContainsField = value;
                OnPropertyChanged("Contains");
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
    }

    public partial class MyContact : INotifyPropertyChanged
    {

        private string phoneNumberField;

        private string displayNameField;

        private bool checkedField;

        private PhoneNumberKind PhoneNumberTypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PhoneNumber
        {
            get
            {
                return this.phoneNumberField;
            }
            set
            {
                this.phoneNumberField = value;
                OnPropertyChanged("PhoneNumber");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DisplayName
        {
            get
            {
                return this.displayNameField;
            }
            set
            {
                this.displayNameField = value;
                OnPropertyChanged("DisplayName");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool Checked
        {
            get
            {
                return this.checkedField;
            }
            set
            {
                this.checkedField = value;
                OnPropertyChanged("Checked");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PhoneNumberType
        {
            get
            {
                return this.PhoneNumberTypeField.ToString();
            }
            set
            {
                this.PhoneNumberTypeField = (PhoneNumberKind) Enum.Parse(typeof(PhoneNumberKind), value);
                OnPropertyChanged("PhoneNumberType");
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
    }
}
