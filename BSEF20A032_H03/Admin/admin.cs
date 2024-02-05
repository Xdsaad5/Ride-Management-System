using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Driver;
using Vehicle;
using Microsoft.Data.SqlClient;
namespace ADMIN
{
    public class Admin
    {
        private List<driver> listOfDrivers = new List<driver>();
        SqlConnection sqlConn;
        public Admin()
        {
            string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=My Db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            sqlConn = new SqlConnection(connectionString: conString);
        }
        public List<driver> DriversRecord
        {
            
            get
            {
                return listOfDrivers;
            }
            set
            {
                foreach (var x in value)
                    listOfDrivers.Add(x);
            }
        }
        /*
         * getDriver
         used this function to take informaton from user/driver who wants to be registered in our system.
         */
        
        private driver getDriver()
        {
            driver drivObj = new driver();
            Console.Write("Enter Name: ");
            Console.ForegroundColor = ConsoleColor.Green;
            drivObj.DriverName = Console.ReadLine();
            Console.ResetColor();
            Console.Write("Enter Age: ");
            Console.ForegroundColor = ConsoleColor.Green;
            drivObj.DriverAge = int.Parse(Console.ReadLine());
            Console.ResetColor();
            Console.Write("Enter Gender: ");
            Console.ForegroundColor = ConsoleColor.Green;
            drivObj.DriverGender = Console.ReadLine();
            Console.ResetColor();
            Console.Write("Enter Address: ");
            Console.ForegroundColor = ConsoleColor.Green;
            drivObj.DriverAddress = Console.ReadLine();
            Console.ResetColor();
            Console.Write("Enter Vehicle Type: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string vehicleType = Console.ReadLine();
            drivObj.DriverVehicle.VehicleType = vehicleType;
            Console.ResetColor();
            Console.Write("Enter Vehicle Model: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string vehicleModel = Console.ReadLine();
            drivObj.DriverVehicle.VehicleModel = vehicleModel;
            Console.ResetColor();
            Console.Write("Enter Vehicle License Plate: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string vehicleNumber = Console.ReadLine();
            drivObj.DriverVehicle.VehicleNumber = vehicleNumber;
            Console.ResetColor();
            drivObj.DriverId = listOfDrivers.Count + 1;
            return drivObj;
        }
        public bool addDriver()
        {
            
            driver obj = getDriver();
            sqlConn.Open();
            int id;
            string query;
            {
                query = $"INSERT INTO Driver(name,age,gender,address) OUTPUT inserted.id VALUES('{obj.DriverName}',' {obj.DriverAge}','{obj.DriverGender}','{obj.DriverAddress}')";
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                id = (int)cmd.ExecuteScalar();
            }
            {
                query = $"Insert into Vehicle(vname,vmodel,vnumber,driverId) values('{obj.DriverVehicle.VehicleType}','{obj.DriverVehicle.VehicleModel}','{obj.DriverVehicle.VehicleNumber}','{id}')";
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                int status = cmd.ExecuteNonQuery();
            }
            {
                query = $"Insert into Location(driverId) values('{id}')";
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                int status = cmd.ExecuteNonQuery();
            }
            listOfDrivers.Add(obj);
            sqlConn.Close();
            return true;
        }
        /*
         * verifyDrviver
         This function I made just to verify a driver
         */
        public driver verifyDriver()
        {
            Console.Write("Enter ID: ");
            Console.ForegroundColor = ConsoleColor.Green;
            int id = int.Parse(Console.ReadLine());
            Console.ResetColor();
            Console.Write("Enter Name: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string name = Console.ReadLine();
            Console.ResetColor();
            if (listOfDrivers.Count > 0)
                for (int i = 0; i < listOfDrivers.Count; i++)
                    if (listOfDrivers[i].DriverId == id && listOfDrivers[i].DriverName == name)
                        return listOfDrivers[i];
            driver obj = new driver();
            obj.DriverId = -1;
            return obj;
        }
        private void updateVeh(string name,string model,string number, int id)
        {
            string queryVeh = "UPDATE Vehicle SET ";
            if (name.Length != 0)
            {
                queryVeh = queryVeh + $" vname='{name}' ";
                if (model.Length != 0 || number.Length != 0)
                    queryVeh = queryVeh + " , ";
            }
            if (model.Length != 0)
            {
                queryVeh = queryVeh + $" vmodel='{model}' ";
                if (number.Length != 0)
                    queryVeh = queryVeh + " , ";
            }
            if (number.Length != 0)
                queryVeh = queryVeh + $" vnumber='{number}' ";
            if (queryVeh.Length > 19)
            {
                queryVeh = queryVeh + $" where driverId='{id}'";
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(queryVeh, sqlConn);
                int st = cmd.ExecuteNonQuery();
                sqlConn.Close();
            }
        }
        public int aunthenticateDriver(int id)
        {
            string query = $"select * from Driver where Id='{id}'";
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            SqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.HasRows == false)
            {
                sqlConn.Close();
                return -1;
            }
            dataReader.Read();
            sqlConn.Close();
            return id;
        }
        public string updateDriver()
        {
            Console.Write("Enter Id: ");
            Console.ForegroundColor = ConsoleColor.Green;
            int id = int.Parse(Console.ReadLine());
            Console.ResetColor();
            if (aunthenticateDriver(id) == -1)
                return "Driver Not Found.\n";
            Console.WriteLine($"----------Driver With Id {id} Exist.---------");
            Console.Write("Enter Name: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string name = Console.ReadLine();
            Console.ResetColor();
            Console.Write("Enter Age: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string tempAge = Console.ReadLine();
            Console.ResetColor();
            Console.Write("Enter Gender: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string gender = Console.ReadLine();
            Console.ResetColor();
            Console.Write("Enter Address: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string address = Console.ReadLine();
            Console.ResetColor();
            Console.Write("Enter Vehicle Type: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string vehicleType = Console.ReadLine();
            Console.ResetColor();
            Console.Write("Enter Vehicle Model: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string vehicleModel = Console.ReadLine();
            Console.ResetColor();
            Console.Write("Enter Vehicle License Plate: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string vehicleNumber = Console.ReadLine();
            Console.ResetColor();
            string query = "UPDATE DRIVER SET ";
            Console.WriteLine(query.Length);
            if (name.Length != 0)
            {
                query = query + $"name='{name}'";
                if (tempAge.Length != 0 || gender.Length != 0 || address.Length != 0)
                    query = query + " , ";
            }
            if (tempAge.Length != 0)
            {     query = query + $" age='{int.Parse(tempAge)}'";
                if (gender.Length != 0 || address.Length != 0)
                    query = query + " , ";
            }
            if (gender.Length != 0)
            {
                query = query + $"gender='{gender}'";
                if (address.Length != 0)
                    query = query + " , ";
            }
            if (address.Length != 0)
                query = query + $" address='{address}'";
            if (query.Length > 18)
            {
                query = query + $" where Id='{id}'";
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                int st = cmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            Console.WriteLine(id);
            updateVeh(vehicleType, vehicleModel, vehicleNumber, id);
            return ("Updated Successfully.");
        }
        /*public int searchIndex(int id)
        {
            for (int i = 0; i < listOfDrivers.Count; i++)
                if (listOfDrivers[i].DriverId == id)
                    return i;
            return -1;
        }*/
        public string removeDriver()
        {
            Console.Write("Enter ID: ");
            Console.ForegroundColor = ConsoleColor.Green;
            int id = int.Parse(Console.ReadLine());
            Console.ResetColor();
            if (this.aunthenticateDriver(id) == -1)
                return "Driver Not Found.\n";
            sqlConn.Open();
            string query;
            {
                query = $"delete from Location where driverId='{id}'";
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                int st = cmd.ExecuteNonQuery();
            }
            {
                query = $"delete from Vehicle where driverId='{id}'";
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                int st = cmd.ExecuteNonQuery();
            }
            {
                query = $"delete from Driver where Id='{id}'";
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                int st = cmd.ExecuteNonQuery();
            }
            sqlConn.Close();
            return "Successfully Deleted.\n";
        }
        /*
 * Search Logic:
 I wanna explain my search logic I assume if admin enters id then we know that id must be unique so no other driver have that
id so I varify that if and if it exist I only show admin that specific driver whose id he entered but if he don't enter id then 
I search in driver list the fields he entered I show him all those drivers whose fields match with the fields he entered.
 
 */
        private void copyVehicle()
        {
            string query = $"select * from Vehicle";
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == false)
            {
                sqlConn.Close();
                return;
            }
            while (dr.Read())
            {
                for (int j = 0; j < listOfDrivers.Count; j++)
                {
                    if (listOfDrivers[j].DriverId == int.Parse(dr[4].ToString()))
                    {
                        listOfDrivers[j].DriverVehicle.VehicleType = dr["vname"].ToString();
                        listOfDrivers[j].DriverVehicle.VehicleModel = dr["vmodel"].ToString();
                        listOfDrivers[j].DriverVehicle.VehicleNumber = dr["vnumber"].ToString();
                    }
                }
            }
            dr.Close();
            sqlConn.Close();
        }
        private void copyLocation()
        {
            string query = $"select * from Location";
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == false)
            {
                sqlConn.Close();
                return;
            }
            while (dr.Read())
            {
                for (int j = 0; j < listOfDrivers.Count; j++)
                    if (listOfDrivers[j].DriverId == int.Parse(dr[3].ToString()))
                    {
                        if (dr[1].ToString().Length != 0)
                            listOfDrivers[j].DriverLocation.LocationLatitude = float.Parse(dr[1].ToString());
                        if (dr["longitude"].ToString().Length != 0)
                            listOfDrivers[j].DriverLocation.LocationLongitude = float.Parse(dr["longitude"].ToString());
                    }
            }
            dr.Close();
            sqlConn.Close();
        }
        private void copyDriver()
        {
            string query = $"select * from Driver";
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == false)
            {
                sqlConn.Close();
                return;
            }
            driver obj = new driver();
            int i = 0;
            List<int> tempList = new List<int>();
            while (dr.Read())
            {
                listOfDrivers.Add(new driver());
                listOfDrivers[i].DriverId = int.Parse(dr["Id"].ToString());
                listOfDrivers[i].DriverName = dr["Name"].ToString();
                listOfDrivers[i].DriverAge = int.Parse(dr["age"].ToString());
                listOfDrivers[i].DriverGender = dr["gender"].ToString();
                listOfDrivers[i].DriverAddress = dr["address"].ToString();
                string rating = dr["rating"].ToString();
                /*if (rating.Length != 0)
                {
                    for (int j = 0; j < rating.Length; j++)
                        tempList.Add(int.Parse(rating[j].ToString()));
                    listOfDrivers[i].RatingList = tempList;
                }*/
                if (dr["availability"].ToString().Length != 0)
                    listOfDrivers[i].DriverAvailability = Convert.ToBoolean(int.Parse(dr["availability"].ToString()));
                i++;
            }
            sqlConn.Close();

        }
        public void  copyDataFromDataBaseToList()
        {
            if (listOfDrivers.Count > 0)
                listOfDrivers.Clear();
            copyDriver();
            copyVehicle();
            copyLocation();
        }
        public void searchDriver()
        {
            Console.Write("Enter Id: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string id = (Console.ReadLine());
            Console.ResetColor();
            int index = 0;
            if (id.Length != 0)
                for (int i = 0; i < listOfDrivers.Count; i++)
                    if (listOfDrivers[i].DriverId == int.Parse(id))
                        index = i;
            if (id.Length != 0)
                Console.WriteLine($"----------Driver With Id {id} Exist.---------");
            Console.Write("Enter Name: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string name = Console.ReadLine();
            Console.ResetColor();
            Console.Write("Enter Age: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string tempAge = Console.ReadLine();
            Console.ResetColor();
            Console.Write("Enter Gender: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string gender = Console.ReadLine();
            Console.ResetColor();
            Console.Write("Enter Address: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string address = Console.ReadLine();
            Console.ResetColor();
            Console.Write("Enter Vehicle Type: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string vehicleType = Console.ReadLine();
            Console.ResetColor();
            Console.Write("Enter Vehicle Model: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string vehicleModel = Console.ReadLine();
            Console.ResetColor();
            Console.Write("Enter Vehicle License Plate: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string vehicleNumber = Console.ReadLine();
            Console.ResetColor();
            List<driver> matchDrivers = new List<driver>();
            if (id.Length == 0)
                foreach (var x in listOfDrivers)
                {
                    if (name.Length != 0)
                        if (x.DriverName == name)
                        {
                            matchDrivers.Add(x);
                            continue;
                        }
                    if (tempAge.Length != 0)
                        if (x.DriverAge == int.Parse(tempAge))
                        {
                            matchDrivers.Add(x);
                            continue;
                        }
                    if (gender.Length != 0)
                        if (x.DriverGender == gender)
                        {
                            matchDrivers.Add(x);
                            continue;
                        }
                    if (address.Length != 0)
                        if (address == x.DriverAddress)
                        {
                            matchDrivers.Add(x);
                            continue;
                        }
                    if (vehicleType.Length != 0)
                        if (vehicleType == x.DriverVehicle.VehicleType)
                        {
                            matchDrivers.Add(x);
                            continue;
                        }
                    if (vehicleModel.Length != 0)
                        if (vehicleModel == x.DriverVehicle.VehicleModel)
                        {
                            matchDrivers.Add(x);
                            continue;
                        }
                    if (vehicleNumber.Length != 0)
                        if (vehicleNumber == x.DriverVehicle.VehicleNumber)
                        {
                            matchDrivers.Add(x);
                            continue;
                        }
                }
            else
                matchDrivers.Add(listOfDrivers[index]);
            Console.WriteLine(matchDrivers[0].DriverName);
            if (matchDrivers.Count == 0)
            {
                Console.WriteLine("No Such Driver Found.");
                return;
            }
            this.displayDrivers(matchDrivers);

        }
        /*
         Functon to display the drivers which have same records as entered by admin in search function.
         */
        private void displayDrivers(List<driver> matchDrivers)
        {
            if (listOfDrivers.Count == 0)
                return;
            for (int i = 0; i < Console.WindowWidth / 1.2; i++)
                Console.Write("-");
            Console.Write("\n\n");
            Console.WriteLine($"Name\t\t Age\t\t Gender\t\t V.Type\t\t V.Model\t V.License\t\t");
            Console.Write('\n');
            for (int i = 0; i < Console.WindowWidth / 1.2; i++)
                Console.Write("-");
            Console.Write('\n');
            foreach (var x in matchDrivers)
                Console.WriteLine($"{x.DriverName} \t\t {x.DriverAge}\t\t {x.DriverGender}\t\t  {x.DriverVehicle.VehicleType}\t\t " +
                    $" {x.DriverVehicle.VehicleModel}\t\t  {x.DriverVehicle.VehicleNumber}");
            Console.Write('\n');
            for (int i = 0; i < Console.WindowWidth / 1.2; i++)
                Console.Write("-");
            Console.Write('\n');
        }
        public void displayDrivers()
        {
            if (listOfDrivers.Count == 0)
                return;
            for (int i = 0; i < Console.WindowWidth / 1.2; i++)
                Console.Write("-");
            Console.Write("\n\n");
            Console.WriteLine($"Name\t\t Age\t\t Gender\t\t V.Type\t\t V.Model\t V.License\t\t");
            Console.Write('\n');
            for (int i = 0; i < Console.WindowWidth / 1.2; i++)
                Console.Write("-");
            Console.Write('\n');
            foreach (var x in listOfDrivers)
                Console.WriteLine($"{x.DriverName} \t\t {x.DriverAge}\t\t {x.DriverGender}\t\t  {x.DriverVehicle.VehicleType}\t\t " +
                    $" {x.DriverVehicle.VehicleModel}\t\t  {x.DriverVehicle.VehicleNumber}");
            Console.Write('\n');
            for (int i = 0; i < Console.WindowWidth / 1.2; i++)
                Console.Write("-");
            Console.Write('\n');
        }
        
    }
}
