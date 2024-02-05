using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle
{
    public class vehicle
    {
        private string type;
        private string model;
        private string licensePlate;
        public string VehicleType
        {
            get
            {
                return type;
            }
            set
            {
                if (value.Length < 1)
                    throw new Exception();
                if (value.ToLower() == "car" || value.ToLower() == "bike" || value.ToLower() == "rickshaw")
                    type = value;
                else
                    throw new Exception();
            }
        }
        public string VehicleModel
        {
            get
            {
                return model;
            }
            set
            {
                if (value.Length < 1)
                    throw new Exception();
                model = value;
            }
        }
        public string VehicleNumber
        {
            get
            {
                return licensePlate;
            }
            set
            {
                if (value.Length < 1)
                    throw new Exception();
                licensePlate = value;
            }
        }
    }
}
