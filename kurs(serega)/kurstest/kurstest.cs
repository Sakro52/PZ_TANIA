using System;
using System.Collections.Generic;
using System.Linq;
using TrafficAccidentLib;
using Xunit;

namespace TrafficAccidentLibUnitTests
{
    public class TrafficAccidentManagerUnitTests
    {
        [Fact]
        public void TestAddDriver()
        {
            // Arrange
            var manager = new TrafficAccidentManager();
            var driver = new Driver { FIO = "John Doe", Experience = 5, LicensePlate = "ABC123", LicenseNumber = "123456" };

            // Act
            manager.AddDriver(driver);

            // Assert
            Assert.Single(manager.GetType().GetField("drivers", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(manager) as List<Driver>);
            Assert.Equal(1, driver.ID);
            Assert.Equal("John Doe", driver.FIO);
        }

        [Fact]
        public void TestAddVehicle()
        {
            // Arrange
            var manager = new TrafficAccidentManager();
            var vehicle = new Vehicle { Manufacturer = "Toyota", Model = "Camry", BodyType = "Sedan", LicensePlate = "XYZ789" };

            // Act
            manager.AddVehicle(vehicle);

            // Assert
            Assert.Single(manager.GetType().GetField("vehicles", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(manager) as List<Vehicle>);
            Assert.Equal(1, vehicle.ID);
            Assert.Equal("Toyota", vehicle.Manufacturer);
        }

        [Fact]
        public void TestAddAccident_WithNewDriver()
        {
            // Arrange
            var manager = new TrafficAccidentManager();
            var driver = new Driver { FIO = "Jane Doe", Experience = 10, LicensePlate = "DEF456", LicenseNumber = "654321" };
            var accident = new TrafficPoliceDepartment
            {
                Name = "Dept1",
                AccidentReportNumber = "REP001",
                Driver = driver,
                LicensePlate = "DEF456",
                Date = new DateTime(2023, 1, 1),
                Location = "City Center",
                NumberOfVictims = 2,
                AccidentType = AccidentType.VehicleCollision,
                Cause = AccidentCause.TrafficViolation
            };

            // Act
            manager.AddAccident(accident);

            // Assert
            var driversList = manager.GetType().GetField("drivers", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(manager) as List<Driver>;
            var accidentsList = manager.GetType().GetField("accidents", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(manager) as List<TrafficPoliceDepartment>;
            Assert.Single(driversList);
            Assert.Single(accidentsList);
            Assert.Equal(1, accident.ID);
            Assert.Equal("REP001", accident.AccidentReportNumber);
        }

        [Fact]
        public void TestRemoveAccident_Success()
        {
            // Arrange
            var manager = new TrafficAccidentManager();
            var accident = new TrafficPoliceDepartment { AccidentReportNumber = "REP002" };
            manager.AddAccident(accident);

            // Act
            bool removed = manager.RemoveAccident("REP002");

            // Assert
            Assert.True(removed);
            Assert.Empty(manager.GetType().GetField("accidents", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(manager) as List<TrafficPoliceDepartment>);
        }

        [Fact]
        public void TestGetDriversWithMultipleAccidents()
        {
            // Arrange
            var manager = new TrafficAccidentManager();
            var driver1 = new Driver { FIO = "Driver1" };
            var driver2 = new Driver { FIO = "Driver2" };
            manager.AddAccident(new TrafficPoliceDepartment { Driver = driver1 });
            manager.AddAccident(new TrafficPoliceDepartment { Driver = driver1 });
            manager.AddAccident(new TrafficPoliceDepartment { Driver = driver2 });

            // Act
            var result = manager.GetDriversWithMultipleAccidents().ToList();

            // Assert
            Assert.Single(result);
            Assert.Equal("Driver1", result[0].FIO);
            Assert.Equal(2, result[0].Count);
        }

        [Fact]
        public void TestGetDriversByLocation()
        {
            // Arrange
            var manager = new TrafficAccidentManager();
            var driver1 = new Driver { FIO = "Driver1" };
            var driver2 = new Driver { FIO = "Driver2" };
            manager.AddAccident(new TrafficPoliceDepartment { Driver = driver1, Location = "LocationA" });
            manager.AddAccident(new TrafficPoliceDepartment { Driver = driver2, Location = "LocationB" });

            // Act
            var result = manager.GetDriversByLocation("LocationA").ToList();

            // Assert
            Assert.Single(result);
            Assert.Equal("Driver1", result[0].FIO);
        }

        [Fact]
        public void TestGetDriversByDate()
        {
            // Arrange
            var manager = new TrafficAccidentManager();
            var driver1 = new Driver { FIO = "Driver1" };
            var driver2 = new Driver { FIO = "Driver2" };
            var date1 = new DateTime(2023, 1, 1);
            var date2 = new DateTime(2023, 1, 2);
            manager.AddAccident(new TrafficPoliceDepartment { Driver = driver1, Date = date1 });
            manager.AddAccident(new TrafficPoliceDepartment { Driver = driver2, Date = date2 });

            // Act
            var result = manager.GetDriversByDate(date1).ToList();

            // Assert
            Assert.Single(result);
            Assert.Equal("Driver1", result[0].FIO);
        }

        [Fact]
        public void TestGetAccidentWithMaxVictims()
        {
            // Arrange
            var manager = new TrafficAccidentManager();
            var accident1 = new TrafficPoliceDepartment { NumberOfVictims = 1 };
            var accident2 = new TrafficPoliceDepartment { NumberOfVictims = 3 };
            var accident3 = new TrafficPoliceDepartment { NumberOfVictims = 2 };
            manager.AddAccident(accident1);
            manager.AddAccident(accident2);
            manager.AddAccident(accident3);

            // Act
            var result = manager.GetAccidentWithMaxVictims();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.NumberOfVictims);  // Этот тест провалится из-за преднамеренной ошибки в методе; после исправления пройдет
        }
    }
}