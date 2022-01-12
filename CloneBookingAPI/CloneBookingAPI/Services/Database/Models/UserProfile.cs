using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models
{
    /// <summary>
    ///     Class of UserProfile model.
    /// </summary>
    [Table("UserProfiles")]
    public class UserProfile
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Date of birth")]
        [Required(ErrorMessage = "Enter a date of birth")]
        [DataType(DataType.Date)]
        public DateOnly BirthDate { get; set; }

        [Required]
        public DateOnly RegisterDate { get; set; }

        [Display(Name = "Nationality")]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Incorrect length")]
        public string Nationality { get; set; }

        public int GenderId { get; set; }
        [ForeignKey("GenderId")]
        public Gender Gender { get; set; }

        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        public int AddressId { get; set; }
        [ForeignKey("AddressId")]
        public Address Address { get; set; }

        public int LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public Language Language { get; set; }

        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
