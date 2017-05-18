using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;


public class XmlBuilder
{
    #region Archived
    //private static List<string> key;
    //private static List<string> value;
    #endregion
    private static XmlBuilder instance; 
    public static XmlBuilder Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new XmlBuilder();
            }
            return instance;
        }
    } //Singleton Pattern

    private static XmlNodeList xmlNodes; //a reference to a given XML node
    
    #region XML Elements
    /// <summary>
    /// We only want to grab a reference to the XML Elements pertaining to the inspection form
    /// we hold a dictionary reference to each parent elements child nodes
    /// </summary>
    public static Dictionary<string, string> InspectionData { get; private set; }
    public static Dictionary<string, string> Survey { get; private set; }
    public static Dictionary<string, string> AddnandCATPerils { get; private set; }
    public static Dictionary<string, string> Misc { get; private set; }
    public static Dictionary<string, string> BldgInfo { get; private set; }
    public static Dictionary<string, string> CommonHaz { get; private set; }
    public static Dictionary<string, string> GeneralLiability { get; private set; }
    public static Dictionary<string, string> NeighboringExposures { get; private set; }
    public static Dictionary<string, string> OperationsOccupancy { get; private set; }
    public static Dictionary<string, string> ProtectionSecurity { get; private set; }
    public static Dictionary<string, string> RecsOpinionLosses { get; private set; }
    public static Dictionary<string, string> SpecialHazards { get; private set; }
    public static List<Dictionary<string, string>> Cooking { get; private set; }
    public static Dictionary<string, string> Sprinkler { get; private set; }
    public static List<Dictionary<string, string>> PropertyRecommendations { get; private set; }
    public static List<Dictionary<string, string>> GLRecommendations { get; private set; }
    public static Dictionary<string, string> Wind { get; private set; }
    public static Dictionary<string, string> BI { get; private set; }
    public static Dictionary<string, string> Operations { get; private set; }
    #endregion
    
    /// <summary>
    /// Returns a dictionary composed of a xml node's child elements and it's value 
    /// </summary>
    /// <param name="xmlNodes"></param>
    /// <returns></returns>
    private Dictionary<string, string> GetElements(XmlNode xmlNodes)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        foreach (XmlNode xmlNode in xmlNodes)
        {
            if (string.IsNullOrEmpty(xmlNode.InnerText) || string.IsNullOrWhiteSpace(xmlNode.InnerText))
                dictionary.Add(xmlNode.Name, "");
            else
                dictionary.Add(xmlNode.Name, xmlNode.InnerText);
        }
        return dictionary;
    }
    
    /// <summary>
    /// Populates ElementNodes with the selected XML's content where the element name matches a
    /// desired case value
    /// </summary>
    private void populate(XmlDocument xmlDoc)
    {
        xmlNodes = xmlDoc.ChildNodes[0].ChildNodes;
        foreach (XmlNode item in xmlNodes)
        {
            switch (item.Name.ToLower())
            {
                case ("wkfc_inspectiondata"):
                    InspectionData = GetElements(item);
                    break;
                case "wkfc_inspectiondata_surveyinfo":
                    Survey = GetElements(item);
                    break;
                case "wkfc_inspectiondata_addnandcatperils":
                    AddnandCATPerils = GetElements(item);
                    break;
                case "wkfc_inspectiondata_misc":
                    Misc = GetElements(item);
                    break;
                case "wkfc_inspectiondata_bldginfo":
                    BldgInfo = GetElements(item);
                    break;
                case "wkfc_inspectiondata_commonhaz":
                    CommonHaz = GetElements(item);
                    break;
                case "wkfc_inspectiondata_generalliability":
                    GeneralLiability = GetElements(item);
                    break;
                case "wkfc_inspectiondata_neighboringexposures":
                    NeighboringExposures = GetElements(item);
                    break;
                case "wkfc_inspectiondata_operationsoccupancy":
                    OperationsOccupancy = GetElements(item);
                    break;
                case "wkfc_inspectiondata_protectionsecurity":
                    ProtectionSecurity = GetElements(item);
                    break;
                case "wkfc_inspectiondata_recsopinionlosses":
                    RecsOpinionLosses = GetElements(item);
                    break;
                case "wkfc_inspectiondata_specialhazards":
                    SpecialHazards = GetElements(item);
                    break;
                case "wkfc_inspectiondata_cooking":
                    if (Cooking == null)
                        Cooking = new List<Dictionary<string, string>>();
                    Cooking.Add(GetElements(item));
                    break;
                case "wkfc_inspectiondata_sprinkler":
                    Sprinkler = GetElements(item);
                    break;
                case "wkfc_inspectiondata_propertyrecommendations":
                    if (PropertyRecommendations == null)
                        PropertyRecommendations = new List<Dictionary<string, string>>();
                    PropertyRecommendations.Add(GetElements(item));
                    break;
                case "wkfc_inspectiondata_glrecommendations":
                    if (GLRecommendations == null)
                        GLRecommendations = new List<Dictionary<string, string>>();
                    GLRecommendations.Add(GetElements(item));
                    break;
                case "wkfc_inspectiondata_wind":
                    Wind = GetElements(item);
                    break;
                case "wkfc_inspectiondata_businessintaddendum":
                    BI = GetElements(item);
                    break;
                case "wkfc_inspectiondata_operationsaddendum":
                    Operations = GetElements(item);
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Iterates through a given IMS inspection XML file
    /// </summary>
    /// <param name="xmlfile"></param>
    public void GetInspectionData(string xmlfile)
    {
        #region Archive
        //XmlDocument xmlDoc = new XmlDocument();

        //xmlDoc.Load(xmlfile);

        //ElementNodes = new Dictionary<string, string>();
        //try
        //{
        //    populate(xmlDoc); 
        //}
        //catch(XmlException ex)
        //{
        //    ErrorExceptions.OnException(ex.Message);
        //}   


        //XmlReader xmlReader = XmlReader.Create(xmlfile);
        //key = new List<string>();
        //value = new List<string>();
        //ElementNodes = new Dictionary<string, string>();
        //xmlReader.MoveToContent();
        //while (xmlReader.Read())
        //{
        //    switch (xmlReader.NodeType)
        //    {
        //        case XmlNodeType.Element:
        //            if (string.Format("{0}", xmlReader.Name).Contains("WKFC_InspectionData") ||
        //                string.Format("{0}", xmlReader.Name).Contains("WKFC_InspectionData_SurveyInfo") ||
        //                string.Format("{0}", xmlReader.Name).Contains("WKFC_InspectionData_RecsOpinionLosses") ||
        //                string.Format("{0}", xmlReader.Name).Contains("WKFC_InspectionData_OperationsOccupancy") ||
        //                string.Format("{0}", xmlReader.Name).Contains("WKFC_InspectionData_BldgInfo") ||
        //                string.Format("{0}", xmlReader.Name).Contains("WKFC_InspectionData_CommonHaz") ||
        //                string.Format("{0}", xmlReader.Name).Contains("WKFC_InspectionData_SpecialHazards") ||
        //                string.Format("{0}", xmlReader.Name).Contains("WKFC_InspectionData_ProtectionSecurity") ||
        //                string.Format("{0}", xmlReader.Name).Contains("WKFC_InspectionData_NeighboringExposures") ||
        //                string.Format("{0}", xmlReader.Name).Contains("WKFC_InspectionData_AddnandCATPerils") ||
        //                string.Format("{0}", xmlReader.Name).Contains("WKFC_InspectionData_Misc") ||
        //                string.Format("{0}", xmlReader.Name).Contains("WKFC_InspectionData_GeneralLiability") ||
        //                string.Format("{0}", xmlReader.Name).Contains("WKFC_InspectionData_Sprinkler") ||
        //                string.Format("{0}", xmlReader.Name).Contains("WKFC_InspectionData_PropertyRecommendations") ||
        //                string.Format("{0}", xmlReader.Name).Contains("WKFC_InspectionData_Cooking") ||
        //                string.Format("{0}", xmlReader.Name).Contains("WKFC_InspectionData_GLRecommendations"))
        //                continue;
        //            else
        //            {
        //                if (xmlReader.IsEmptyElement)
        //                {
        //                    key.Add(string.Format("<{0}>", xmlReader.Name));
        //                    value.Add("EMPTY");
        //                }
        //                else
        //                    key.Add(string.Format("<{0}>", xmlReader.Name));
        //            }
        //            break;
        //        case XmlNodeType.Text:
        //            if (!string.IsNullOrEmpty(xmlReader.Value))
        //            {
        //                string unxml = xmlReader.Value;
        //                //replace entities with literal values
        //                unxml = unxml.Replace("&amp;", "&");
        //                value.Add(string.Format("{0}", unxml));
        //            }
        //            else
        //            {

        //                value.Add("EMPTY!");
        //            }
        //            break;
        //    }
        //}
        //for (int i = 0; i < value.Count; i++)
        //    ElementNodes.Add(key[i], value[i]);
        #endregion
        string line = "";
        string fullLine = null;
        //Cleans the XML file of any invalid characters by changing them into their corresponding syntax
        using (StreamReader sr = new StreamReader(xmlfile))
        {
            while (true)
            {
                line = sr.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    break;
                else if (line.Contains("&"))
                {
                    line = line.Replace("&", "&amp;");
                    fullLine += "\n" + line;
                }
                else
                    fullLine += "\n" + line;
            }
        }
        //Uncomment the below line to view  the entire cleaned xml file's content in the console window
        //Console.WriteLine(fullLine);
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(fullLine);
        populate(xmlDocument);
    }
    public void GetInspectionData(Stream xmlFile)
    {
        string line = "";
        string fullLine = null;
        //Cleans the XML file of any invalid characters by changing them into their corresponding syntax
        using (StreamReader sr = new StreamReader(xmlFile))
        {
            while (true)
            {
                line = sr.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    break;
                else if (line.Contains("&"))
                {
                    line = line.Replace("&", "&amp;");
                    fullLine += "\n" + line;
                }
                else
                    fullLine += "\n" + line;
            }
        }
        //Uncomment the below line to view  the entire cleaned xml file's content in the console window
        //Console.WriteLine(fullLine);
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(fullLine);
        populate(xmlDocument);
    }
}
