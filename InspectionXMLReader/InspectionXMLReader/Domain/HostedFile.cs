using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace InspectionXMLReader
{
    /// <summary>
    /// this class queries the IMS database and saves the results on the users machine as a text file.
    /// The text file is disposed off after the XML Builder parses and scrubs the results.
    /// </summary>
    public class HostedFile 
    {
        private string controlNumber;
        private string filePath;
        private static Dictionary<int, string> InspectionTables;
        public string FilePath { get { return filePath; } }
        public string ControlNumber
        {
            get
            {
                return controlNumber;
            }
            set
            {
                controlNumber = value;
            }
        }

        public HostedFile(string controlNumber) 
        {
            this.controlNumber = controlNumber;
            initialize();
            buildXmlFile();
        }

        protected void initialize()
        {
            filePath = System.IO.Directory.GetCurrentDirectory() + @"\Temp\" + controlNumber + ".xml";
            InspectionTables = new Dictionary<int, string>();
            InspectionTables.Add(1, "WKFC_InspectionData");
            InspectionTables.Add(2, "WKFC_InspectionData_SurveyInfo");
            InspectionTables.Add(3, "WKFC_InspectionData_AddnandCATPerils");
            InspectionTables.Add(4, "WKFC_InspectionData_Misc");
            InspectionTables.Add(5, "WKFC_InspectionData_BldgInfo");
            InspectionTables.Add(6, "WKFC_InspectionData_CommonHaz");
            InspectionTables.Add(7, "WKFC_InspectionData_NeighboringExposures");
            InspectionTables.Add(8, "WKFC_InspectionData_OperationsOccupancy");
            InspectionTables.Add(9, "WKFC_InspectionData_ProtectionSecurity");
            InspectionTables.Add(10, "WKFC_InspectionData_RecsOpinionLosses");
            InspectionTables.Add(11, "WKFC_InspectionData_SpecialHazards");
            InspectionTables.Add(12, "WKFC_InspectionData_Sprinkler");
            InspectionTables.Add(13, "WKFC_InspectionData_Wind");
            InspectionTables.Add(14, "WKFC_InspectionData_PropertyRecommendations");
           
        }
        private void buildXmlFile()
        {
            writeToXmlFile("<InspectionData>");
            foreach (KeyValuePair<int, string> tableName in InspectionTables)
                queryIMSTable(tableName.Value);
            writeToXmlFile(@"</InspectionData>");
        }
        private void writeToXmlFile(string line)
        {
            using (StreamWriter sw = new StreamWriter(filePath, true))
                sw.WriteLine(line);
        }
        private void queryIMSTable(string table)
        {
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = "Server=sql.wkfc.com;Initial Catalog=WKFC_IMS;UID=jmarkman@wkfc.com;PWD=@20kfWc17!!;";
                cnn.Open();
                SqlCommand cmd = new SqlCommand(selectedQry(table), cnn);
                cmd.Parameters.AddWithValue("@controlNumber", controlNumber);
                SqlDataReader rdr = cmd.ExecuteReader();
                ReadQuery(rdr, table);
                cnn.Close();
        }
        private string selectedQry(string table)
        {
           switch (table)
            {
                case "WKFC_InspectionData":
                    return string.Format("SELECT LocationID, ControlNo,Recs,ReportNumber,Added,WKFCNotes,Num1,Num2,Num3,Num4,Num5,Num6,Num7,Num8,Num9,Num10,Num11,Num12,Num13,Num14,Num15,Num16,Num17,Num18,Num19,Num20,VendorID FROM {0} WHERE ControlNo = @controlNumber", table.ToString());
                case "WKFC_InspectionData_SurveyInfo":
                    return string.Format("SELECT {0}.Survey1,{0}.Survey2,{0}.Survey3,{0}.Survey4,{0}.Survey5,{0}.Survey6,{0}.Survey7,{0}.Survey8 from {0} inner join dbo.WKFC_InspectionData on dbo.WKFC_InspectionData.ControlNo = @controlNumber AND {0}.InspID = dbo.WKFC_InspectionData.InspID", table.ToString());
                case "WKFC_InspectionData_AddnandCATPerils":
                    return string.Format("SELECT {0}.Peril1,{0}.Peril2,{0}.Peril3,{0}.Peril4,{0}.Peril5,{0}.Peril6,{0}.Peril7,{0}.Peril8,{0}.Peril9,{0}.Peril10,{0}.Peril11 from {0} inner join dbo.WKFC_InspectionData on dbo.WKFC_InspectionData.ControlNo = @controlNumber AND {0}.InspID = dbo.WKFC_InspectionData.InspID", table.ToString());
                case "WKFC_InspectionData_Misc":
                    return string.Format("SELECT {0}.Misc1 FROM {0} inner join dbo.WKFC_InspectionData on dbo.WKFC_InspectionData.ControlNo = @controlNumber AND {0}.InspID = dbo.WKFC_InspectionData.InspID", table.ToString());
                case "WKFC_InspectionData_BldgInfo":
                    return string.Format("SELECT {0}.Bld1,{0}.Bld2,{0}.Bld3,{0}.Bld4,{0}.Bld5,{0}.Bld6,{0}.Bld7,{0}.Bld8,{0}.Bld9,{0}.Bld10,{0}.Bld11,{0}.Bld12,{0}.Bld13,{0}.Bld14,{0}.Bld15,{0}.Bld16,{0}.Bld17,{0}.Bld18,{0}.Bld19,{0}.Bld20,{0}.Bld21,{0}.Bld22,{0}.Bld23,{0}.Bld24,{0}.Bld25,{0}.Bld26,{0}.Bld27,{0}.Bld28,{0}.Bld29,{0}.Bld30,{0}.Bld31,{0}.Bld32,{0}.Bld33,{0}.Bld34,{0}.Bld35,{0}.Bld36,{0}.Bld37,{0}.Bld38,{0}.Bld39,{0}.Bld40,{0}.Bld41,{0}.Bld42,{0}.Bld43,{0}.Bld44,{0}.Bld45,{0}.Bld46 from {0} inner join dbo.WKFC_InspectionData on dbo.WKFC_InspectionData.ControlNo = @controlNumber AND {0}.InspID = dbo.WKFC_InspectionData.InspID", table.ToString());
                case "WKFC_InspectionData_CommonHaz":
                    return string.Format("SELECT {0}.CH1,{0}.CH2,{0}.CH3,{0}.CH4,{0}.CH5,{0}.CH6,{0}.CH7,{0}.CH8,{0}.CH9,{0}.CH10,{0}.CH11,{0}.CH12,{0}.CH13,{0}.CH14,{0}.CH15,{0}.CH16,{0}.CH17,{0}.CH18,{0}.CH19,{0}.CH20,{0}.CH21,{0}.CH22,{0}.CH23,{0}.CH24,{0}.CH25,{0}.CH26,{0}.CH27,{0}.CH28,{0}.CH29,{0}.CH30,{0}.CH31,{0}.CH32,{0}.CH33,{0}.CH34,{0}.CH35,{0}.CH36,{0}.CH37,{0}.CH38,{0}.CH39,{0}.CH40,{0}.CH41,{0}.CH42,{0}.CH43,{0}.CH44,{0}.CH45,{0}.CH46,{0}.CH47 from {0} inner join dbo.WKFC_InspectionData on dbo.WKFC_InspectionData.ControlNo = @controlNumber AND {0}.InspID = dbo.WKFC_InspectionData.InspID", table.ToString());
                case "WKFC_InspectionData_NeighboringExposures":
                    return string.Format("SELECT {0}.NE1,{0}.NE2,{0}.NE3,{0}.NE4,{0}.NE5,{0}.NE6,{0}.NE7,{0}.NE8,{0}.NE9,{0}.NE10,{0}.NE11,{0}.NE12,{0}.NE13,{0}.NE14,{0}.NE15,{0}.NE16,{0}.NE17,{0}.NE18,{0}.NE19,{0}.NE20,{0}.NE21,{0}.NE22,{0}.NE23,{0}.NE24,{0}.NE25,{0}.NE26,{0}.NE27 from {0} inner join dbo.WKFC_InspectionData on dbo.WKFC_InspectionData.ControlNo = @controlNumber AND {0}.InspID = dbo.WKFC_InspectionData.InspID", table.ToString());
                case "WKFC_InspectionData_OperationsOccupancy":
                    return string.Format("SELECT {0}.Ops1,{0}.Ops2,{0}.Ops3,{0}.Ops4,{0}.Ops5,{0}.Ops6,{0}.Ops7,{0}.Ops8,{0}.Ops9,{0}.Ops10,{0}.Ops11,{0}.Ops12,{0}.Ops13,{0}.Ops14,{0}.Ops15,{0}.Ops16,{0}.Ops17,{0}.Ops18,{0}.Ops19,{0}.Ops20,{0}.Ops21,{0}.Ops22,{0}.Ops23,{0}.Ops24,{0}.Ops25,{0}.Ops26,{0}.Ops27,{0}.Ops28 from {0} inner join dbo.WKFC_InspectionData on dbo.WKFC_InspectionData.ControlNo = @controlNumber AND {0}.InspID = dbo.WKFC_InspectionData.InspID", table.ToString());
                case "WKFC_InspectionData_ProtectionSecurity":
                    return string.Format("SELECT {0}.Prot1,{0}.Prot2,{0}.Prot3,{0}.Prot4,{0}.Prot5,{0}.Prot6,{0}.Prot7,{0}.Prot8,{0}.Prot9,{0}.Prot10,{0}.Prot11,{0}.Prot12,{0}.Prot13,{0}.Prot14,{0}.Prot15,{0}.Prot16,{0}.Prot17,{0}.Prot18,{0}.Prot19,{0}.Prot20,{0}.Prot21,{0}.Prot22,{0}.Prot23,{0}.Prot24,{0}.Prot25,{0}.Prot26,{0}.Prot27,{0}.Prot28,{0}.Prot29,{0}.Prot30,{0}.Prot31,{0}.Prot32,{0}.Prot33,{0}.Prot34,{0}.Prot35,{0}.Prot36,{0}.Prot37,{0}.Prot38,{0}.Prot39 from {0} inner join dbo.WKFC_InspectionData on dbo.WKFC_InspectionData.ControlNo = @controlNumber AND {0}.InspID = dbo.WKFC_InspectionData.InspID", table.ToString());
                case "WKFC_InspectionData_RecsOpinionLosses":
                    return string.Format("SELECT {0}.Recs1,{0}.Recs2,{0}.Recs3,{0}.Opinion1,{0}.Opinion2,{0}.Opinion3,{0}.Loss1,{0}.Loss2 from {0} inner join dbo.WKFC_InspectionData on dbo.WKFC_InspectionData.ControlNo = @controlNumber AND {0}.InspID = dbo.WKFC_InspectionData.InspID", table.ToString());
                case "WKFC_InspectionData_SpecialHazards":
                    return string.Format("SELECT {0}.SH1,{0}.SH2,{0}.SH3,{0}.SH4,{0}.SH5,{0}.SH6,{0}.SH7,{0}.SH8,{0}.SH9,{0}.SH10,{0}.SH11,{0}.SH12,{0}.SH13 from {0} inner join dbo.WKFC_InspectionData on dbo.WKFC_InspectionData.ControlNo = @controlNumber AND {0}.InspID = dbo.WKFC_InspectionData.InspID", table.ToString());
                case "WKFC_InspectionData_Sprinkler":
                    return string.Format("SELECT {0}.Sprink1,{0}.Sprink2,{0}.Sprink3,{0}.Sprink4,{0}.Sprink5,{0}.Sprink6,{0}.Sprink7,{0}.Sprink8,{0}.Sprink9,{0}.Sprink10,{0}.Sprink11,{0}.Sprink12,{0}.Sprink13,{0}.Sprink14,{0}.Sprink15,{0}.Sprink16,{0}.Sprink17,{0}.Sprink18,{0}.Sprink19,{0}.Sprink20,{0}.Sprink21,{0}.Sprink22,{0}.Sprink23,{0}.Sprink24,{0}.Sprink25,{0}.Sprink26,{0}.Sprink27,{0}.Sprink28,{0}.Sprink29,{0}.Sprink30,{0}.Sprink31 from {0} inner join dbo.WKFC_InspectionData on dbo.WKFC_InspectionData.ControlNo = @controlNumber AND {0}.InspID = dbo.WKFC_InspectionData.InspID", table.ToString());
                case "WKFC_InspectionData_Wind":
                    return string.Format("SELECT {0}.Wind1,{0}.Wind2,{0}.Wind3,{0}.Wind4,{0}.Wind5,{0}.Wind6,{0}.Wind7,{0}.Wind8,{0}.Wind9,{0}.Wind10,{0}.Wind11,{0}.Wind12,{0}.Wind13,{0}.Wind14,{0}.Wind15,{0}.Wind16,{0}.Wind17,{0}.Wind18,{0}.Wind19,{0}.Wind20,{0}.Wind21,{0}.Wind22,{0}.Wind23,{0}.Wind24,{0}.Wind25,{0}.Wind26,{0}.Wind27,{0}.Wind28,{0}.Wind29,{0}.Wind30,{0}.Wind31,{0}.Wind32,{0}.Wind33,{0}.Wind34,{0}.Wind35,{0}.Wind36,{0}.Wind37,{0}.Wind38,{0}.Wind39,{0}.Wind40,{0}.Wind41,{0}.Wind42,{0}.Wind43,{0}.Wind44,{0}.Wind45,{0}.Wind46,{0}.Wind47,{0}.Wind48,{0}.Wind49,{0}.Wind50,{0}.Wind51,{0}.Wind52,{0}.Wind53,{0}.Wind54,{0}.Wind55,{0}.Wind56,{0}.Wind57,{0}.Wind58,{0}.Wind59,{0}.Wind60,{0}.Wind61,{0}.Wind62,{0}.Wind63,{0}.Wind64,{0}.Wind65,{0}.Wind66,{0}.Wind67,{0}.Wind68,{0}.Wind69,{0}.Wind70,{0}.Wind71,{0}.Wind72,{0}.Wind73,{0}.Wind74,{0}.Wind75,{0}.Wind76,{0}.Wind77,{0}.Wind78,{0}.Wind79,{0}.Wind80,{0}.Wind81,{0}.Wind82,{0}.Wind83 FROM {0} inner join dbo.WKFC_InspectionData on dbo.WKFC_InspectionData.ControlNo = @controlNumber AND {0}.InspID = dbo.WKFC_InspectionData.InspID", table.ToString());
                case "WKFC_InspectionData_PropertyRecommendations":
                    return string.Format("SELECT {0}.Num1,{0}.Num2,{0}.Num8,{0}.Num9,{0}.Num10,{0}.Num13,{0}.PrRecNum,{0}.PropRec from {0} inner join dbo.WKFC_InspectionData on dbo.WKFC_InspectionData.ControlNo = @controlNumber AND {0}.InspID = dbo.WKFC_InspectionData.InspID", table.ToString());
                default:
                    return null;
            }
        }
        private void ReadQuery(SqlDataReader rdr, string table)
        {
            while (rdr.Read())
            {
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    if (i == 0)
                    {
                        writeToXmlFile(string.Format("<{0}>", table.ToString()));
                    }
                    if (rdr.GetName(i).ToString().Contains("Survey") || rdr.GetName(i).ToString().Contains("Ops") || rdr.GetName(i).ToString().Contains("Peril"))
                    {
                        writeToXmlFile(string.Format("<{0}>{1}</{0}>", rdr.GetName(i).ToString(), rdr.GetTextReader(i).ReadLine()));
                    }
                    else
                        writeToXmlFile(string.Format("<{0}>{1}</{0}>", rdr.GetName(i).ToString(), rdr.GetValue(i).ToString()));
                    if (i == rdr.FieldCount - 1)
                    {
                        writeToXmlFile(string.Format("</{0}>", table.ToString()));
                    }
                }
            }
        }
    }
}

