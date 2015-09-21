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
            MinAmountOf100Km = fuelEntries.Min(i => i.AmountOfFuelPer100Km);
            MaxAmountOf100Km = fuelEntries.Max(i => i.AmountOfFuelPer100Km);

            MinCost1Km = fuelEntries.Min(i => i.CostOf1Km);
            MaxCost1Km = fuelEntries.Max(i => i.CostOf1Km);

            SumDistance = fuelEntries.Sum(i => i.Distance);
            SumAmountOfFuel = fuelEntries.Sum(i => i.AmountOfFuel);
            SumTotalCost = fuelEntries.Sum(i => i.GasPrice * i.AmountOfFuel);
        }

        public decimal AmountOfFuelPer100Km { get; set; }
        public decimal CostOf1Km { get; set; }
        public decimal DistancePerMonth { get; set; }
        public decimal MinAmountOf100Km { get; set; }
        public decimal MaxAmountOf100Km { get; set; }
        public decimal MinCost1Km { get; set; }
        public decimal MaxCost1Km { get; set; }
        public decimal SumDistance { get; set; }
        public decimal SumAmountOfFuel { get; set; }
        public decimal SumTotalCost { get; set; }

        public decimal AvgAmountOf100Km
        {
            get
            {
                Contract.Ensures(SumDistance!=0);
                return SumAmountOfFuel/SumDistance*100;
            }
        }

        public decimal AvgCostOf1Km
        {
            get
            {
                Contract.Ensures(SumDistance != 0);
                return SumTotalCost/SumDistance;
            }
        }
    }
}