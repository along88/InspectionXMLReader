using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InspectionXMLReader
{
    class LocalFile : File
    {
        private static OpenFileDialog file;
        /// <summary>
        /// Read-Only string reference to the OpenFileDialog file name
        /// </summary>
        private static string fileName
        {
            get { return file.FileName; }

        }
        /// <summary>
        /// Opens a File Dialog for an XML file
        /// </summary>

        public override void RetrieveFile()
        {
            file = new OpenFileDialog();
            file.Filter = "XML Files (*.xml)|*.xml|All files (*.*)|*.*";
            file.FileOk += OnFileOK;
            file.ShowDialog();
        }

        private void OnFileOK(object sender, EventArgs e)
        {
            Console.WriteLine(fileName.ToString());
            try
            {
                XmlBuilder.Instance.GetInspectionData(fileName); //Starts Process for parsing xml file into a string dictionary
            }
            catch (Exception ex)
            {
                if (!file.ToString().Substring(file.ToString().Length - 3, 3).Equals("xml"))
                    ErrorExceptions.OnException("please select a file with .xml extension");
                else
                    ErrorExceptions.OnException(ex.StackTrace);
            }

        }
    }
}
