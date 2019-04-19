using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Study._04.LectureTimeTable__최사원
{
    class Program
    {
        static void Main(string[] args)
        {
            LectureTime table = new LectureTime();
            table.ResetLectureTimeCode();
            table.ResetCanTakeLecture();
            
        }
    }
}
