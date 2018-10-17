using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace SportsStore.Models {
    public class Product {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductId {get; set;}
        public string Name {get; set;}
        public string Category {get; set;}
        public string Description {get; set;}
        public decimal Price {get; set;}
        public List<Rating> Ratings { get; set; }
        public Supplier Supplier { get; set; }
    }
}
