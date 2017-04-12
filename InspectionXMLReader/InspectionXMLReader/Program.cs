using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ComponentModel;
using System.Windows.Forms;


class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        try
        {
            FileManager.Instance.NewFile();
            InspectionForm inspectionForm = new InspectionForm(RequestForm());
        }
        catch (Exception ex)
        {
            ErrorExceptions.OnException(ex.Message);
            Console.WriteLine(ex.Message);
        }
        Console.ReadKey();
    }

    /// <summary>
    /// Prompts user for a form to load and returns the form selection as a string
    /// </summary>
    /// <returns></returns>
    static string RequestForm()
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
}

