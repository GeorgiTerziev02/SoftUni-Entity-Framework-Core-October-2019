namespace VaporStore.DataProcessor.Dtos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ImportGameDTO
    {
        //"Name": "Dota 2",
        //"Price": 0,
        //"ReleaseDate": "2013-07-09",
        //"Developer": "Valve",
        //"Genre": "Action",
        //"Tags": [
        //  "Multi-player",
        //  "Co-op",
        //  "Steam Trading Cards",
        //  "Steam Workshop",
        //  "SteamVR Collectibles",
        //  "In-App Purchases",
        //  "Valve Anti-Cheat enabled"
        //]

        [Required]
        public string Name { get; set; }

        [Range(0, double.MaxValue), Required]
        public decimal Price { get; set; }

        [Required]
        public string ReleaseDate { get; set; }

        [Required]
        public string Developer { get; set; }

        [Required]
        public string Genre { get; set; }

        public List<string> Tags { get; set; }
    }
}
