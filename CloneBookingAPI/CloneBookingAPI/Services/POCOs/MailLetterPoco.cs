using CloneBookingAPI.Services.Database.Models;
using System;

namespace CloneBookingAPI.Services.POCOs
{
    public class MailLetterPoco
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime SendingDate { get; set; }

        public int ReceiversAmount { get; set; }

        public string Sender { get; set; }

        public FileModel File { get; set; }
    }
}
