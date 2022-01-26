﻿using CloneBookingAPI.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CloneBookingAPI.Services.Email
{
    public class DealsEmailSender : IEmailSender
    {
        private const string _emailSender = "clonebooking.itstep@gmail.com";
        private const string _emailPassword = "clonebooking_2022!";

        public async Task<bool> SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("Apart.com", _emailSender));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, false);
                    await client.AuthenticateAsync(_emailSender, _emailPassword);
                    await client.SendAsync(emailMessage);

                    await client.DisconnectAsync(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
