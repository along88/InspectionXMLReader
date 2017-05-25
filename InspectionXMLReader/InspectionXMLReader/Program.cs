using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ComponentModel;
using System.Windows.Forms;
using InspectionXMLReader;

class Program
{
    
    [STAThread]
    static void Main(string[] args)
    {
        bool active = true;
        while (active)
        {
            requestForm();
            active = Continue();
        }
    }

    private static void requestForm()
    {
        int ctrlNo;
        Console.WriteLine("Enter Control Number");
        string userResponse = userInput();
        Console.Clear();
        switch (userResponse.ToLower())
        {
            case "":
                LocalFile localFile = new LocalFile();
                loadXml(localFile.FilePath);
                InspectionForm localForm = new InspectionForm(requestFormType());
                break;
            default:
                if (int.TryParse(userResponse, out ctrlNo))
                {
                    HostedFile hostedFile = new HostedFile(userResponse);
                    loadXml(hostedFile.FilePath);
                    System.IO.File.Delete(hostedFile.FilePath);
                    InspectionForm imsForm = new InspectionForm(requestFormType());
                }
                else
                    requestForm();
                break;
        }
    }
    private static string userInput()
    {
        string response;
        Console.Write(">> ");
        return response = Console.ReadLine();
    }
    private static void loadXml(string path)
    {
        try
        {
            XmlBuilder.Instance.GetInspectionData(path);
        }
        catch (Exception ex)
        {
            if (string.IsNullOrEmpty(path))
                requestForm();
            else if (!path.ToString().Substring(path.ToString().Length - 3, 3).Equals("xml"))
                ErrorExceptions.OnException("please select a file with an xml extension");
            else
                ErrorExceptions.OnException(ex.StackTrace);
        }
    }
    private static string requestFormType()
    {
        bool selecting = true;
        string formType = "";
        while (selecting)
        {
            Console.WriteLine("Addendum Type:");
            Console.WriteLine("1. Inspection Format \n2. IM - Builders Risk \n3. GL Rec Letter \n4. BI Addendum \n5. Operations Addendum \n6. Property Rec Letter \n7. Rec Check Inspection Form \n8. Wind Addendum");
            formType = retrieveForm(formType);
            Console.Clear();
            if(!string.IsNullOrEmpty(formType))
                selecting = false;
        }
        return formType;
    }
    private static string retrieveForm(string formType)
    {
        switch (userInput())
        {
            case "1":
                formType = "inspection format";
                break;
            case "2":
                formType = "im builders risk";
                break;
            case "3":
                formType = "GL Rec Letter";
                break;
            case "4":
                formType = "BI Addendum";
                break;
            case "5":
                formType = "Operations Addendum";
                break;
            case "6":
                formType = "Property Rec Letter";
                break;
            case "7":
                formType = "Rec Check Inspection Form";
                break;
            case "8":
                formType = "Wind Addendum";
                break;
            default:
                ErrorExceptions.OnException("Please pick a corresponding number");
                break;
        }
        return formType;
    }
    private static bool Continue()
    {
        Console.WriteLine("Would you like to load a new Inspection Form? Y/N");
        string newForm = userInput();
        switch (newForm.ToLower())
        {
            case "y":
                Console.Clear();
                return true;
            case "n":
                Console.Clear();
                return false;
            default:
                Continue();
                return true; 
        }
    }
}

