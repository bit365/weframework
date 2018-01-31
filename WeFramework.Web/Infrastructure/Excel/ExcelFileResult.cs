using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace WeFramework.Web.Infrastructure
{
    public class ExcelFileResult<TModel> : FileResult where TModel : class
    {
        public IEnumerable<TModel> Model { get; private set; }

        public ExcelFileResult(IEnumerable<TModel> model, string fileName = null) : base("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            Model = model;
            fileName = string.IsNullOrWhiteSpace(fileName) ? Path.GetRandomFileName() : fileName;
            FileDownloadName = Path.ChangeExtension(fileName, "xlsx");
        }

        protected override void WriteFile(HttpResponseBase response)
        {
            byte[] buffer = GenerateExcel();
            response.OutputStream.Write(buffer, 0, buffer.Length);
        }

        private byte[] GenerateExcel()
        {
            var excelSheetAttribute = typeof(TModel).GetCustomAttribute<ExcelSheetAttribute>(false) ?? new ExcelSheetAttribute();

            var properties = typeof(TModel).GetProperties().Where(p => !p.IsDefined(typeof(ExcelIgnoreAttribute)));

            var propertyDictionary = properties.Select(property => new { Property = property, ExcelColumnAttribute = property.GetCustomAttribute<ExcelColumnAttribute>() ?? new ExcelColumnAttribute() })
                .OrderBy(o => o.ExcelColumnAttribute.Sort).ToDictionary(o => o.Property, o => o.ExcelColumnAttribute);

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add(excelSheetAttribute.Name);
                GenerateExcelHeader(excelWorksheet, excelSheetAttribute, propertyDictionary);
                GenerateExcelBody(excelWorksheet, excelSheetAttribute, propertyDictionary);
                FormatExcelSheet(excelWorksheet, excelSheetAttribute, propertyDictionary);

                return excelPackage.GetAsByteArray();
            }
        }

        private void FormatExcelSheet(ExcelWorksheet excelWorksheet, ExcelSheetAttribute excelSheetAttribute, Dictionary<PropertyInfo, ExcelColumnAttribute> propertyDictionary)
        {
            PropertyInfo[] propertyInfoArray = propertyDictionary.Select(pd => pd.Key).ToArray();

            excelWorksheet.Cells.AutoFitColumns();

            for (int col = 1; col <= propertyInfoArray.Length; col++)
            {
                PropertyInfo currentProperyInfo = propertyInfoArray[col - 1];
                ExcelColumnAttribute excelColumnAttribute = propertyDictionary[currentProperyInfo];
                ExcelColumn currentExcelColumn = excelWorksheet.Column(col);
                currentExcelColumn.Style.Numberformat.Format = excelColumnAttribute.Format;
                currentExcelColumn.Style.QuotePrefix = excelColumnAttribute.Quote;
                currentExcelColumn.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                currentExcelColumn.Style.HorizontalAlignment = excelColumnAttribute.IsCenter ? ExcelHorizontalAlignment.Center : ExcelHorizontalAlignment.General;
                currentExcelColumn.Width += 3;
            }

            excelWorksheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            excelWorksheet.Row(1).Style.Font.Bold = true;

        }

        private void GenerateExcelHeader(ExcelWorksheet excelWorksheet, ExcelSheetAttribute excelSheetAttribute, Dictionary<PropertyInfo, ExcelColumnAttribute> propertyDictionary)
        {
            PropertyInfo[] propertyInfoArray = propertyDictionary.Select(pd => pd.Key).ToArray();

            for (int col = 1; col <= propertyInfoArray.Length; col++)
            {
                PropertyInfo currentProperyInfo = propertyInfoArray[col - 1];

                DisplayAttribute displayAttribute = currentProperyInfo.GetCustomAttribute<DisplayAttribute>();

                DisplayNameAttribute displayNameAttribute = currentProperyInfo.GetCustomAttribute<DisplayNameAttribute>();

                string displayName = displayAttribute?.GetName() ?? displayNameAttribute?.DisplayName ?? currentProperyInfo.Name;

                ExcelRange currentCell = excelWorksheet.Cells[1, col];
                currentCell.Value = displayName;
            }

            excelWorksheet.Row(1).Height = excelSheetAttribute.RowHeight;
        }

        private void GenerateExcelBody(ExcelWorksheet excelWorksheet, ExcelSheetAttribute excelSheetAttribute, Dictionary<PropertyInfo, ExcelColumnAttribute> propertyDictionary)
        {
            TModel[] modelArray = Model.ToArray();
            PropertyInfo[] propertyInfoArray = propertyDictionary.Select(pd => pd.Key).ToArray();

            for (int row = 2; row <= modelArray.Length + 1; row++)
            {
                TModel currentModel = modelArray[row - 2];
                for (int col = 1; col <= propertyInfoArray.Length; col++)
                {
                    PropertyInfo currentProperyInfo = propertyInfoArray[col - 1];
                    ExcelColumnAttribute excelColumnAttribute = propertyDictionary[currentProperyInfo];
                    ExcelRange currentCell = excelWorksheet.Cells[row, col];
                    var displayFormatAttribute = currentProperyInfo.GetCustomAttribute<DisplayFormatAttribute>();
                    if (displayFormatAttribute != null && typeof(IFormattable).IsAssignableFrom(currentProperyInfo.PropertyType))
                    {
                        currentCell.Value = ((IFormattable)currentProperyInfo.GetValue(currentModel)).ToString(displayFormatAttribute.DataFormatString, CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        currentCell.Value = currentProperyInfo.GetValue(currentModel);
                    }
                }

                excelWorksheet.Row(row).Height = excelSheetAttribute.RowHeight;
            }
        }
    }
}
