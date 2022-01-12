using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models
{
    [Table("AreaInfos")]
    public class AreaInfo
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
