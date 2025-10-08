using System;

namespace TrafficAccidentLib
{
    public enum AccidentType
    {
        PedestrianCollision,
        ObstacleCollision,
        VehicleCollision,
        Rollover,
        Other
    }

    public enum AccidentCause
    {
        OncomingTraffic,
        DriverCondition,
        VehicleMalfunction,
        TrafficViolation,
        Other
    }

    public class Driver
    {
        public int ID { get; set; }
        public string FIO { get; set; }
        public int Experience { get; set; }
        public string LicensePlate { get; set; }
        public string LicenseNumber { get; set; }
    }

    public class Vehicle
    {
        public int ID { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string BodyType { get; set; }
        public string LicensePlate { get; set; }
    }

    public class TrafficPoliceDepartment
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string AccidentReportNumber { get; set; }
        public Driver Driver { get; set; }
        public string LicensePlate { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int NumberOfVictims { get; set; }
        public AccidentType AccidentType { get; set; }
        public AccidentCause Cause { get; set; }
    }

    public class TrafficAccidentManager
    {
        private List<Driver> drivers = new List<Driver>();
        private List<Vehicle> vehicles = new List<Vehicle>();
        private List<TrafficPoliceDepartment> accidents = new List<TrafficPoliceDepartment>();

        public void AddDriver(Driver driver)
        {
            if (driver != null)
            {
                driver.ID = drivers.Count + 1;
                drivers.Add(driver);
            }
        }

        public void AddVehicle(Vehicle vehicle)
        {
            if (vehicle != null)
            {
                vehicle.ID = vehicles.Count + 1;
                vehicles.Add(vehicle);
            }
        }

        public void AddAccident(TrafficPoliceDepartment accident)
        {
            if (accident != null)
            {
                accident.ID = accidents.Count + 1;
                if (accident.Driver != null && !drivers.Any(d => d.FIO == accident.Driver.FIO))
                {
                    AddDriver(accident.Driver);
                }
                accidents.Add(accident);
            }
        }

        public bool RemoveAccident(string accidentReportNumber)
        {
            var accident = accidents.FirstOrDefault(a => a.AccidentReportNumber == accidentReportNumber);
            if (accident != null)
            {
                accidents.Remove(accident);
                return true;
            }
            return false;
        }

        public IEnumerable<(string FIO, int Count)> GetDriversWithMultipleAccidents()
        {
            return accidents.GroupBy(a => a.Driver?.FIO)
                            .Where(g => g.Key != null && g.Count() > 1)
                            .Select(g => (g.Key, g.Count()));
        }

        public IEnumerable<Driver> GetDriversByLocation(string location)
        {
            return accidents.Where(a => string.Equals(a.Location, location, StringComparison.OrdinalIgnoreCase))
                            .Select(a => a.Driver)
                            .Where(d => d != null)
                            .Distinct();
        }

        public IEnumerable<Driver> GetDriversByDate(DateTime date)
        {
            return accidents.Where(a => a.Date.Date == date.Date)
                            .Select(a => a.Driver)
                            .Where(d => d != null)
                            .Distinct();
        }

        public TrafficPoliceDepartment GetAccidentWithMaxVictims()
        {
            // ❌ Ошибка специально, чтобы тест показал проблему: возвращаем accident с минимальным количеством жертв вместо максимального
            return accidents.OrderBy(a => a.NumberOfVictims).FirstOrDefault();
            // Для исправления: return accidents.OrderByDescending(a => a.NumberOfVictims).FirstOrDefault();
        }

        // Дополнительные методы из курсовой, но мы тестируем только восемь указанных
    }
}