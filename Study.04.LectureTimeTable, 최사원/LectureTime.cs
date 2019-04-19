using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Study._04.LectureTimeTable__최사원
{
    class LectureTime
    {

        string[,] timetableArr; // 시간표 배열
        int[,] lectureTimeCode; // 강의 코드 배열
        bool[] canTakeLecture; // 강의 수강 가능

        Utility utility;

        public LectureTime() {

            timetableArr = new string[Constants.TIMETABLE_COLUMN_RANGE, Constants.TIMETABLE_ROW_RANGE];
            lectureTimeCode = new int[Constants.LAST_COLUMN_OF_LECTURETABLE,Constants.SECOND_LECTURE_TIME+1];
            canTakeLecture = new bool[Constants.LAST_COLUMN_OF_LECTURETABLE];
            utility = new Utility();
        }


        /*------------수강 가능 bool 초기화------------*/

        public void ResetCanTakeLecture()  // 처음 초기화
        {
            for (int index=0; index<Constants.LAST_COLUMN_OF_LECTURETABLE;index++)
                canTakeLecture[index] = true;
          
        }

        public void SetIfAllLecturesCanTake(int indexToCheck, bool setValue) // 강의를 추가하거나 삭제할 때 모든 강의를 초기화 해준다.
        {
            for (int index = 1;index < Constants.LAST_COLUMN_OF_LECTURETABLE;index++)
            {
                if (index == indexToCheck)  //같은 인덱스일 경우 그 강의로 인해 전체 강의 수강가능 여부를 setValue로 바꿔야 하기 때문에 자신은 setValue로 바로 초기화.
                    canTakeLecture[index] = setValue;

                else if (canTakeLecture[index] == setValue) //바꿔야 하는 값과 똑같이 저장되어 있으면 바꿀 필요가 없음 
                    continue;

                else if (IsSameDayLecture(indexToCheck, index)) // 바꿔야 하는 값이랑 다르면 검사를 해준다.
                    canTakeLecture[index] = setValue;

            }
        }

        private bool IsSameTimeLecture(int lecture1TimeCode, int lecture2TimeCode)
        {
            //강의시작시간이 코드의 앞부분에 있기 때문에 강의 시작시간이 빠른 것을 lecture1TimeCode로 옮겨준다.
            if (lecture2TimeCode < lecture1TimeCode)
                utility.SwapInt(ref lecture1TimeCode, ref lecture2TimeCode);

            //그 후로 lecture 1이 끝나는 시간 보다 lecture2의 강의 시작시간이 빠르면 강의가 무조건 중복된다.

            int lecture1StartTime = lecture1TimeCode / 100;
            int lecture1LectureTime = lecture2TimeCode % 100;
            int lecture2StartTime = lecture2TimeCode / 100;

            if (lecture2StartTime < lecture1StartTime + lecture1LectureTime)
                return true;

            return false;
        }

        private bool IsSameDayLecture(int lecture1Index, int lecture2Index)
        {
            int[] lecture1 = {Constants.FIRST_LECTURE_TIME,Constants.SECOND_LECTURE_TIME,Constants.FIRST_LECTURE_TIME};  //for문으로 한번에 3개의 경우의 수를 확인 하기위해,
            int[] lecture2 = {Constants.FIRST_LECTURE_TIME, Constants.SECOND_LECTURE_TIME, Constants.SECOND_LECTURE_TIME};

            int lecture1DayCode=0;
            int lecture2DayCode = 0;

            for(int compare=0; compare<3; compare++)
            {
                lecture1DayCode = lectureTimeCode[lecture1Index,lecture1[compare]]/10000;  //뒤 4자리는 강의 시간 코드
                lecture2DayCode = lectureTimeCode[lecture2Index, lecture2[compare]] /10000; 

                if (lecture1DayCode != 0 && lecture2DayCode != 0) //하나라도 코드가 0이면 강의 시간이 0이므로 볼 필요 없음.
                    if (IsSameDayOneOfLectureTime(lecture1DayCode, lecture2DayCode))                                                                             //요일코드에 겹치는 부분이 존재한다면
                        if(IsSameTimeLecture(lectureTimeCode[lecture1Index, lecture1[compare]]%1000, lectureTimeCode[lecture2Index, lecture2[compare]] % 1000)) //시간코드도 겹치는 부분을 확인해야한다.
                        return true;
            }

            return false;
        }

        private bool IsSameDayOneOfLectureTime(int lecture1DayCode, int lecture2DayCode)
        {
            int sumOfLectureCode = lecture1DayCode + lecture2DayCode; // 두 강의의 요일 코드를 합쳤을 때
            return utility.NumberTwoCheck(sumOfLectureCode.ToString()); //2가 존재한다면 같은 날짜에 강의가 존재한다는 의미.
        }

        /*------------강의 코드 초기화 함수------------*/

        /// <summary>
        /// 코드 :  0  0   0  0  0                  00                                  00      (9자리)
        ///        요일                       <강의 시작시간>                     <강의 시간>
        ///        금 목 수 화 월          9:00 = 00 30분 마다 1 증가        강의 시간 30분 마다 증가
        ///   -> 다른 요일 코드랑  더해서      -> 시간표 인덱스 번호          ->시작시간 인덱스부터 얼만큼 자리를 차지하는 가.
        /// 2가 되는 자리가 있는 경우 중복검사      => 강의 시간 중복 검사시에 시작시간 + 강의시간을 더해서 유용하게 검사가능
        /// </summary>
        /// 

        public void ResetLectureTimeCode() 
        {
            Array lectureTable = LectureTableData.Instance.lectureTable;
            
            for(int line =Constants.START_COLUMN_OF_LECTURETABLE+1; line<=Constants.LAST_COLUMN_OF_LECTURETABLE; line++) //엑셀 Array는 2번째 줄 부터 강의 정보가 시작
            {
                if(lectureTable.GetValue(line, LectureTableData.Instance.lectureTimeRow)!= null)
                    SetLectureTimeCode(lectureTable.GetValue(line, LectureTableData.Instance.lectureTimeRow).ToString(),line-1); //하지만 저장은 index 1부터 저장할 것, 
            }
        }

        private void SetLectureTimeCode(string lectureTime,int column) //시간 코드
        {
            string[] lectureTimes = utility.SplitByComma(lectureTime); // ,를 기준으로 강의 시간이 2개인 경우 2개로 나눈다.
            for (int day = 0; day < lectureTimes.Length; day++)
            {
                lectureTimeCode[column, day]=GetDayTimeCode(lectureTimes[day]); //강의 시간 코드를 저장.
            }

        }

        private int GetDayTimeCode(string lectureTimeOfDay) // 실제 요일과 날짜 코드를 반환하는 함수
        {
            int lectureTimeCode=0; //처음은 0으로 초기화

            string[] dayAndTime = utility.SplitBySpace(lectureTimeOfDay); //공백기준으로 나눈다.

            for (int day = 0; day < dayAndTime.Length - 1; day++) //마지막 인덱스 바로 전 까지는 요일이 저장되어있다.
                lectureTimeCode+=GetDayCode(dayAndTime[day].Trim()); //모든 요일 코드를 더하기.

            int[] timeCode = GetTimeCode(dayAndTime[dayAndTime.Length - 1].Trim()); //마지막 인덱스의 시간코드로 기간 코드 Get/
        
            return  lectureTimeCode * 10000 + timeCode[Constants.LECTURESTARTTIME] * 100 + timeCode[Constants.LECTURETIME]; //모든 코드 합쳐서 반환한다.

        }

        private int GetDayCode(string day) //요일 코드 반환함수
        {
            Console.WriteLine(day);
            switch (day)
            {
                case "월": return Constants.MONDAY;
                case "화": return Constants.TUESDAY;
                case "수": return Constants.WEDNESDAY;
                case "목": return Constants.THURSDAY;
                case "금": return Constants.FRIDAY;
            }
            return 0;
        }

        private int[] GetTimeCode(string time) //시간 코드 반환함수
        {
            int[] timeCode = new int[2];
            time = Regex.Replace(time, @"\D", "");
            int startTime = int.Parse(time.Substring(0, 4));
            int finishTime = int.Parse(time.Substring(4, 4));

            finishTime -= 100;  //시간 부분에서 한시간을 빼서
            finishTime += 60;   // 60분을 더해줌. (시간으로 계산하기 위해)
            int lectureTime = finishTime - startTime; // 수강하는 시간

            timeCode[Constants.LECTURETIME] = 2 * (lectureTime / 100) + (lectureTime % 100) / 30 ;
            int baseTime = startTime - 900; // 9시를 0으로 기준해줌.
            timeCode[Constants.LECTURESTARTTIME] = 2 *( baseTime / 100) + (baseTime % 100) / 30;

            return timeCode;
        }


        /*------------반환 함수------------*/

        public bool CanTakeThisLecture(int index)
        {
            return canTakeLecture[index];
        }

    }
}
