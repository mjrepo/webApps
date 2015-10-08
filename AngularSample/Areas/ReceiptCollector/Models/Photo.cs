using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AngularSample.Areas.ReceiptCollector.Models
{
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }

        [Display(Name = "Decription")]
        [Required]
        public string Decription { get; set; }

        [Display(Name = "Image Path")]
        public String ImagePath { get; set; }

        [Display(Name = "Thumb Path")]
        public String ThumbPath { get; set; }


        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }
    }

    public class PhotoWithText: Photo
    {
        public string Text { get; set; }
    }

    public class ReceiptData
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ReceiptDate { get; set; }
        public virtual IList<Photo> Articles { get; set; }
        public virtual IList<PhotoWithText> Receipts { get; set; }
    }
}