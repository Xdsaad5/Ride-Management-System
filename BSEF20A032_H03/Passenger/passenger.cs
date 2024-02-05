using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passenger
{
    public class passenger
    {
        private string name;
        private string phoneNumber;
        public string PassengerName
        {
            get
            {
                return name;
            }
            set
            {
                if (value.Length < 1)
                    throw new Exception();
                name = value;
            }
        }
        public string PassengerPhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                if (value.All(Char.IsDigit) == false)
                    throw new Exception();
                phoneNumber = value;
            }
        }

    }
}
