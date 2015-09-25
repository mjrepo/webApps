using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using AngularSample.Areas.CarCalculator.Models;

namespace AngularSample.Areas.CarCalculator.ViewModels
{
    public class CarCalculatorSummary
    {
        public CarCalculatorSummary(IList<FuelEntryModel> fuelEntries)
        {
            HasData = fuelEntries != null && fuelEntries.Any();
            if (!HasData) return;

            MinAmountOf100Km = fuelEntries.Min(i => i.AmountOfFuelPer100Km);
            MaxAmountOf100Km = fuelEntries.Max(i => i.AmountOfFuelPer100Km);

            MinCost1Km = fuelEntries.Min(i => i.CostOf1Km);
            MaxCost1Km = fuelEntries.Max(i => i.CostOf1Km);

            SumDistance = fuelEntries.Sum(i => i.CurrentDistance);
            SumAmountOfFuel = fuelEntries.Sum(i => i.AmountOfFuel);
            SumTotalCost = fuelEntries.Sum(i => i.GasPrice * i.AmountOfFuel);

            var minDate = fuelEntries.Min(i => i.Date);
            var maxDate = fuelEntries.Max(i => i.Date);
            AverageFuel = (decimal)((maxDate - minDate).TotalDays/fuelEntries.Count);
        }

        public bool HasData { get; private set; }


        public decimal AmountOfFuelPer100Km { get; private set; }
        public decimal CostOf1Km { get; private set; }
        public decimal DistancePerMonth { get; private set; }
        public decimal MinAmountOf100Km { get; private set; }
        public decimal MaxAmountOf100Km { get; private set; }
        public decimal MinCost1Km { get; private set; }
        public decimal MaxCost1Km { get; private set; }
        public decimal SumDistance { get; private set; }
        public decimal SumAmountOfFuel { get; private set; }
        public decimal SumTotalCost { get; private set; }
        public decimal AverageFuel { get; private set; }

        public decimal AvgAmountOf100Km
        {
            get
            {
                Contract.Ensures(SumDistance != 0);
                return SumAmountOfFuel / SumDistance * 100;
            }
        }

        public decimal AvgCostOf1Km
        {
            get
            {
                Contract.Ensures(SumDistance != 0);
                return SumTotalCost / SumDistance;
            }
        }
    }
}