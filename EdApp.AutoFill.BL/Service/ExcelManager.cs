using System;
using System.IO;
using EdApp.AutoFill.BL.Contract.Services;
using Microsoft.Office.Interop.Excel;
using Range = Microsoft.Office.Interop.Excel.Range;

namespace EdApp.AutoFill.BL.Service
{

    /// <inheritdoc cref="IExcel"/>>
    public class ExcelManager : IExcel
    {  
        protected Application Application { get; private set; }

        public Workbook Workbook { get; private set; }

        public Worksheet Worksheet { get; private set; }

        public IExcel OpenWorkbook(string fullFilePath)
        {
            if (!File.Exists(fullFilePath))
            {
                throw new ArgumentOutOfRangeException(nameof(fullFilePath),
                    $"File with full '{fullFilePath}' path is not found.");
            }
            Application = new Application();
            Workbook = Application.Workbooks.Open(fullFilePath);
            return this;
        }

        void IExcel.SelectWorksheet(int worksheetIndex)
        {
            Worksheet = Workbook.Worksheets[worksheetIndex];
        }

        public string GetCellValue(int rowNumber, int columnNumber)
        {
            return (Worksheet.Cells[rowNumber, columnNumber] as Range).Value?.ToString().Trim() ??
                string.Empty;
        }

        // Clear resources.
        public void Dispose()
        {
            Worksheet = null;
            Workbook.Close();
            Workbook = null;
            Application.Quit();
            Application = null;
        }
    }
}