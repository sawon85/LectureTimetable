using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study._04.LectureTimeTable__최사원
{
    static class Constants
    {
        /*---------------Lecture Table---------------*/

        public const string FILELOCATION = @"\\2019-1학기 강의시간표(class schedule)-190207.xlsx";
        public const string SHEET_NAME = "Sheet1";

        public const string START_CELL_OF_LECTURETABLE = "A1";
        public const int START_ROW_OF_LECTURETABLE = 1;
        public const int START_COLUMN_OF_LECTURETABLE = 1;

        public const string LAST_CELL_OF_LECTURETABLE = "L180";
        public const int LAST_ROW_OF_LECTURETABLE = 12;
        public const int LAST_COLUMN_OF_LECTURETABLE = 180;

        public const string MAJOR = "개설학과전공";
        public const string COURSE_ID = "학수번호";
        public const string CLASS_NUMBER = "분반";
        public const string COURSE_TITLE = "교과목명";
        public const string COMPLETION_TYPE = "이수\n구분";
        public const string SEMESTER = "학년";
        public const string CREDIT = "학점";
        public const string LECTURE_TIME = "요일 및 강의시간";
        public const string LECTUREROOM = "강의실";
        public const string PROFESSOR = "메인\n교수명";
        public const string LANGUAGE = "강의언어";

        /*---------------Time Table---------------*/

        public const int TIMETABLE_ROW_RANGE = 6;
        public const int TIMETABLE_COLUMN_RANGE = 22;

        public const int FIRST_LECTURE_TIME = 0;
        public const int SECOND_LECTURE_TIME = 1;

        public const int MONDAY = 1;
        public const int TUESDAY = 10;
        public const int WEDNESDAY = 100;
        public const int THURSDAY = 1000;
        public const int FRIDAY = 10000;

        public const int LECTURETIME = 0;
        public const int LECTURESTARTTIME = 1;

        /*---------------Register for courses---------------*/

        public const bool TAKE_LECTURE = false;
        public const bool DROP_LECTURE = true;

    }
}