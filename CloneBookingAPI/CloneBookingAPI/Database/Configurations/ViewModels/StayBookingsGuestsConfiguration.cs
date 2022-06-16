﻿using CloneBookingAPI.Database.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloneBookingAPI.Database.Configurations.ViewModels
{
    public class StayBookingsGuestsConfiguration : IEntityTypeConfiguration<StayBookingGuest>
    {
        public void Configure(EntityTypeBuilder<StayBookingGuest> builder)
        {
            builder.HasData(
              new StayBookingGuest[]
              {

              });
        }
    }
}
