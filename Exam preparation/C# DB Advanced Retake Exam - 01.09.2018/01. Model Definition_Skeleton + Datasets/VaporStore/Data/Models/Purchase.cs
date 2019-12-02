namespace VaporStore.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using VaporStore.Data.Models.Enumerations;

    public class Purchase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public PurchaseType Type { get; set; }

        [Required]
        [RegularExpression("^[0-9A-Z]{4}[-][0-9A-Z]{4}[-][0-9A-Z]{4}$")]
        public string ProductKey { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int GameId { get; set; }

        public Game Game { get; set; }

        [Required]
        public int CardId { get; set; }

        public Card Card { get; set; }

        //•	Id – integer, Primary Key                                      
        //•	Type – enumeration of type PurchaseType, with possible values(“Retail”, “Digital”) (required)            
        //•	ProductKey – text, which consists of 3 pairs of 4 uppercase Latin letters and digits, separated by dashes(ex. “ABCD-EFGH-1J3L”) (required)                                   
        //•	Date – Date(required)                               
        //•	CardId – integer, foreign key(required)                                
        //•	Card – the purchase’s card(required)                                 
        //•	GameId – integer, foreign key(required)                                  
        //•	Game – the purchase’s game(required)

    }
}
