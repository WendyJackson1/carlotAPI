using GCCarDealership.Models;
using Microsoft.AspNetCore.Mvc;

namespace GCCarDealership.Controllers
{
    [ApiController]
    [Route("controller")]
    public class CarController
    {
        CarAPIContext context = new CarAPIContext();

        public List<Car> car = new List<Car>();

        [HttpGet("ShowAllCars")]

        public List<Car> ListOfCars()
        {
            return context.Cars.ToList();
        }

        [HttpGet("SearchCar/{Id}")]
        public Car FindCarId(int Id)
        {
            Car output = context.Cars.Find(Id);
            return output;
        }
        [HttpGet("SearchCarByMake/{Make}")]
        public List<Car> CarByMake(string Make)
        {
            List<Car> cars = context.Cars.Where(x => x.Make == Make).ToList();
            return cars;
        }
        [HttpGet("SearchCarByModel/{Model}")]
        public List<Car> CarByModel(string Model)
        {
            List<Car> cars = context.Cars.Where(x => x.Model == Model).ToList();
            return cars;
        }
        [HttpGet("SearchCarByYear/{Year}")]
        public List<Car> CarByYear(int Year)
        {
            List<Car> cars = context.Cars.Where(x => x.Year == Year).ToList();
            return cars;
        }
        [HttpGet("SearchCarByColor/{Color}")]
        public List<Car> CarByColor(string Color)
        {
            List<Car> cars = context.Cars.Where(x => x.Color == Color).ToList();
            return cars;
        }

        [HttpPost("CreateCar")]
        public void AddCar(Car Car)
        {
            context.Cars.Add(Car);
            context.SaveChanges();
        }
        [HttpDelete("DeleteCar/{Id}")]

        public string DelCar(int Id)
        {
            int count = context.Cars.Count();
            try
            {
                context.Cars.Remove(FindCarId(Id));
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                message += "No changes were made";
                return message;
            }
            context.SaveChanges();
            int finalCount = context.Cars.Count();
            return $"Started with {count}, ended with {finalCount}";
        }
        [HttpPut("UpdateCar/{Id}")]
        public string UpdateCar(int id, Car UpdatedCar)
        {
            Car car = FindCarId(id);
            car.Make = UpdatedCar.Make;
            car.Color = UpdatedCar.Color;
            car.Model = UpdatedCar.Model;
            car.Year = UpdatedCar.Year;
            context.Cars.Update(car);
            context.SaveChanges();
            return $"This {car.Make}{car.Model} was replaced with {UpdatedCar.Make}{UpdatedCar.Model}";
        }
    }
}
