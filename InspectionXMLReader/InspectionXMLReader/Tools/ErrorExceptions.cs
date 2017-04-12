using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    static class ErrorExceptions
    {
        public static string LastMessage { get; set; }

        public static void OnException(string exceptionMessage)
        {
            LastMessage = exceptionMessage;
            System.Windows.Forms.MessageBox.Show(LastMessage);
        }
       
    }

