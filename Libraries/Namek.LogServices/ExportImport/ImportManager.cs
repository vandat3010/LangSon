using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CDKTTCTN.Core.Config;
using CDKTTCTN.Core.Entity;
using CDKTTCTN.Services.StudyResults;
using OfficeOpenXml;

namespace CDKTTCTN.Services.ExportImport
{
    public class ImportManager : IImportManager
    {
        private IStudyResultService _studyResultService;

        public ImportManager(IStudyResultService studyResultService)
        {
            _studyResultService = studyResultService;
        }

        #region Utilities

        protected virtual int GetColumnIndex(string[] properties, string columnName)
        {
            if (properties == null)
                throw new ArgumentNullException("properties");

            if (columnName == null)
                throw new ArgumentNullException("columnName");

            for (int i = 0; i < properties.Length; i++)
                if (properties[i].Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return i + 1; //excel indexes start from 1
            return 0;
        }

        protected virtual string ConvertColumnToString(object columnValue)
        {
            if (columnValue == null)
                return null;

            return Convert.ToString(columnValue);
        }

        protected virtual string GetMimeTypeFromFilePath(string filePath)
        {
            var mimeType = MimeMapping.GetMimeMapping(filePath);

            //little hack here because MimeMapping does not contain all mappings (e.g. PNG)
            if (mimeType == MimeTypes.ApplicationOctetStream)
                mimeType = MimeTypes.ImageJpeg;

            return mimeType;
        }


        #endregion
        #region Methods

        /// <summary>
        /// Import  from XLSX file
        /// </summary>
        /// <param name="stream">Stream</param>      
        public virtual void ImportStudyResultFromXlsx(Stream stream)
        {
            var start = DateTime.Now;
            Trace.WriteLine(start);

            //property array
            var properties = new[]
            {
                new PropertyByName<Study_result>(StudyResultCaption.subjects),
                new PropertyByName<Study_result>(StudyResultCaption.semester),
                new PropertyByName<Study_result>(StudyResultCaption.student_code),
                new PropertyByName<Study_result>(StudyResultCaption.student_firstname),
                new PropertyByName<Study_result>(StudyResultCaption.student_lastname),
                new PropertyByName<Study_result>(StudyResultCaption.date_of_birth),
                new PropertyByName<Study_result>(StudyResultCaption.class_code),
                new PropertyByName<Study_result>(StudyResultCaption.mark),
                new PropertyByName<Study_result>(StudyResultCaption.note),
            };

            var manager = new PropertyManager<Study_result>(properties);

            using (var xlPackage = new ExcelPackage(stream))
            {
                // get the first worksheet in the workbook
                var worksheet = xlPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    throw new Exception("No worksheet found");

                var iRow = 2;
                List< Study_result > studyResults = new List<Study_result>();
                while (true)
                {
                    var allColumnsAreEmpty = manager.GetProperties
                        .Select(property => worksheet.Cells[iRow, property.PropertyOrderPosition])
                        .All(cell => cell == null || cell.Value == null || String.IsNullOrEmpty(cell.Value.ToString()));

                    if (allColumnsAreEmpty)
                        break;

                    manager.ReadFromXlsx(worksheet, iRow);

                    //var student_code = manager.GetProperty("student_code").StringValue;

                    //var studyResult = _studyResultService.FirstOrDefault(
                    //    x => x.student_code == student_code);

                    //var isNew = studyResult == null;

                    //studyResult = studyResult ?? new Study_result();
                    var studyResult = new Study_result
                    {
                        subjects = manager.GetProperty(StudyResultCaption.subjects).StringValue,
                        semester = manager.GetProperty(StudyResultCaption.semester).StringValue,
                        student_code = manager.GetProperty(StudyResultCaption.student_code).StringValue,
                        student_firstname = manager.GetProperty(StudyResultCaption.student_firstname).StringValue,
                        student_lastname = manager.GetProperty(StudyResultCaption.student_lastname).StringValue,
                        date_of_birth = manager.GetProperty(StudyResultCaption.date_of_birth).DateTimeNullable,
                        class_code = manager.GetProperty(StudyResultCaption.class_code).StringValue,
                        mark = manager.GetProperty(StudyResultCaption.mark).DoubleValueWithComma,
                        note = manager.GetProperty(StudyResultCaption.note).StringValue
                    };
                    
                    studyResults.Add(studyResult);
                    iRow++;
                }// end while

                _studyResultService.Insert(studyResults);
            }
            Trace.WriteLine(DateTime.Now - start);
        }

        #endregion
    }
}
