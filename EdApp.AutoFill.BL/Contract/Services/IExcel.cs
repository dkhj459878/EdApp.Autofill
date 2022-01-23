using System;
using Microsoft.Office.Interop.Excel;

namespace EdApp.AutoFill.BL.Contract.Services
{
    /// <summary>
    /// Facilitates work with Excel workbook.
    /// </summary>
    public interface IExcel : IDisposable
    {
        /// <summary>
        /// Represents currently opened workbook.
        /// </summary>
        Workbook Workbook { get; }

        /// <summary>
        /// Represents currently selected worksheet.
        /// </summary>
        Worksheet Worksheet { get; }

        /// <summary>
        /// Opens excel workbook.
        /// </summary>
        /// <param name="fullFilePath">Full file path.</param>
        IExcel OpenWorkbook(string fullFilePath);

        /// <summary>
        /// Selects worksheet based on its index.
        /// </summary>
        /// <param name="worksheetIndex">Index of selecting worksheet (starts from 1).</param>
        void SelectWorksheet(int worksheetIndex);

        /// <summary>
        /// Retrieves cell value.
        /// </summary>
        /// <param name="rowNumber">Row number (starts from 1).</param>
        /// <param name="columnNumber">Column number (starts from 1).</param>
        /// <returns></returns>
        string GetCellValue(int rowNumber, int columnNumber);
    }
}