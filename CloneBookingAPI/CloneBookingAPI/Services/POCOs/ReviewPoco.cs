﻿using CloneBookingAPI.Services.Database.Configurations.Review;
using System.Collections.Generic;

namespace CloneBookingAPI.Services.POCOs
{
    public class ReviewPoco
    {
        public int Id { get; set; }
        public string BookingNumber { get; set; }
        public string BookingPIN { get; set; }
        public int SuggestionId { get; set; }
        public int OwnerId { get; set; }
        public ReviewMessage ReviewMessage { get; set; }
        public List<ReviewTuple> Grades { get; set; }
    }
}
