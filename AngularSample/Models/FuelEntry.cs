using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AngularSample.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

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
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<FuelEntryModel> FuelEntries { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
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

        public string Year { get { return Date.Year.ToString(); } }
        public string YearAndMonth { get { return Date.ToString("MMMM yyyy"); } }
    }

    public class Car
    {
        public int Id { get; set; }
        public string CarName { get; set; }
        public bool IsSelected { get; set; }

        public virtual ICollection<FuelEntryModel> FuelEntries { get; set; }
    }
}