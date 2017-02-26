using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace iStore.Models
{

    /**
        Model object for a product item in the store
        Could really do with adding an image url to this :)
    **/
    public class StoreItem
    {
        public StoreItem()
        {
            this.ID = Guid.NewGuid(); // always give it a unique id
            this.AddedDate = DateTime.Now;
        }

        public StoreItem(string name, decimal price) : this()
        {
            this.Name = name;
            this.Price = price;
        }

        [Key]
        public Guid ID { get; set; }

        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "£ {0:0.00}")]
        public decimal Price { get; set; }

        // show that data annotations can be used 
        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AddedDate { get; set; }
    }
}