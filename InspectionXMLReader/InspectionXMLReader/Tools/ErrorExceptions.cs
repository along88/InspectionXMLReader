using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    static class ErrorExceptions
    {
        private static string LastMessage;
        public static void OnException(string exceptionMessage)
        {
            LastMessage = exceptionMessage;
            System.Windows.Forms.MessageBox.Show(LastMessage);
        }
    }

