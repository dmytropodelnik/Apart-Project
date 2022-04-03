using CloneBookingAPI.Services.Database.Models.Suggestions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Database.Models.Suggestions
{
    [Table("BookedDates")]
    public class BookedDate
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime DateIn { get; set; } = new();
        public DateTime DateOut { get; set; } = new();

        public List<Suggestion> Suggestions { get; set; } = new();
    }
}
