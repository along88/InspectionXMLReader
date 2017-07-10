using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;
using System.Diagnostics;
using System.Reflection;
/// <summary>
/// PLEASE REFACTOR ME!!! FOR THE LOVE OF GLOB PLEASE REFACTOR ME!
/// https://www.youtube.com/watch?v=L4DX2DBWtTk
/// This Class Consumes the XMLBuilders found elements based on the form requested.
/// It then matches the found elements it consumed with the elements in the form.
/// </summary>


public class InspectionForm
{
    private object missing = System.Reflection.Missing.Value;
    private object fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Temp\form"; //filename of the given word document
    private string prefix = ".doc";

    private Application wordApp; //A instance of a word Application
    private Document inspectionDoc;// A instance of a document inside a word Application
    private List<Dictionary<string, string>> foundElements; //used to hold a list of XmlBuilder's element dictionaries

    public InspectionForm(string formType)
    {
        GetFileName(formType);
        InitializeInspectionForm();
        Console.Write("Building Document" + Environment.NewLine + "Please Wait.");
        FillInspectionForm();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Complete!");
        Console.ForegroundColor = ConsoleColor.Gray;
        wordApp.Visible = true;
    }

    private void GetFileName(string form)
    {
        foundElements = new List<Dictionary<string, string>>();
        string root = System.IO.Directory.GetCurrentDirectory();
        object _fileName = "";
        switch (form)
        {
            case "inspection format":
                _fileName = root + @"\WKFCInspectionformat";
                if (XmlParser.InspectionData != null)
                    foundElements.Add(XmlParser.InspectionData);
                if (XmlParser.Survey != null)
                    foundElements.Add(XmlParser.Survey);
                if (XmlParser.RecsOpinionLosses != null)
                    foundElements.Add(XmlParser.RecsOpinionLosses);
                if (XmlParser.OperationsOccupancy != null)
                    foundElements.Add(XmlParser.OperationsOccupancy);
                if (XmlParser.BldgInfo != null)
                    foundElements.Add(XmlParser.BldgInfo);
                if (XmlParser.CommonHaz != null)
                    foundElements.Add(XmlParser.CommonHaz);
                if (XmlParser.SpecialHazards != null)
                    foundElements.Add(XmlParser.SpecialHazards);
                if (XmlParser.ProtectionSecurity != null)
                    foundElements.Add(XmlParser.ProtectionSecurity);
                if (XmlParser.NeighboringExposures != null)
                    foundElements.Add(XmlParser.NeighboringExposures);
                if (XmlParser.AddnandCATPerils != null)
                    foundElements.Add(XmlParser.AddnandCATPerils);
                if (XmlParser.Misc != null)
                    foundElements.Add(XmlParser.Misc);
                if (XmlParser.Cooking != null)
                    for (int i = 0; i < XmlParser.Cooking.Count; i++)
                        foundElements.Add(XmlParser.Cooking[i]);
                if (XmlParser.Sprinkler != null)
                    foundElements.Add(XmlParser.Sprinkler);
                if (XmlParser.GeneralLiability != null)
                    foundElements.Add(XmlParser.GeneralLiability);
                break;
            case "im builders risk":
                _fileName = root + @"\imbuildersriskdataelements";
                break;
            case "GL Rec Letter":
                _fileName = root + @"\GLRecLetter";
                if (XmlParser.GLRecommendations != null)
                    for (int i = 0; i < XmlParser.GLRecommendations.Count; i++)
                        foundElements.Add(XmlParser.GLRecommendations[i]);
                break;
            case "BI Addendum":
                _fileName = root + @"\BIADDENDUM";
                if (XmlParser.BI != null)
                    foundElements.Add(XmlParser.BI);
                break;
            case "Operations Addendum":
                _fileName = root + @"\OPERATIONSADDENDUM";
                if (XmlParser.Operations != null)
                    foundElements.Add(XmlParser.Operations);
                break;
            case "Property Rec Letter":
                _fileName = root + @"\PropertyRecLetter";
                if (XmlParser.PropertyRecommendations != null)
                    for (int i = 0; i < XmlParser.PropertyRecommendations.Count; i++)
                        foundElements.Add(XmlParser.PropertyRecommendations[i]);
                break;
            case "Rec Check Inspection Form":
                _fileName = root + @"\RECCHECKINSPECTIONFORM";
                if (XmlParser.PropertyRecommendations != null)
                    for (int i = 0; i < XmlParser.PropertyRecommendations.Count; i++)
                        foundElements.Add(XmlParser.PropertyRecommendations[i]);
                if (XmlParser.GLRecommendations != null)
                    for (int i = 0; i < XmlParser.GLRecommendations.Count; i++)
                        foundElements.Add(XmlParser.GLRecommendations[i]);
                break;
            case "Wind Addendum":
                _fileName = root + @"\WindAddendum";
                if (XmlParser.Wind != null)
                    foundElements.Add(XmlParser.Wind);
                break;
            default:
                break;
        }
        if (System.IO.File.Exists(fileName.ToString() + prefix))
        {
            System.IO.File.Delete(fileName.ToString() + prefix);
        }
        System.IO.File.Copy(_fileName.ToString() + prefix, fileName.ToString() + prefix);


    }
    private void InitializeInspectionForm()
    {
        wordApp = new Application();
        wordApp.Visible = false;
        inspectionDoc = new Document();
        object appended = fileName+ prefix;
        inspectionDoc = wordApp.Documents.Open(ref appended, ReadOnly: false);
    }
    private void FillInspectionForm()
    {
        int percentage;
        try
        {
          for (int i = 0; i < inspectionDoc.Tables.Count; i++)
            {
                foreach (Cell cell in inspectionDoc.Tables[i + 1].Range.Cells)
                {
                    WriteToCell(cell);
                    ColorCell(cell);
                }
                
                percentage = ProgressCounter(i, inspectionDoc.Tables.Count + foundElements.Count);
            }
            Console.SetCursorPosition(0, 2);
            Console.Write("100%");
            Console.WriteLine();
            inspectionDoc.Activate();

        }
        catch (Exception ex)
        {
            ErrorExceptions.OnException(ex.StackTrace);
            inspectionDoc.Application.Quit(ref missing, ref missing, ref missing);
        }
    }
    private void WriteToCell(Cell cell)
    {
        if (cell.Range.Text[0].Equals('<'))
        {
            for (int k = 0; k < foundElements.Count; k++)
            {
                if (foundElements[k] == null)
                    continue;
                foreach (var key in foundElements[k])
                {
                    if (cell.Range.Text.Contains(String.Format("<{0}>", key.Key)))
                    {
                        //cell.Range.Font.Color = WdColor.wdColorRed;
                        cell.Range.Text = key.Value;
                        //Need to change text of tag color here
                        foundElements[k].Remove(key.Key);
                        break;
                    }
                }
            }
        }
    }
    private void ColorCell(Cell cell)
    {
        if (cell.Range.Text[0].Equals('<'))
            cell.Range.Font.Color = WdColor.wdColorRed;
    }
    private int ProgressCounter(int currentProgress, int TableCount)
    {
        Console.SetCursorPosition(0,2);
        currentProgress = (currentProgress * 100 / TableCount);
        Console.Write(currentProgress.ToString() + "%");
        return currentProgress;
    }

    
}

