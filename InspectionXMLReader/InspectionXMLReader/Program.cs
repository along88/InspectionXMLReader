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
    private static string[] fileTypes =  new string[2];
    [STAThread]
    static void Main(string[] args)
    {
        bool active = true;
        while (active)
        {
            RequestForm();
            active = Continue();
        }
    }

    private static void RequestForm()
    {
        int ctrlNo;
        Console.WriteLine("Enter Control Number");
        //Console.WriteLine("2.Load Local File");
        string response = Console.ReadLine();
        switch (response.ToLower())
        {
            case "cmd load":
                LocalFile localFile = new LocalFile();
                Load(localFile.Path);
                InspectionForm localForm = new InspectionForm(SelectFormType());
                break;
            default:
                if (int.TryParse(response,out ctrlNo))
                {
                    HostedFile hostedFile = new HostedFile(response);
                    Load(hostedFile.Path);
                    System.IO.File.Delete(hostedFile.Path);
                    InspectionForm imsForm = new InspectionForm(SelectFormType());

                }
                else
                {
                    RequestForm();
                }
                break;
        }
        
        

    }

    /// <summary>
    /// Prompts user for a form to load and returns the form selection as a string
    /// </summary>
    /// <returns></returns>
    static string SelectFormType()
    {
        bool selecting = true;
        string formType = "";
        while (selecting)
        {
            Console.WriteLine("Addendum Type:");
            Console.WriteLine("1. Inspection Format \n2. IM - Builders Risk \n3. GL Rec Letter \n4. BI Addendum \n5. Operations Addendum \n6. Property Rec Letter \n7. Rec Check Inspection Form \n8. Wind Addendum");
            string response = Console.ReadLine();

            switch (response)
            {
                case "1":
                    formType = "inspection format";
                    Console.Clear();
                    selecting = false;
                    break;
                case "2":
                    formType = "im builders risk";
                    Console.Clear();
                    selecting = false;
                    break;
                case "3":
                    formType = "GL Rec Letter";
                    Console.Clear();
                    selecting = false;
                    break;
                case "4":
                    formType = "BI Addendum";
                    Console.Clear();
                    selecting = false;
                    break;
                case "5":
                    formType = "Operations Addendum";
                    Console.Clear();
                    selecting = false;
                    break;
                case "6":
                    formType = "Property Rec Letter";
                    Console.Clear();
                    selecting = false;
                    break;
                case "7":
                    formType = "Rec Check Inspection Form";
                    Console.Clear();
                    selecting = false;
                    break;
                case "8":
                    formType = "Wind Addendum";
                    Console.Clear();
                    selecting = false;
                    break;
                default:
                    ErrorExceptions.OnException("Please pick a corresponding number");
                    Console.Clear();
                    break;
            }
        }
        return formType;
    }

    static void Load(string path)
    {
        try

        {
            XmlBuilder.Instance.GetInspectionData(path);
            
        }
        catch (Exception ex)
        {
            if(string.IsNullOrEmpty(path))
            {
                RequestForm();
            }
            else if (!path.ToString().Substring(path.ToString().Length - 3, 3).Equals("xml") ||
                            !path.ToString().Substring(path.ToString().Length - 3, 3).Equals("txt"))
                ErrorExceptions.OnException("please select a file with xml or txt extension");
            else
                ErrorExceptions.OnException(ex.StackTrace);
        }
    }

    static bool Continue()
    {
        Console.WriteLine("Would you like to load a new Inspection Form? Y/N");
        string newForm = Console.ReadLine();
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

