using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models
{
    [Table("Languages")]
    public class Language
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Language")]
        [Display(Name = "Language")]
        [DataType(DataType.Text)]
        [MinLength(3)]
        public string Title { get; set; }

        public List<User> Users { get; set; } = new();
    }
}
