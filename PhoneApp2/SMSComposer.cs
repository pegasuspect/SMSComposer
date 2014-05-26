using Microsoft.Phone.UserData;
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
    public class SMSComposer : INotifyPropertyChanged
    {
        private const string FileKey = "ApplicationData.txt";
        public static Dictionary<string, string> COUNTRY_CODES =
            new Dictionary<string, string>() { { "Abkhazia", "+995" }, { "Afghanistan", "+93" }, { "Albania", "+355" }, { "Algeria", "+213" }, { "American Samoa", "+1684" }, { "Andorra", "+376" }, { "Angola", "+244" }, { "Anguilla", "+1264" }, { "Antigua and Barbuda", "+1268" }, { "Argentina", "+54" }, { "Armenia", "+374" }, { "Aruba", "+297" }, { "Ascension", "+247" }, { "Australia", "+61" }, { "Australian External Territories", "+672" }, { "Austria", "+43" }, { "Azerbaijan", "+994" }, { "Bahamas", "+1242" }, { "Bahrain", "+973" }, { "Bangladesh", "+880" }, { "Barbados", "+1246" }, { "Barbuda", "+1268" }, { "Belarus", "+375" }, { "Belgium", "+32" }, { "Belize", "+501" }, { "Benin", "+229" }, { "Bermuda", "+1441" }, { "Bhutan", "+975" }, { "Bolivia", "+591" }, { "Bonaire", "+5997" }, { "Bosnia and Herzegovina", "+387" }, { "Botswana", "+267" }, { "Brazil", "+55" }, { "British Indian Ocean Territory", "+246" }, { "British Virgin Islands", "+1284" }, { "Brunei Darussalam", "+673" }, { "Bulgaria", "+359" }, { "Burkina Faso", "+226" }, { "Burma", "+95" }, { "Burundi", "+257" }, { "Cambodia", "+855" }, { "Cameroon", "+237" }, { "Canada", "+1" }, { "Cape Verde", "+238" }, { "Caribbean Netherlands", "+599" }, { "Cayman Islands", "+1" }, { "Central African Republic", "+236" }, { "Chad", "+235" }, { "Chatham Island, New Zealand", "+64" }, { "Chile", "+56" }, { "China", "+86" }, { "Christmas Island", "+61" }, { "Cocos (Keeling) Islands", "+61" }, { "Colombia", "+57" }, { "Comoros", "+269" }, { "Congo", "+242" }, { "Congo, Democratic Republic of the (Zaire)", "+243" }, { "Cook Islands", "+682" }, { "Costa Rica", "+506" }, { "Croatia", "+385" }, { "Cuba", "+53" }, { "Curaçao", "+599" }, { "Cyprus", "+357" }, { "Czech Republic", "+420" }, { "Côte d'Ivoire", "+225" }, { "Denmark", "+45" }, { "Diego Garcia", "+246" }, { "Djibouti", "+253" }, { "Dominica", "+1" }, { "Dominican Republic", "+1" }, { "East Timor", "+670" }, { "Easter Island", "+56" }, { "Ecuador", "+593" }, { "Egypt", "+20" }, { "El Salvador", "+503" }, { "Ellipso (Mobile Satellite service)", "+881" }, { "EMSAT (Mobile Satellite service)", "+882" }, { "Equatorial Guinea", "+240" }, { "Eritrea", "+291" }, { "Estonia", "+372" }, { "Ethiopia", "+251" }, { "Falkland Islands", "+500" }, { "Faroe Islands", "+298" }, { "Fiji", "+679" }, { "Finland", "+358" }, { "France", "+33" }, { "French Antilles", "+596" }, { "French Guiana", "+594" }, { "French Polynesia", "+689" }, { "Gabon", "+241" }, { "Gambia", "+220" }, { "Georgia", "+995" }, { "Germany", "+49" }, { "Ghana", "+233" }, { "Gibraltar", "+350" }, { "Global Mobile Satellite System (GMSS)", "+881" }, { "Globalstar (Mobile Satellite Service)", "+881" }, { "Greece", "+30" }, { "Greenland", "+299" }, { "Grenada", "+1" }, { "Guadeloupe", "+590" }, { "Guam", "+1" }, { "Guantanamo Bay, Cuba", "+53" }, { "Guatemala", "+502" }, { "Guernsey", "+44" }, { "Guinea", "+224" }, { "Guinea-Bissau", "+245" }, { "Guyana", "+592" }, { "Haiti", "+509" }, { "Honduras", "+504" }, { "Hong Kong", "+852" }, { "Hungary", "+36" }, { "Iceland", "+354" }, { "ICO Global (Mobile Satellite Service)", "+881" }, { "India", "+91" }, { "Indonesia", "+62" }, { "Inmarsat SNAC", "+870" }, { "International Freephone Service", "+800" }, { "International Shared Cost Service (ISCS)", "+808" }, { "Iran", "+98" }, { "Iraq", "+964" }, { "Ireland", "+353" }, { "Iridium (Mobile Satellite service)", "+881" }, { "Isle of Man", "+44" }, { "Israel", "+972" }, { "Italy", "+39" }, { "Jamaica", "+1" }, { "Jan Mayen", "+47" }, { "Japan", "+81" }, { "Jersey", "+44" }, { "Jordan", "+962" }, { "Kazakhstan", "+7" }, { "Kenya", "+254" }, { "Kiribati", "+686" }, { "Korea, North", "+850" }, { "Korea, South", "+82" }, { "Kuwait", "+965" }, { "Kyrgyzstan", "+996" }, { "Laos", "+856" }, { "Latvia", "+371" }, { "Lebanon", "+961" }, { "Lesotho", "+266" }, { "Liberia", "+231" }, { "Libya", "+218" }, { "Liechtenstein", "+423" }, { "Lithuania", "+370" }, { "Luxembourg", "+352" }, { "Macau", "+853" }, { "Macedonia", "+389" }, { "Madagascar", "+261" }, { "Malawi", "+265" }, { "Malaysia", "+60" }, { "Maldives", "+960" }, { "Mali", "+223" }, { "Malta", "+356" }, { "Marshall Islands", "+692" }, { "Martinique", "+596" }, { "Mauritania", "+222" }, { "Mauritius", "+230" }, { "Mayotte", "+262" }, { "Mexico", "+52" }, { "Micronesia, Federated States of", "+691" }, { "Midway Island, USA", "+1" }, { "Moldova", "+373" }, { "Monaco", "+377" }, { "Mongolia", "+976" }, { "Montenegro", "+382" }, { "Montserrat", "+1" }, { "Morocco", "+212" }, { "Mozambique", "+258" }, { "Namibia", "+264" }, { "Nauru", "+674" }, { "Nepal", "+977" }, { "Netherlands", "+31" }, { "Nevis", "+1" }, { "New Caledonia", "+687" }, { "New Zealand", "+64" }, { "Nicaragua", "+505" }, { "Niger", "+227" }, { "Nigeria", "+234" }, { "Niue", "+683" }, { "Norfolk Island", "+672" }, { "Northern Mariana Islands", "+1" }, { "Norway", "+47" }, { "Oman", "+968" }, { "Pakistan", "+92" }, { "Palau", "+680" }, { "Palestinian territories", "+970" }, { "Panama", "+507" }, { "Papua New Guinea", "+675" }, { "Paraguay", "+595" }, { "Peru", "+51" }, { "Philippines", "+63" }, { "Pitcairn Islands", "+64" }, { "Poland", "+48" }, { "Portugal", "+351" }, { "Puerto Rico", "+1" }, { "Qatar", "+974" }, { "Romania", "+40" }, { "Russia", "+7" }, { "Rwanda", "+250" }, { "Réunion", "+262" }, { "Saba", "+599" }, { "Saint Barthélemy", "+590" }, { "Saint Helena", "+290" }, { "Saint Kitts and Nevis", "+1" }, { "Saint Lucia", "+1" }, { "Saint Martin (France)", "+590" }, { "Saint Pierre and Miquelon", "+508" }, { "Saint Vincent and the Grenadines", "+1" }, { "Samoa", "+685" }, { "San Marino", "+378" }, { "Saudi Arabia", "+966" }, { "Senegal", "+221" }, { "Serbia", "+381" }, { "Seychelles", "+248" }, { "Sierra Leone", "+232" }, { "Singapore", "+65" }, { "Sint Eustatius", "+599" }, { "Sint Maarten (Netherlands)", "+1" }, { "Slovakia", "+421" }, { "Slovenia", "+386" }, { "Solomon Islands", "+677" }, { "Somalia", "+252" }, { "South Africa", "+27" }, { "South Georgia and the South Sandwich Islands", "+500" }, { "South Ossetia", "+995" }, { "South Sudan", "+211" }, { "Spain", "+34" }, { "Sri Lanka", "+94" }, { "Sudan", "+249" }, { "Suriname", "+597" }, { "Svalbard", "+47" }, { "Swaziland", "+268" }, { "Sweden", "+46" }, { "Switzerland", "+41" }, { "Syria", "+963" }, { "São Tomé and Príncipe", "+239" }, { "Taiwan", "+886" }, { "Tajikistan", "+992" }, { "Tanzania", "+255" }, { "Thailand", "+66" }, { "Thuraya (Mobile Satellite service)", "+882" }, { "Togo", "+228" }, { "Tokelau", "+690" }, { "Tonga", "+676" }, { "Trinidad and Tobago", "+1" }, { "Tristan da Cunha", "+290" }, { "Tunisia", "+216" }, { "Turkey", "+90" }, { "Turkmenistan", "+993" }, { "Turks and Caicos Islands", "+1" }, { "Tuvalu", "+688" }, { "Uganda", "+256" }, { "Ukraine", "+380" }, { "United Arab Emirates", "+971" }, { "United Kingdom", "+44" }, { "United States", "+1" }, { "Universal Personal Telecommunications (UPT)", "+878" }, { "Uruguay", "+598" }, { "US Virgin Islands", "+1" }, { "Uzbekistan", "+998" }, { "Vanuatu", "+678" }, { "Vatican City State (Holy See)", "+3906698" }, { "Venezuela", "+58" }, { "Vietnam", "+84" }, { "Wake Island, USA", "+1" }, { "Wallis and Futuna", "+681" }, { "Yemen", "+967" }, { "Zambia", "+260" }, { "Zanzibar", "+255" }, { "Zimbabwe", "+263" }, { "Åland Islands", "+358" } };
        private Group phoneContactsField;
        private List<Group> groupListField;
        private string countryCodeField;
        private string signatureField;
        private List<TemplateCatagory> templateListField;

        public void Reset()
        {
            this.Signature = this.CountryCode = string.Empty;
            this.TemplateList = null;
            this.GroupList = null;
        }

        public List<Group> GroupList
        {
            get
            {
                if (this.groupListField == null)
                {
                    this.groupListField = new List<Group>();
                }
                return this.groupListField;
            }
            set
            {
                this.groupListField = value;
                OnPropertyChanged("GroupList");
            }
        }
        public List<TemplateCatagory> TemplateList
        {
            get
            {
                if (this.templateListField == null)
                {
                    this.templateListField = new List<TemplateCatagory>();
                }
                return this.templateListField;
            }
            set
            {
                this.templateListField = value;
                OnPropertyChanged("TemplateList");
            }
        }
        public string CountryCode
        {
            get
            {
                if (this.countryCodeField == null)
                {
                    string region = System.Globalization.RegionInfo.CurrentRegion.NativeName;
                    string key = SMSComposer.COUNTRY_CODES.Keys.Where(a => a.Contains(region)).FirstOrDefault();
                    this.countryCodeField = SMSComposer.COUNTRY_CODES[key];
                }
                return this.countryCodeField;
            }
            set
            {
                this.countryCodeField = value;
                OnPropertyChanged("CountryCode");
            }
        }
        public string Signature
        {
            get
            {
                if (signatureField == null)
                {
                    this.signatureField = "";
                }
                return this.signatureField;
            }
            set
            {
                this.signatureField = value;
                OnPropertyChanged("Signature");
            }
        }
        public Group PHONE_CONTACTS
        {
            get
            {
                if (this.phoneContactsField == null)
                {
                    this.phoneContactsField = new Group();
                }
                return this.phoneContactsField;
            }
        }


        public void Save()
        {
            IsolatedStorageFileStream fs = IsolatedStorageFile.GetUserStoreForApplication().OpenFile(FileKey, System.IO.FileMode.Truncate);
            byte[] fileInput = GetBytes(JsonConvert.SerializeObject(this));
            fs.Write(fileInput, 0, fileInput.Length);
            fs.Close();
        }
        public SMSComposer Read()
        {
            IsolatedStorageFileStream fs = IsolatedStorageFile.GetUserStoreForApplication().OpenFile(FileKey, System.IO.FileMode.OpenOrCreate);
            byte[] fileOutput = new byte[fs.Length];
            fs.Read(fileOutput, 0, fileOutput.Length);
            fs.Close();
            string result = string.Copy(GetString(fileOutput));
            SMSComposer smscmp = (SMSComposer)JsonConvert.DeserializeObject(result, typeof(SMSComposer));
            return smscmp == null ? new SMSComposer() : smscmp;
        }
        private byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
        private string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            string result = new string(chars);
            return result;
        }
        public void GetPhoneContactsAndSave()
        {
            Contacts cons = new Contacts();

            //Identify the method that runs after the asynchronous search completes.
            cons.SearchCompleted += new EventHandler<ContactsSearchEventArgs>(Contacts_SearchCompleted);

            //Start the asynchronous search.
            cons.SearchAsync(String.Empty, FilterKind.None, "Getting all contacts!");
        }
        private void Contacts_SearchCompleted(object sender, ContactsSearchEventArgs e)
        {
            phoneContactsField = new Group();
            List<Contact> res =
            (from Contact con in e.Results
             where con.PhoneNumbers.Count() == 1
             select con).ToList();

            foreach (Contact contact in res)
            {
                phoneContactsField.ContactsList.Add(new MyContact()
                {
                    DisplayName = contact.DisplayName,
                    PhoneNumber = contact.PhoneNumbers.FirstOrDefault().PhoneNumber.Trim('-', '(', ')', ' '),
                    PhoneNumberType = contact.PhoneNumbers.FirstOrDefault().Kind.ToString(),
                    Checked = false
                });
            }

            res =
            (from Contact con in e.Results
             where con.PhoneNumbers.Count() > 1
             select con).ToList();

            foreach (Contact contact in res)
            {
                foreach (ContactPhoneNumber contactnumber in contact.PhoneNumbers)
                {
                    phoneContactsField.ContactsList.Add(new MyContact()
                    {
                        DisplayName = contact.DisplayName,
                        PhoneNumber = contactnumber.PhoneNumber,
                        PhoneNumberType = contactnumber.Kind.ToString(),
                        Checked = false
                    });
                }
            }

            phoneContactsField.ContactsList =
                phoneContactsField.ContactsList.OrderBy(a => a.DisplayName).ToList();

            Save();
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
}
