using Driver;
using Location;
using Passenger;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ride
{
    public class ride
    {
        private location startLocation = new location();
        private location endLocation = new location();
        private int price;
        private driver driver = new driver();
        private passenger passenger = new passenger();
        public location RideStartLocation
        {
            get
            {
                return startLocation;
            }
            set
            {
                startLocation.LocationLatitude = value.LocationLatitude;
                startLocation.LocationLongitude = value.LocationLongitude;
            }
        }
        public location RideEndLocation
        {
            get
            {
                return endLocation;
            }
            set
            {
                endLocation.LocationLatitude = value.LocationLatitude;
                endLocation.LocationLongitude = value.LocationLongitude;
            }
        }
        public int RidePrice
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }
        public driver RideDriver
        {
            get
            {
                return driver;
            }
            set
            {
                driver.DriverName = value.DriverName;
                driver.DriverAge = value.DriverAge;
                driver.DriverGender = value.DriverGender;
                driver.DriverAddress = value.DriverAddress;
                driver.DriverPhoneNumber = value.DriverPhoneNumber;
                driver.DriverLocation = value.DriverLocation;
                driver.DriverAvailability = value.DriverAvailability;
                driver.DriverVehicle = value.DriverVehicle;
            }
        }
        public passenger RidePassenger
        {
            get
            {
                return passenger;
            }
            set
            {
                passenger = value;
            }
        }
        public bool assignPassenger(string name, string phoneNumber)
        {
            Console.Write("Do you want to Book a Ride? ");
            Console.ForegroundColor = ConsoleColor.Green;
            string option = Console.ReadLine();
            Console.ResetColor();
            if (option.ToLower() == "no" || option.ToLower() == "n")
            {
                Console.WriteLine("ok See you again!");
                return false;
            }
            this.passenger.PassengerName = name;
            this.passenger.PassengerPhoneNumber = phoneNumber;
            return true;
        }
        public int calculatePrice()
        {
            double distance = Math.Pow((startLocation.LocationLatitude - endLocation.LocationLatitude), 2) + Math.Pow((startLocation.LocationLongitude - endLocation.LocationLongitude), 2);
            distance = Math.Sqrt(distance);
            int distanceInInt = Convert.ToInt32(distance);
            if (driver.DriverVehicle.VehicleType.ToLower() == "car")
                return (distanceInInt * 300) / 15 + 20;
            else if (driver.DriverVehicle.VehicleType.ToLower() == "bike")
                return (distanceInInt * 272) / 50 + 5;
            else if (driver.DriverVehicle.VehicleType.ToLower() == "rickshaw")
                return (distanceInInt * 272) / 35 + 10;
            else
                return -1;
        }
        private int calculateDistance(location passengerStartLocation, location driverCurrentLocation)
        {
            double distance = Math.Pow((passengerStartLocation.LocationLatitude - driverCurrentLocation.LocationLatitude), 2) + Math.Pow((passengerStartLocation.LocationLongitude - driverCurrentLocation.LocationLongitude), 2);
            distance = Math.Sqrt(distance);
            return Convert.ToInt32(distance);
        }
        /*private int findMin(ArrayList distances)
        {
            if (distances.Count == 0)
                return -1;
            double min = distances[0];
            int minIndex = 0;
            for (int i = 0; i < distances.Count; i++)
                if (distances[i] < min)
                { 
                    min = distances[i];
                    minIndex = i;
                }
            return minIndex;
        }*/
        public int assignDriver(List<driver> listDriv)
        {
            if (listDriv.Count == 0)
                return -1;
            List<int> availableDriverIndexs = new List<int>();
            for (int i = 0; i < listDriv.Count; i++)
                if (listDriv[i].DriverAvailability == true && listDriv[i].DriverVehicle.VehicleType.ToLower() == this.driver.DriverVehicle.VehicleType.ToLower())
                    availableDriverIndexs.Add(i);
            if (availableDriverIndexs.Count == 0)
                return -1;
            double minDist = calculateDistance(this.startLocation, listDriv[availableDriverIndexs[0]].DriverLocation);
            double distance;
            int minIndex = availableDriverIndexs[0];
            for (int i = 1; i < availableDriverIndexs.Count; i++)
            {
                distance = calculateDistance(this.startLocation, listDriv[availableDriverIndexs[i]].DriverLocation);
                if (distance <= minDist)
                {
                    minDist = distance;
                    minIndex = availableDriverIndexs[i];
                }
            }
            this.driver.DriverName = listDriv[minIndex].DriverName;
            this.driver.DriverAge = listDriv[minIndex].DriverAge;
            this.driver.DriverGender = listDriv[minIndex].DriverGender;
            this.driver.DriverAddress = listDriv[minIndex].DriverAddress;
            this.driver.DriverVehicle.VehicleModel = listDriv[minIndex].DriverVehicle.VehicleModel;
            this.driver.DriverVehicle.VehicleNumber = listDriv[minIndex].DriverVehicle.VehicleNumber;
            this.driver.DriverLocation.LocationLatitude = listDriv[minIndex].DriverLocation.LocationLatitude;
            this.driver.DriverLocation.LocationLongitude = listDriv[minIndex].DriverLocation.LocationLongitude;
            listDriv[minIndex].DriverAvailability = false;
            listDriv[minIndex].newAvailablility(listDriv[minIndex].DriverId);
            return minIndex;
        }
        public int giveRating()
        {
            bool status = true;
            int rating = 0;
            while (status == true)
            {
                Console.Write("Give Rating out of 5: ");
                Console.ForegroundColor = ConsoleColor.Green;
                rating = int.Parse(Console.ReadLine());
                Console.ResetColor();
                if (rating >= 0 && rating <= 5)
                    status = false;
            }
            this.driver.DriverRating = rating;
            return rating;

        }
        public void getLocation()
        {
            Console.Write("Enter Start Location:(Latitude) ");
            Console.ForegroundColor = ConsoleColor.Green;
            this.startLocation.LocationLatitude = float.Parse(Console.ReadLine());
            Console.ResetColor();
            Console.Write("Enter Start Location:(Longitude) ");
            Console.ForegroundColor = ConsoleColor.Green;
            this.startLocation.LocationLongitude = float.Parse(Console.ReadLine());
            Console.ResetColor();
            Console.Write("Enter End Location:(Latitude) ");
            Console.ForegroundColor = ConsoleColor.Green;
            this.endLocation.LocationLatitude = float.Parse(Console.ReadLine());
            Console.ResetColor();
            Console.Write("Enter End Location:(Longitude) ");
            Console.ForegroundColor = ConsoleColor.Green;
            this.endLocation.LocationLongitude = float.Parse(Console.ReadLine());
            Console.ResetColor();
        }
        public bool bookRide()
        {
            Console.Write("Enter Name: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string passName = Console.ReadLine();
            Console.ResetColor();
            Console.Write("Enter Phone Number: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string phoneNumber = Console.ReadLine();
            Console.ResetColor();
            this.getLocation();
            Console.Write("Enter Ride Type: ");
            Console.ForegroundColor = ConsoleColor.Green;
            this.driver.DriverVehicle.VehicleType = Console.ReadLine();
            Console.ResetColor();
            this.price = this.calculatePrice();
            for (int i = 0; i < Console.WindowWidth / 4; i++)
                Console.Write('-');
            Console.Write("THANK YOU");
            for (int i = 0; i < Console.WindowWidth / 4; i++)
                Console.Write('-');
            Console.Write($"\nPrice of Ride is: {price}\n");
            return this.assignPassenger(passName, phoneNumber);
        }
    }
}
