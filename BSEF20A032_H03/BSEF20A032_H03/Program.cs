using ADMIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Vehicle;
using Ride;
using Driver;

namespace BSEF20A032_H03
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Admin ad = new Admin();
             ad.copyDataFromDataBaseToList();
             ride obj = new ride();
             obj.bookRide();
             int index = obj.assignDriver(ad.DriversRecord);
             Console.Write(ad.DriversRecord[index].DriverName);*/
            /* string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=My Db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
              SqlConnection myConn = new SqlConnection(connectionString: conString);
              string query = "select * from Driver where name='pakistan'";
              SqlCommand cmd = new SqlCommand(query, myConn);
              myConn.Open();
              SqlDataReader dr = cmd.ExecuteReader();
              Console.WriteLine(dr.HasRows);
              while(dr.Read())
              {
                  Console.WriteLine($"Id: {dr[0]} Name: {dr["name"]}");
              }
              myConn.Close();*/
            /*string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=My Db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection sqlConn = new SqlConnection(connectionString: conString);
            vehicle obj = new vehicle();
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
            int id = 1002;
            string queryVeh = "UPDATE Vehicle SET ";
            if (vehicleType.Length != 0)
                queryVeh = queryVeh + $" vname='{vehicleType}' ";
            if (vehicleModel.Length != 0)
                queryVeh = queryVeh + $", vmodel='{vehicleModel}' ";
            if (vehicleNumber.Length != 0)
                queryVeh = queryVeh + $", vnumber='{vehicleNumber}' ";
            Console.WriteLine(queryVeh);
            queryVeh = queryVeh + $" where driverId='{id}'";
            Console.WriteLine(queryVeh);
            sqlConn.Open();
            SqlCommand cmd1 = new SqlCommand(queryVeh, sqlConn);
            int st = cmd1.ExecuteNonQuery();
            Console.WriteLine($"id: {st}");
            sqlConn.Close();*/
            /* try
            {*/

/*            try
            {
*/
                for (int i = 0; i < Console.WindowWidth / 2; i++)
                Console.Write("-");
                Console.Write('\n');
                Console.WriteLine("{0," + ((Console.WindowWidth / 4) + 8) + "}", "WELCOME TO MY RIDE.");
                for (int i = 0; i < Console.WindowWidth / 2; i++)
                    Console.Write("-");
                bool mainMenu = true;
                Admin adObj = new Admin();
                adObj.copyDataFromDataBaseToList();
                ride rideObj = new ride();
                while (mainMenu == true)
                {
                    Console.WriteLine("\n1. Book a Ride.");
                    Console.WriteLine("2. Enter as Driver.");
                    Console.WriteLine("3. Enter as Admin.");
                    Console.WriteLine("4. Exit.");

                    Console.ForegroundColor = ConsoleColor.Green;
                    int option = int.Parse(Console.ReadLine());
                    Console.ResetColor();
                    if (option == 1)
                    {
                        if (rideObj.bookRide() == false)
                            continue;
                        int index = rideObj.assignDriver(adObj.DriversRecord);
                        if (index == -1)
                        {
                            Console.WriteLine("Driver is not available.");
                            continue;
                        }
                        Console.WriteLine($"Happy Travel With !!! : {adObj.DriversRecord[index].DriverName}");
                        int rating = rideObj.giveRating();
                        adObj.DriversRecord[index].DriverRating = rating;
                        adObj.DriversRecord[index].updateRating(adObj.DriversRecord[index].DriverId);
                    }
                    else if (option == 3)
                    {
                        int optionForAdmin = 0;
                        bool mainAdmnMenu = true;
                        while (mainAdmnMenu == true)
                        {
                            bool status = true;
                            while (status == true)
                            {
                                Console.WriteLine("1. Add Driver.");
                                Console.WriteLine("2. Remove Driver.");
                                Console.WriteLine("3. Update Driver.");
                                Console.WriteLine("4. Search Driver.");
                                Console.WriteLine("5. Exit.");
                                Console.ForegroundColor = ConsoleColor.Green;
                                optionForAdmin = int.Parse(Console.ReadLine());
                                Console.ResetColor();
                                if (optionForAdmin == 1 || optionForAdmin == 2 || optionForAdmin == 3 || optionForAdmin == 4 || optionForAdmin == 5)
                                    status = false;
                            }
                            status = true;
                            if (optionForAdmin == 1)
                                if (adObj.addDriver() == true)
                                {
                                    Console.WriteLine("Successfully Added.");
                                    adObj.copyDataFromDataBaseToList();
                                }
                                else
                                    Console.WriteLine("Error!! Not Added!");
                            else if (optionForAdmin == 2)
                            {
                                Console.WriteLine(adObj.removeDriver());
                                adObj.copyDataFromDataBaseToList();
                            }
                            else if (optionForAdmin == 3)
                            {
                                Console.WriteLine(adObj.updateDriver());
                                adObj.copyDataFromDataBaseToList();
                            }
                            else if (optionForAdmin == 4)
                                adObj.searchDriver();
                            else if (optionForAdmin == 5)
                                mainAdmnMenu = false;
                        }
                    }
                    else if (option == 2)
                    {
                        bool mainDriverMenu = true;
                        adObj.copyDataFromDataBaseToList();
                        driver drivObj = adObj.verifyDriver();
                        if (drivObj.DriverId == -1)
                            Console.WriteLine("Wrong Id or Name.");
                        else
                            while (mainDriverMenu == true)
                            {
                                bool status = true;
                                int optionForDriver = 0;
                                while (status == true)
                                {
                                    Console.WriteLine("1. Change Availability.");
                                    Console.WriteLine("2. Change Location.");
                                    Console.WriteLine("3. Exit.");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    optionForDriver = int.Parse(Console.ReadLine());
                                    Console.ResetColor();
                                    if (optionForDriver == 1 || optionForDriver == 2 || optionForDriver == 3)
                                        status = false;
                                }
                                status = true;
                                if (optionForDriver == 1)
                                    drivObj.updateAvailability(drivObj.DriverId);
                                else if (optionForDriver == 2)
                                    drivObj.updateLocation(drivObj.DriverId);
                                else
                                    mainDriverMenu = false;
                            }
                    }
                    else
                    {
                        mainMenu = false;
                    }
                }
/*            }*/   
            /*catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error Occoured During Execution.\n" +
                    "1.May be you Entered wrong age.\n" +
                    "2.May you Entered words in phone number.\n" +
                    "3.May be you left an empty field while adding driver.\n" +
                    "4.May you Entered Wrong Gender.\n" +
                    "5.May be Your String length is smaller than 1.\n" +
                    "6.May you Entered Wrong Vehicle.");
            }*/
           /* string ratin = "1234";
            List<int> array = new List<int>();
            for (int i = 0; i < ratin.Length; i++)
            {
                array.Add(int.Parse(ratin[i].ToString()));
                Console.Write(array[i]);
            }*/
        }
    }
}
