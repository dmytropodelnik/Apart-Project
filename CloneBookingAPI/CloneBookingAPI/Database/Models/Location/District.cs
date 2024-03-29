﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models.Location
{
    [Table("Districts")]
    public class District
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MinLength(2)]
        public string Title { get; set; }

        public Address Address { get; set; }

        public int? ImageId { get; set; }
        [ForeignKey("ImageId")]
        public FileModel Image { get; set; }
    }
}
