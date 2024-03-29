﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models.UserProfile
{
    [Table("Genders")]
    public class Gender
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Gender")]
        [Display(Name = "Gender")]
        [Required]
        [DataType(DataType.Text)]
        [MinLength(3)]
        public string Title { get; set; }

        public List<UserProfile> UserProfiles { get; set; } = new();
    }
}
