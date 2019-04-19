using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Study._04.LectureTimeTable__최사원
{
    /*--------------싱글톤--------------*/

    class LectureTableData
    {
        private static LectureTableData instance = null;
        public static LectureTableData Instance {

            get
            {
                if (instance == null)
                {
                    instance = new LectureTableData();
                }

                return instance;
            }
        }

        Excel.Application ExcelApp;
        Excel.Workbook workbook;
        Excel.Sheets sheets;
        Excel.Worksheet lectureTableWroksheet;
        Excel.Range lectureTableRange;
        public Array lectureTable { get; set; }

        public int majorRow { get; set; }
        public int courseIDRow { get; set; }
        public int classNumberRow { get; set; }
        public int courseTitleRow { get; set; }
        public int completionTypeRow { get; set; }
        public int semesterRow { get; set; }
        public int creditRow { get; set; }
        public int lectureTimeRow { get; set; }
        public int lectureRoomRow { get; set; }
        public int professorRow { get; set; }
        public int languageRow { get; set; }

       

        private LectureTableData()
        {
            ExcelApp = new Excel.Application();
            workbook = ExcelApp.Workbooks.Open(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + Constants.FILELOCATION);
            sheets = workbook.Sheets;
            lectureTableWroksheet = sheets[Constants.SHEET_NAME] as Excel.Worksheet;
            lectureTableRange = lectureTableWroksheet.get_Range(Constants.START_CELL_OF_LECTURETABLE, Constants.LAST_CELL_OF_LECTURETABLE) as Excel.Range;
            lectureTable = lectureTableRange.Cells.Value2;
            SetRowNumber();
        }
        ~LectureTableData()
        {
            ExcelApp.Workbooks.Close();
            ExcelApp.Quit();
        }

        void SetRowNumber()
        {
            for (int line = Constants.START_ROW_OF_LECTURETABLE; line <= Constants.LAST_ROW_OF_LECTURETABLE; line++)
            {
                switch(lectureTable.GetValue(Constants.START_COLUMN_OF_LECTURETABLE,line))
                {
                    case Constants.MAJOR:
                        majorRow = line;
                        break;

                    case Constants.COURSE_ID:
                        courseIDRow = line;
                        break;
                    case Constants.CLASS_NUMBER:
                        classNumberRow = line;
                        break;
                    case Constants.COURSE_TITLE:
                        courseTitleRow = line;
                        break;
                    case Constants.COMPLETION_TYPE:
                        completionTypeRow = line;
                        break;
                    case Constants.SEMESTER:
                        semesterRow = line;
                        break;
                    case Constants.CREDIT:
                        creditRow = line;
                        break;
                    case Constants.LECTURE_TIME:
                        lectureTimeRow = line;
                        break;
                    case Constants.LECTUREROOM:
                        lectureRoomRow = line;
                        break;
                    case Constants.PROFESSOR:
                        professorRow = line;
                        break;
                    case Constants.LANGUAGE:
                        languageRow = line;
                        break;

                }
            }
        }

    }
}
