using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Runtime.Serialization;

namespace AngularSample.Areas.CarCalculator.Models
{
    public class FuelEntryDbContext : DbContext
    {
        // Your context has been configured to use a 'FuelEntry' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'AngularSample.Models.FuelEntry' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'FuelEntry' 
        // connection string in the application configuration file.
        public FuelEntryDbContext()
            : base("name=FuelEntryModel")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<FuelEntryModel> FuelEntries { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Milestone> Milestones { get; set; }
    }

    public class FuelEntryModel
    {
        [Key]
        public int Id { get; set; }

        public int CarId { get; set; }
        public DateTime Date { get; set; }
        public decimal AmountOfFuel { get; set; }
        public decimal GasPrice { get; set; }
        public decimal CurrentDistance { get; set; }
        public decimal Distance { get; set; }

        public decimal TotalCost { get { return GasPrice*AmountOfFuel; } }

        public string Year { get { return Date.Year.ToString(); } }
        public string YearAndMonth { get { return Date.ToString("MMMM yyyy"); } }

        public decimal AmountOfFuelPer100Km { get { return AmountOfFuel/CurrentDistance*100; } }
        public decimal CostOf1Km { get { return TotalCost/CurrentDistance; } }

    }

    public class Car
    {
        public int Id { get; set; }
        public string CarName { get; set; }
        public bool IsSelected { get; set; }

        [IgnoreDataMember]
        public virtual ICollection<FuelEntryModel> FuelEntries { get; set; }
    }

    public class Milestone
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}