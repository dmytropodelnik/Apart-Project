﻿using CloneBookingAPI.Interfaces;
using CloneBookingAPI.Services.POCOs;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CloneBookingAPI.Services.Email
{
    public class DealsEmailSender : IEmailSender
    {
        private readonly string _emailSender    = default;
        private readonly string _emailPassword  = default;
        private readonly string _smtpHost       = default;
        private readonly string _smtpPort       = default;

        public DealsEmailSender(IConfiguration configuration)
        {
            _emailSender    = configuration["EmitterData:EmmiterEmail"];
            _emailPassword  = configuration["EmitterData:EmmiterPass"];
            _smtpHost       = configuration["EmitterData:SmtpData:Gmail:Host"];
            _smtpPort       = configuration["EmitterData:SmtpData:Gmail:Port"];
        }

        public async Task<bool> SendEmailAsync(MailLetterPoco letter)
        {
            try
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("Apart.com", _emailSender));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = letter.Title;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = letter.Text
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_smtpHost, int.Parse(_smtpPort), false);
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
