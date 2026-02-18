using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace BusinessLogic.Services.Printing

{
    public class WordToPdfConverter
    {
        public void Convert(string docxPath, string pdfPath)
        {
            var wordApp = new Word.Application();
            Word.Document? document = null;

            try
            {
                document = wordApp.Documents.Open(docxPath);
                document.ExportAsFixedFormat(
                    pdfPath,
                    Word.WdExportFormat.wdExportFormatPDF);
            }
            finally
            {
                document?.Close(false);
                wordApp.Quit();
            }
        }
    }
}
