using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Study._04.LectureTimeTable__최사원
{
    public class Utility
    {

        public Utility()
        { }

        public string[] SplitBySpace(string sentence)
        {
            string[] sentences = sentence.Split(' ');

            return sentences;
        }

        public string[] SplitByComma(string sentence)
        {
            string[] sentences = sentence.Split(',');
            return sentences;
        }


        public void SwapInt(ref int number1, ref int number2) //int 교체
        {
            int temp = number1;
            number1 = number2;
            number2 = temp;
        }

        public bool NumberTwoCheck(string sentence)
        {
            Regex numberTwoMatch = new Regex("(?=.*[2]).{1}");

            Match match = numberTwoMatch.Match(sentence);

            if (match.Success)
                return true;

            else
                return false;
        }



    }
}
