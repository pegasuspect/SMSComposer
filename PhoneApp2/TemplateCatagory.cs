using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneApp2
{
    public class TemplateCatagory
    {
        private string CatagoryNameField;
        private List<string> MessageListField;


        public List<string> MessageList
        {
            get
            {
                return this.MessageListField;
            }
            set
            {
                this.MessageListField = value;
            }
        }
        public string CatagoryName
        {
            get
            {
                return this.CatagoryNameField;
            }
            set
            {
                this.CatagoryNameField = value;
            }
        }

    }
}
