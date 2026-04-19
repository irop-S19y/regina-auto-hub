using System.ComponentModel.DataAnnotations.Schema;

namespace AutoServicesRegina.Models
{
    public class Service
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string Address { get; set; } = "";

        public string City { get; set; } = "";

        public string Phone { get; set; } = "";

        public string Website { get; set; } = "";

        public string Description { get; set; } = "";

        public string WorkingHours { get; set; } = "";

        
        public string ImageUrl { get; set; } = "";
        
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Rating> Ratings { get; set; } = new();
        
        
         [NotMapped]
        public double Rating { get; set; }

          [NotMapped]
        public int RatingCount { get; set; }
        
       [NotMapped]
        public int MyRating { get; set; }
    
    
    }


}
    

