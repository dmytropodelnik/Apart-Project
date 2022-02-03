﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models.Flights
{
    [Table("FlightClassTypes")]
    public class FlightClassType
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Type")]
        [Required]
        [Display(Name = "Flight Class Type")]
        [DataType(DataType.Text)]
        [MinLength(2)]
        public string Type { get; set; }
    }
}