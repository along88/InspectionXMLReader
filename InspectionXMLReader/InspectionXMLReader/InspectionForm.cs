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
/// </summary>

public class InspectionForm
{
    private object missing = System.Reflection.Missing.Value;
    private object fileName = ""; //filename of the given word document
    private Application wordApp; //A instance of a word Application
    private Document inspectionDoc;// A instance of a document inside a word Application
    private List<Dictionary<string, string>> foundElements; //used to hold a list of XmlBuilder's element dictionaries
    public InspectionForm(string form)
    {
        GetFileName(form);
        InitializeInspectionForm();
        Console.Write("Building Document" + Environment.NewLine + "Please Wait.");
        FillInspectionForm();
        Console.WriteLine("Complete!");
        wordApp.Visible = true;
    }

    /// <summary>
    /// Initializes Word Application and Fill it's content with the XML dictionary
    /// </summary>
    private void InitializeInspectionForm()
    {
        wordApp = new Application();
        wordApp.Visible = false;
        inspectionDoc = new Document();
        inspectionDoc = wordApp.Documents.Open(ref fileName, ReadOnly: false);
    }

    /// <summary>
    /// Fills in the inspection form using the XmlBuilders dictionary
    /// </summary>
    /// <param name="inspectionDoc"></param>
    /// <param name="wordApp"></param>
    private void FillInspectionForm()
    {
        int percentage;
        try
        {
            for (int i = 0; i < inspectionDoc.Tables.Count; i++)
            {
                foreach (Cell cell in inspectionDoc.Tables[i + 1].Range.Cells)
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
                                    cell.Range.Text = key.Value;
                                    //Need to change text of tag color here
                                    foundElements[k].Remove(key.Key);
                                    break;
                                }
                            }
                        }
                    }
                }
                Console.SetCursorPosition(0, 2);
                percentage = (i * 100 / inspectionDoc.Tables.Count);
                Console.Write(percentage.ToString() + "%");
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

    /// <summary>
    /// Loads the specified Inspection Form Template
    /// </summary>
    /// <param name="form"></param>
    private void GetFileName(string form)
    {
        foundElements = new List<Dictionary<string, string>>();
        string root = System.IO.Directory.GetCurrentDirectory();
        switch (form)
        {
            case "inspection format":
                fileName = root + @"\Template\WKFCInspectionformat.doc";
                if (XmlBuilder.InspectionData != null)
                    foundElements.Add(XmlBuilder.InspectionData);
                if (XmlBuilder.Survey != null)
                    foundElements.Add(XmlBuilder.Survey);
                if (XmlBuilder.RecsOpinionLosses != null)
                    foundElements.Add(XmlBuilder.RecsOpinionLosses);
                if (XmlBuilder.OperationsOccupancy != null)
                    foundElements.Add(XmlBuilder.OperationsOccupancy);
                if (XmlBuilder.BldgInfo != null)
                    foundElements.Add(XmlBuilder.BldgInfo);
                if (XmlBuilder.CommonHaz != null)
                    foundElements.Add(XmlBuilder.CommonHaz);
                if (XmlBuilder.SpecialHazards != null)
                    foundElements.Add(XmlBuilder.SpecialHazards);
                if (XmlBuilder.ProtectionSecurity != null)
                    foundElements.Add(XmlBuilder.ProtectionSecurity);
                if (XmlBuilder.NeighboringExposures != null)
                    foundElements.Add(XmlBuilder.NeighboringExposures);
                if (XmlBuilder.AddnandCATPerils != null)
                    foundElements.Add(XmlBuilder.AddnandCATPerils);
                if (XmlBuilder.Misc != null)
                    foundElements.Add(XmlBuilder.Misc);
                if (XmlBuilder.Cooking != null)
                    for (int i = 0; i < XmlBuilder.Cooking.Count; i++)
                        foundElements.Add(XmlBuilder.Cooking[i]);
                if (XmlBuilder.Sprinkler != null)
                    foundElements.Add(XmlBuilder.Sprinkler);
                if (XmlBuilder.GeneralLiability != null)
                    foundElements.Add(XmlBuilder.GeneralLiability);
                break;
            case "im builders risk":
                fileName = root + @"\Template\imbuildersriskdataelements.doc";
                break;
            case "GL Rec Letter":
                fileName = root + @"\Template\GLRecLetter.doc";
                if (XmlBuilder.GLRecommendations != null)
                    for (int i = 0; i < XmlBuilder.GLRecommendations.Count; i++)
                        foundElements.Add(XmlBuilder.GLRecommendations[i]);
               break;
            case "BI Addendum":
                fileName = root + @"\Template\BIADDENDUM.doc";
                if (XmlBuilder.BI != null)
                    foundElements.Add(XmlBuilder.BI);
                break;
            case "Operations Addendum":
                fileName = root + @"\Template\OPERATIONSADDENDUM.doc";
                if (XmlBuilder.Operations != null)
                    foundElements.Add(XmlBuilder.Operations);
                break;
            case "Property Rec Letter":
                fileName = root + @"\Template\PropertyRecLetter.doc";
                if (XmlBuilder.PropertyRecommendations != null)
                    for (int i = 0; i < XmlBuilder.PropertyRecommendations.Count; i++)
                        foundElements.Add(XmlBuilder.PropertyRecommendations[i]);
                break;
            case "Rec Check Inspection Form":
                fileName = root + @"\Template\RECCHECKINSPECTIONFORM.docx";
                if (XmlBuilder.PropertyRecommendations != null)
                    for (int i = 0; i < XmlBuilder.PropertyRecommendations.Count; i++)
                        foundElements.Add(XmlBuilder.PropertyRecommendations[i]);
               if (XmlBuilder.GLRecommendations != null)
                    for (int i = 0; i < XmlBuilder.GLRecommendations.Count; i++)
                        foundElements.Add(XmlBuilder.GLRecommendations[i]);
                break;
            case "Wind Addendum":
                fileName = root + @"\Template\WindAddendum.docx";
                if (XmlBuilder.Wind != null)
                    foundElements.Add(XmlBuilder.Wind);
                break;
            default:
                break;
        }
    }
}

