using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace CodeCamp.ClassCreator
{
    public class EDMXtoClasses
    {
        public static void CreateClasses(string aEDMXFileURL, string aOutputFolderURL,string aNameSpace)
        {
            if (File.Exists(aEDMXFileURL) == false)
                throw new Exception("EDMXFile does not exist");
            if (Directory.Exists(aOutputFolderURL) == false)
            {
                Directory.CreateDirectory(aOutputFolderURL);
            }
            try
            {
                xDocument = XDocument.Load(aEDMXFileURL);
            }
            catch (Exception eL)
            {
                throw new Exception("EDMXFile is not a valid EDMX format,or cannot be read.Error message:" + eL.Message);
            }
            PopulateReservedWords();
            XName _EDMX = XName.Get("ConceptualModels", "http://schemas.microsoft.com/ado/2008/10/edmx");
            XName _Name = XName.Get("EntityType", "http://schemas.microsoft.com/ado/2008/09/edm");
            IEnumerable<XElement> _Classes = xDocument.Descendants(_EDMX).Descendants(_Name);
            //StringBuilder _EF4ContextClasses = new StringBuilder();
            StringBuilder _PartialClassExtentsions = new StringBuilder();
            foreach (XElement _Class in _Classes)
            {
                string _ClassName = _Class.Attribute("Name").Value;
                //_EF4ContextClasses.AppendFormat("public DbSet<{0}> {1} {{get;set;}}",_ClassName,_ClassName.EndsWith("s")?_ClassName:_ClassName + "s");
                //_EF4ContextClasses.AppendLine();
                _PartialClassExtentsions.AppendFormat("public partial {0}:BaseClass{{}}", _ClassName, _ClassName.EndsWith("s") ? _ClassName : _ClassName + "s");
                StringBuilder _SB = new StringBuilder();
                _SB.AppendLine(usingStatements);
                _SB.Append("namespace ");
                _SB.AppendLine(aNameSpace);
                _SB.AppendLine("{");
                _SB.Append("\tpublic partial class ");
                _SB.AppendLine(_ClassName);
                _SB.AppendLine("\t{");
                ProcessCTors(_SB, _ClassName);
                _SB.AppendLine();
                ProcessFields(_SB, _Class);
                _SB.AppendLine("\t}");
                _SB.AppendLine("}");
                File.WriteAllText(String.Format("{0}/{1}.cs", aOutputFolderURL, _ClassName), _SB.ToString());
            }
            File.WriteAllText(String.Format("{0}/PartialClasses.txt", aOutputFolderURL), _PartialClassExtentsions.ToString());
        }

        private static bool IsList(XElement aNavigationPropertyNode,out string aTypeName)
        {
            string[] _RelationShipName = aNavigationPropertyNode.Attribute("Relationship").Value.Split('.');
            XName _EDMX = XName.Get("ConceptualModels", "http://schemas.microsoft.com/ado/2008/10/edmx");
            XElement _RelationShipNode = xDocument.Descendants(_EDMX).Descendants().Where(x => x.Name.LocalName == "Association" && x.Attribute("Name").Value == _RelationShipName[_RelationShipName.Length - 1]).FirstOrDefault();
            if (_RelationShipNode == null)
            {
                throw new Exception("Canmnot find association for:" + aNavigationPropertyNode.Attribute("Relationship").Value);
            }
            string[] _FullTypeName = _RelationShipNode.Descendants().Where(x => x.Name.LocalName == "End").Skip(1).First().Attribute("Type").Value.Split('.');
            aTypeName = _FullTypeName[_FullTypeName.Length - 1];
            return _RelationShipNode.Descendants().Where(x => x.Name.LocalName == "End" && x.Attribute("Multiplicity").Value == "*").FirstOrDefault() != null;
        }

        private static void PopulateReservedWords()
        {
            reservedWords = new Dictionary<string, object>();
            reservedWords["event"] = new object();
        }

        private static void ProcessFields(StringBuilder aStringBuilder, XElement aClassElement)
        {
            XName _Name = XName.Get("Property", "http://schemas.microsoft.com/ado/2008/09/edm");
            IEnumerable<XElement> _Properties = aClassElement.Descendants().Where(x=>x.Name.LocalName=="Property" || x.Name.LocalName=="NavigationProperty");
            StringBuilder _SBProperties = new StringBuilder();
            StringBuilder _SBFields = new StringBuilder();
            foreach (XElement _Property in _Properties)
            {
                string _NetType = null;
                string _TypeName = null;
                string _PropertyName = _Property.Attribute("Name").Value;
                if (_PropertyName.ToLower().IndexOf("id") != -1 && _PropertyName.Length != 2)
                {
                    continue;
                }
                if (_Property.Name.LocalName == "NavigationProperty")
                {
                    if (IsList(_Property, out _TypeName))
                    {
                        _NetType = String.Format("ICollection<{0}>", _TypeName);
                        if (_PropertyName.EndsWith("s") == false)
                        {
                            _PropertyName += "s";
                        }
                    }
                    else
                    {
                        _NetType = _Property.Attribute("ToRole").Value;
                    }
                }
                else
                {
                    _NetType = _Property.Attribute("Type").Value;
                }
                string _FieldName="_" + _PropertyName.Substring(0, 1).ToLower() +  _PropertyName.Substring(1);
                /* NET now complains that it is not CLS compliant to differ names only by case !
                if(reservedWords.ContainsKey(_FieldName))
                {
                    _FieldName="_" + _FieldName;
                }
                 */
                _SBProperties.AppendFormat("\t\tpublic virtual {0} {1}",_NetType, _PropertyName);
                _SBProperties.AppendLine();
                _SBProperties.AppendLine("\t\t{");
                _SBProperties.AppendLine("\t\t\tget");
                _SBProperties.AppendLine("\t\t\t{");
                _SBProperties.AppendFormat("\t\t\t\treturn {0};", _FieldName);
                _SBProperties.AppendLine();
                _SBProperties.AppendLine("\t\t\t}");
                _SBProperties.AppendLine("\t\t\tset");
                _SBProperties.AppendLine("\t\t\t{");
                _SBProperties.AppendFormat("\t\t\t\t{0}=value;", _FieldName);
                _SBProperties.AppendLine();
                _SBProperties.AppendLine("\t\t\t}");
                _SBProperties.AppendLine("\t\t}");
                _SBFields.AppendFormat("\t\tprivate {0} {1};", _NetType, _FieldName);
                _SBFields.AppendLine();
            }
            aStringBuilder.AppendLine("\t\t#region fields");
            aStringBuilder.Append(_SBFields.ToString());
            aStringBuilder.AppendLine("\t\t#endregion");
            aStringBuilder.AppendLine();
            aStringBuilder.AppendLine("\t\t#region properties");
            aStringBuilder.Append(_SBProperties.ToString());
            aStringBuilder.AppendLine("\t\t#endregion");
            aStringBuilder.AppendLine();
        }

        private static void ProcessCTors(StringBuilder aStringBuilder, string aClassName)
        {
            aStringBuilder.Append("\t\tpublic ");
            aStringBuilder.Append(aClassName);
            aStringBuilder.AppendLine("()");
            aStringBuilder.AppendLine("\t\t{");
            aStringBuilder.AppendLine("\t\t}");
        }

        private static string usingStatements=@"using System;
using System.Collections.Generic;
";
        private static Dictionary<string, object> reservedWords;
        private static XDocument xDocument;

    }
}
