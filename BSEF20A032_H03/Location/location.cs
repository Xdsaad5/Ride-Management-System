using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Location
{
    public class location
    {
        private float latitude;
        private float longitude;
        public float LocationLatitude
        {
            get
            {
                return latitude;
            }
            set
            {
                latitude = value;
            }
        }
        public float LocationLongitude
        {
            get
            {
                return longitude;
            }
            set
            {
                longitude = value;
            }
        }
        public void setLocation(float latitude, float longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }
    }
}
