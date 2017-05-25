using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InspectionXMLReader
{
    class LocalFile 
    {
        private static OpenFileDialog file;
        public string FilePath
        {
            get { return file.FileName; }

        }

        public LocalFile()
        {
            initialize();
        }

        private void initialize()
        {
            file = new OpenFileDialog();
            file.Filter = "XML Files (*.xml)|*.xml|All files (*.*)|*.*";
            file.ShowDialog();
        }
    }
}
