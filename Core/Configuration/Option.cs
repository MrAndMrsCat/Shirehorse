using System.Xml.Serialization;

namespace Shirehorse.Core.Configuration
{
    [Serializable()]
    public class Option
    {
        public string Description;

        [XmlIgnoreAttribute]
        public object Value
        {
            get
            {
                if (_value == null) _value = Default;
                return _value;
            }
            set
            {
                _value = value;
                Type = value.GetType().ToString();

                OnValueChanged(new ValueChangedEventArgs()
                {
                    Name = Name,
                    Value = value,
                    Save = true
                });
            }
        }
        private object _value;

        [XmlIgnoreAttribute]
        public object Default
        {
            get { return _default; }
            set
            {
                _default = value;
                Type = value.GetType().ToString();
            }
        }
        private object _default;




        public void Update(Option default_option)
        {
            Default = default_option.Default;
            Description = default_option.Description;
            DescriptionExtended = default_option.DescriptionExtended;
            Choices = default_option.Choices;
            UserCanAddChoice = default_option.UserCanAddChoice;
            UserControlWidth = default_option.UserControlWidth;
        }

        public bool TryGetTooltip(out string tooltip)
        {
            tooltip = "";

            if (DescriptionExtended != null)
                tooltip = $"{DescriptionExtended}\n";

            var def = Default.ToString();
            if (def != "")
                tooltip += $"Default: {def}";

            tooltip.Trim('\n');

            return tooltip != "";
        }


        // this is used by the xml serializer
        public object Value_Serialized
        {
            get
            {
                return Parser == null
                    ? Value
                    : Parser.Serialize(Value);
            }
            set
            {
                Value = Parser == null
                    ? value
                    : Parser.Deserialize(value.ToString());
            }
        }

        // this is used by the xml serializer
        public object Default_Value_Serialized
        {
            get
            {
                return Parser == null
                    ? Default
                    : Parser.Serialize(Default);
            }
            set
            {
                Default = Parser == null
                    ? value
                    : Parser.Deserialize(value.ToString());
            }
        }
        
        public string Type;
        public string Name;

        [XmlIgnoreAttribute]
        public CustomOption Parser
        {
            get
            {
                CustomOption.Instances.TryGetValue(Type, out CustomOption parser);
                return parser;
            }
        }

        [XmlIgnoreAttribute]
        public string DescriptionExtended;

        [XmlIgnoreAttribute]
        public List<object> Choices;

        [XmlIgnoreAttribute]
        public bool UserCanAddChoice;

        [XmlIgnoreAttribute]
        public int UserControlWidth;

        [XmlIgnoreAttribute]
        public bool Bool
        {
            get { return (bool)Value; }
            set { Value = value; }
        }
        [XmlIgnoreAttribute]
        public int Int
        {
            get { return (int)Value; }
            set { Value = value; }
        }
        [XmlIgnoreAttribute]
        public double Dbl
        {
            get { return (double)Value; }
            set { Value = value; }
        }
        [XmlIgnoreAttribute]
        public string Str
        {
            get { return Value.ToString(); }
            set { Value = value; }
        }

        internal void OnValueChanged(ValueChangedEventArgs e) { ValueChanged?.Invoke(this, e); }
        public event EventHandler<ValueChangedEventArgs> ValueChanged;
        public class ValueChangedEventArgs : EventArgs { public object Value; public string Name; public bool Save; }
    }




}

