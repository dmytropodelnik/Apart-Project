﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloneBookingAPI.Services.Database.Models.Payment
{
    [Table("CreditCards")]
    public class CreditCard
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Cardholder's Name")]
        [Required]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Incorrect length")]
        public string Cardholder { get; set; }

        [Display(Name = "Card Number")]
        [Required]
        [DataType(DataType.CreditCard)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Incorrect length")]
        public string CardNumber { get; set; }

        [Display(Name = "Expiration Date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }

        [Display(Name = "CVC")]
        [Required(ErrorMessage = "Enter a CVC")]
        [DataType(DataType.Password)]
        public string CVC { get; set; }

        public int CardTypeId { get; set; }
        [ForeignKey("CardTypeId")]
        public CardType CardType { get; set; }

        public List<Payment> Payments { get; set; } = new();
    }
}
