using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study._04.LectureTimeTable__최사원
{
    class UserVO
    {
        List<int> myLecture { get; set; }
        List<int> shoppingBasket { get; set;}

        public UserVO()
        {
            myLecture = new List<int>();
            shoppingBasket = new List<int>();
        }
    }
}
