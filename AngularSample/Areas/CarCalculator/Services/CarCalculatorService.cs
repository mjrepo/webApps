using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AngularSample.Areas.CarCalculator.Models;
using AngularSample.Areas.CarCalculator.ViewModels;

namespace AngularSample.Areas.CarCalculator.Services
{
    public class CarCalculatorService
    {
        public CarCalculatorSummary CalculateSummary()
        {
            return new CarCalculatorSummary(GetFuelEntries(DateTime.MinValue, DateTime.MaxValue));
        }

        public CarCalculatorSummary CalculateSummary(DateTime startDate, DateTime endDate)
        {
            return new CarCalculatorSummary(GetFuelEntries(startDate, endDate));
        }

        private IList<FuelEntryModel> GetFuelEntries(DateTime startDate, DateTime endDate)
        {
            using (FuelEntryDbContext db = new FuelEntryDbContext())
            {
                var defaultCar = db.Cars.SingleOrDefault(i => i.IsSelected);
                if (defaultCar == null)
                {
                    throw new InvalidOperationException("Brak domyślnego samochodu");
                }
                return db.FuelEntries
                    .Where(i => i.CarId == defaultCar.Id)
                    .Where(i => i.Date >= startDate && i.Date <= endDate)
                    .ToList();
            }
        }
    }

   
}