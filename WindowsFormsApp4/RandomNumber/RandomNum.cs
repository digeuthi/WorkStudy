using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RandomNumber
{
    public class RandomNum
    {
        /*  static void Main(string[] args) {

              string randomNum = GenerateRandomNum(10);

          }*/

        public string GenerateRandomNum(int length)
        {
            Random random = new Random();
            List<int> list = new List<int>();
            StringBuilder sb = new StringBuilder();

            int randomValue;

            for (int i = 0; i < length; i++)
            {
                randomValue = random.Next(10);
                sb.Append(randomValue);

                //list.Add(randomValue);
            }

            //            return string.Join("", list);
            return sb.ToString();
        }
    }
}
