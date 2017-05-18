using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public class FileManager
{
    
    private File file;
    private static FileManager instance;
    public static FileManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new FileManager();
            }
            return instance;
        }
    }
    

    public void acceptfile(File fileType)
    {
        file = fileType;
        file.RetrieveFile();
    }
}

