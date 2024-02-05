using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Location;
using Vehicle;
using Microsoft.Data.SqlClient;
namespace Driver
{
    public class driver
    {
        private int id;
        private string name;
        private int age;
        private string gender;
        private string address;
        private string phoneNumber;
        private bool availability;
        private List<int> ratingList = new List<int>();
        private location currentLocation = new location();
        private vehicle vehicle = new vehicle();
        public int DriverId
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public vehicle DriverVehicle
        {
            get
            {
                return vehicle;
            }
            set
            {
                vehicle.VehicleType = value.VehicleType;
                vehicle.VehicleModel = value.VehicleModel;
                vehicle.VehicleNumber = value.VehicleNumber;
            }
        }
        public List<int> RatingList
        {
            get
            {
                return ratingList;
            }
            set
            {
                for (var i = 0; i < value.Count; i++)
                    ratingList.Add(value[i]);
            }
        }
        public int DriverRating
        {
            set
            {
                ratingList.Add(value);
            }
        }
        public location DriverLocation
        {
            get
            {
                return currentLocation;
            }
            set
            {
                currentLocation.LocationLatitude = value.LocationLatitude;
                currentLocation.LocationLongitude = value.LocationLongitude;
            }
        }
        public string DriverName
        {
            get
            {
                return name;
            }
            set
            {
                if (value.Length < 1)
                {
                    throw new Exception();
                }
                name = value;
            }
        }
        public int DriverAge
        {
            get
            {
                return age;
            }
            set
            {
                if (value <= 0)
                {
                    throw new Exception();
                }
                age = value;
            }
        }
        public string DriverGender
        {
            get
            {
                return gender;
            }
            set
            {
                if (value.ToLower() == "male" || value.ToLower() == "female" || value.ToLower() == "other")
                {
                    gender = value;
                    return;
                }
                throw new Exception();
            }
        }
        public string DriverAddress
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }
        public string DriverPhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                /*if (value.All(Char.IsDigit) == false)
                {
                    throw new Exception();
                }*/
                phoneNumber = value;
            }
        }
        public bool DriverAvailability
        {
            get
            {
                return availability;
            }
            set
            {
                availability = value;
            }
        }
        public void updateAvailability(int id)
        {
            Console.Write("Do you wanna change your Availability status?");
            Console.ForegroundColor = ConsoleColor.Green;
            string str = Console.ReadLine();
            Console.ResetColor();
            if (str.ToLower() == "no" || str.ToLower() == "n")
                return;
            Console.Write("Are you available?");
            Console.ForegroundColor = ConsoleColor.Green;
            str = Console.ReadLine();
            Console.ResetColor();
            int status = 0;
            if (str.ToLower() == "no" || str.ToLower() == "n")
                this.availability = false;
            else
            {
                this.availability = true;
                status = 1;
            }
            string query = $"Update Driver set availability='{status}' where Id='{id}'";
            string connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=My Db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection sqlConn = new SqlConnection(connectionString: connStr);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            int st = cmd.ExecuteNonQuery();
            sqlConn.Close();
        }
        public double getRating()
        {
            if (ratingList.Count == 0)
                return -1;
            return ratingList.Average();
        }
        public void updateLocation(int id)
        {
            Console.Write("Do you want to change your Location?");
            Console.ForegroundColor = ConsoleColor.Green;
            string str = Console.ReadLine();
            Console.ResetColor();
            if (str.ToLower() == "no" || str.ToLower() == "n")
                return;
            Console.Write("Where are you now? Your latitude: ");
            Console.ForegroundColor = ConsoleColor.Green;
            this.currentLocation.LocationLatitude = float.Parse(Console.ReadLine());
            Console.ResetColor();
            Console.Write("Your longitude: ");
            Console.ForegroundColor = ConsoleColor.Green;
            this.currentLocation.LocationLongitude = float.Parse(Console.ReadLine());
            Console.ResetColor();
            string query = $"Update Location set latititude='{this.currentLocation.LocationLatitude}', longitude='{this.currentLocation.LocationLongitude}' where driverId='{id}'";
            string connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=My Db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection sqlConn = new SqlConnection(connectionString: connStr);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            int st = cmd.ExecuteNonQuery();
            sqlConn.Close();
        }
        public void newAvailablility(int id)
        {
            string connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=My Db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection sqlConn = new SqlConnection(connectionString: connStr);
            sqlConn.Open();
            int status = 0;
            if (this.DriverAvailability == true)
                status = 1;
            string query = $"Update Driver set availability='{status}' where Id='{id}'";
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            cmd.ExecuteNonQuery();
            sqlConn.Close();
        }
        public void updateRating(int id)
        {
            string connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=My Db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection sqlConn = new SqlConnection(connectionString: connStr);
            sqlConn.Open();
            string query = $"select rating from Driver where Id='{id}'";
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            SqlDataReader dr = cmd.ExecuteReader();
            String rating = "";
            if(dr.HasRows==true)
            {
                dr.Read();
                rating = dr["rating"].ToString();
            }
            dr.Close();
            for (int i= 0; i < this.ratingList.Count;i++)
                rating = rating + this.ratingList[i].ToString();
            query = $"Update Driver set rating='{rating}' where Id='{id}'";
            SqlCommand cmd1 = new SqlCommand(query, sqlConn);
            cmd1.ExecuteNonQuery();
            sqlConn.Close();

        }
    }
}
