using System.ComponentModel.DataAnnotations;

namespace AngularSample.Models
{
    using System;
    using System.Data.Entity;

    public class PhotoModelDbContext : DbContext
    {
        // Your context has been configured to use a 'PhotoModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'AngularSample.Models.PhotoModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'PhotoModel' 
        // connection string in the application configuration file.
        public PhotoModelDbContext()
            : base("name=PhotoModel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<PhotoModel> Photos { get; set; }
    }

    public class PhotoModel
    {
        [Key]
        public int PhotoId { get; set; }

        [Display(Name = "Opis")]
        [Required]
        public string Decription { get; set; }

        [Display(Name = "Image Path")]
        public string ImagePath { get; set; }

        [Display(Name = "Thumb Path")]
        public string ThumbPath { get; set; }

        [Display(Name = "Data utworzenia")]
        public DateTime CreatedOn { get; set; }
    }
}