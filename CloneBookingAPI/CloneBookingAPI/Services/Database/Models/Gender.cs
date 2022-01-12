using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models
{
    [Table("Genders")]
    public class Gender
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Gender")]
        [Display(Name = "Gender")]
        [DataType(DataType.Text)]
        [MinLength(3)]
        public string Title { get; set; }

        public List<User> Users { get; set; } = new();
    }
}
