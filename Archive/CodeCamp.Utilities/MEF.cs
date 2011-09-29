using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Xml.Linq;
using System.Configuration;

namespace CodeCamp.Utilities.MEF
{

    public class ConfigExportProvider : ExportProvider
    {
        #region methods
        public ConfigExportProvider()
        {
            MEFConfigurationSection _MEFConfigurationSection = ConfigurationManager.GetSection("MEF") as MEFConfigurationSection;
            if (_MEFConfigurationSection == null)
                throw new Exception("MEF configuration missing in web/app.config");
            mappings = _MEFConfigurationSection.Exports.Cast<MEFExportItem>().ToDictionary(x => x.Contract, y=>y);
        }

        private object[] CreateParameters(string aParameterString)
        {
            if (string.IsNullOrEmpty(aParameterString))
            {
                return null;
            }
            //stupid approach, better would be to extens the MEFExportItem to add a list of parameters
            //for now we say that parameters are separated by a , and if any conatins a : than its a list
            //if it contains a ; than its a dictionary
            string[] _Parameters = aParameterString.Split(',');
            List<object> _ParameterList = new List<object>();
            foreach (string _Parameter in _Parameters)
            {
                if (_Parameter.IndexOf(';') != -1)
                {
                    //its a dictionary of key:value;
                    Dictionary<string, string> _Dictionary = _Parameter.Split(':').ToDictionary(key => key.Split(';')[0], value => value.Split(';')[1]);
                    _ParameterList.Add(_Dictionary);
                    continue;
                }
                if (_Parameter.IndexOf(':') != -1)
                {
                    //its a list separated by :
                    List<string> _List = _Parameter.Split(':').ToList();
                    _ParameterList.Add(_List);
                    continue;
                }
                _ParameterList.Add(_Parameter);
            }
            return _ParameterList.ToArray();
        }

        protected override IEnumerable<Export> GetExportsCore(ImportDefinition aDefinition, AtomicComposition aAtomicComposition)
        {
            List<Export> _Exports = new List<Export>();
            MEFExportItem _MEFExportItem;
            if (mappings.TryGetValue(aDefinition.ContractName, out _MEFExportItem))
            {
                Type _Type = Type.GetType(_MEFExportItem.Type);
                object[] _Parameters = CreateParameters(_MEFExportItem.Arguments);
                Type[] _ParameterTypes = _Parameters.Select(x => x.GetType()).ToArray();
                object _Instance = _Type.GetConstructor(_ParameterTypes).Invoke(_Parameters);
                ExportDefinition _ExportDefintion = new ExportDefinition(aDefinition.ContractName, new Dictionary<string, object>());
                Export _NewExport = new Export(_ExportDefintion, () => _Instance);
                _Exports.Add(_NewExport);
            }
            return _Exports;
        }
        #endregion

        #region fields
        private Dictionary<string, MEFExportItem> mappings;
        #endregion
    }

    public class MEFConfigurationSection : ConfigurationSection
    {
		#region Constructors
        static MEFConfigurationSection()
		{
            exports = new ConfigurationProperty(
				"",
                typeof(MEFExportItemCollection),
				null,
				ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsDefaultCollection
				);

            properties = new ConfigurationPropertyCollection();

            properties.Add(exports);
		}
		#endregion

		#region Fields
		private static ConfigurationPropertyCollection properties;
		private static ConfigurationProperty exports;
		#endregion

		#region Properties

        public MEFExportItemCollection Exports
		{
            get { return (MEFExportItemCollection)base[exports]; }
		}

		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
                return properties;
			}
		}
		#endregion

    }

    public class MEFExportItem : ConfigurationElement
    {
		#region Constructors
        static MEFExportItem()
		{
			contract = new ConfigurationProperty(
				"contract",
				typeof(string),
				null,
				ConfigurationPropertyOptions.IsRequired
				);

			type = new ConfigurationProperty(
				"type",
				typeof(string),
				null,
				ConfigurationPropertyOptions.None
				);

			arguments = new ConfigurationProperty(
				"arguments",
				typeof(string),
				null,
				ConfigurationPropertyOptions.None
				);

			properties = new ConfigurationPropertyCollection();

			properties.Add(contract);
			properties.Add(type);
			properties.Add(arguments);
		}
		#endregion

		#region Fields
		private static ConfigurationPropertyCollection properties;
		private static ConfigurationProperty contract;
		private static ConfigurationProperty type;
		private static ConfigurationProperty arguments;
		#endregion

		#region Properties
		public string Arguments
		{
			get { return (string)base[arguments]; }
			set { base[arguments] = value; }
		}

		public string Contract
		{
			get { return (string)base[contract]; }
			set { base[contract] = value; }
		}

		public string Type
		{
			get { return (string)base[type]; }
			set { base[type] = value; }
		}

		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return properties;
			}
		}
		#endregion
    }

    [ConfigurationCollection(typeof(MEFExportItem), AddItemName = "Export", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class MEFExportItemCollection : ConfigurationElementCollection
    {
		#region Constructor
        public MEFExportItemCollection()
		{
		}
		#endregion

		#region Properties
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}
		protected override string ElementName
		{
			get
			{
				return "Export";
			}
		}

		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return new ConfigurationPropertyCollection();
			}
		}
		#endregion

		#region Indexers
        public MEFExportItem this[int index]
		{
			get
			{
				return (MEFExportItem)base.BaseGet(index);
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				base.BaseAdd(index, value);
			}
		}

        public MEFExportItem this[string name]
		{
			get
			{
                return (MEFExportItem)base.BaseGet(name);
			}
		}
		#endregion

		#region Methods
        public void Add(MEFExportItem item)
		{
			base.BaseAdd(item);
		}

        public void Remove(MEFExportItem item)
		{
			base.BaseRemove(item);
		}

		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
		#endregion

		#region Overrides
		protected override ConfigurationElement CreateNewElement()
		{
            return new MEFExportItem();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
            return (element as MEFExportItem).Contract;
		}
		#endregion
    }



}
