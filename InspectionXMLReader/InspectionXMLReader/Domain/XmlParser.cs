using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;


public class XmlParser
{
    private static XmlParser instance; 
    public static XmlParser Instance
    {
        get
        {
            if (instance == null)
                instance = new XmlParser();
            return instance;
        }
    } //Singleton Pattern
    private static XmlNodeList xmlNodes; //a reference to a given XML node
    #region XML Elements
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
    public void GetInspectionData(string filePath)
    {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(cleanXmlFile(filePath));
        populateElements(xmlDocument);
    }
    private string cleanXmlFile(string filePath)
    {
        string line = "";
        StringBuilder fullLine = new StringBuilder();
        using (StreamReader sr = new StreamReader(filePath))
        {
            while (true)
            {
                line = sr.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    break;
                else if (line.Contains("&"))
                {
                    line = line.Replace("&", "&amp;");
                    fullLine.Append("\n" + line);
                }
                else
                    fullLine.Append("\n" + line);
            }
        }
        return fullLine.ToString();
    }
    private void populateElements(XmlDocument xmlDoc)
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
}
