using CloneBookingAPI.Interfaces;
using CloneBookingAPI.Services.POCOs;
using CloneBookingAPI.Services.Repositories;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace CloneBookingAPI.Services.Email
{
    public class DealsEmailSender : IEmailSenderAsync
    {
        private readonly string _emailSender    = default;
        private readonly string _emailPassword  = default;
        private readonly string _smtpHost       = default;
        private readonly string _smtpPort       = default;

        private readonly MailUserListRepository _repository;

        public DealsEmailSender(IConfiguration configuration, MailUserListRepository repository)
        {
            _emailSender    = configuration["EmitterData:EmmiterEmail"];
            _emailPassword  = configuration["EmitterData:EmmiterPass"];
            _smtpHost       = configuration["EmitterData:SmtpData:Gmail:Host"];
            _smtpPort       = configuration["EmitterData:SmtpData:Gmail:Port"];

            _repository = repository;
        }

        public bool SendEmail(MailLetterPoco letter)
        {
            try
            {
                Thread newThread = new(obj =>
                {
                    if (obj is MailLetterPoco letter)
                    {
                        foreach (var emailTo in _repository.Subscribers)
                        {
                            var emailMessage = new MimeMessage();

                            emailMessage.From.Add(new MailboxAddress("Apartstep.com", _emailSender));
                            emailMessage.To.Add(new MailboxAddress("", emailTo));
                            emailMessage.Subject = letter.Title;
                            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                            {
                                Text = letter.Text
                            };

                            using (var client = new SmtpClient())
                            {
                                client.Connect(_smtpHost, int.Parse(_smtpPort), false);
                                client.Authenticate(_emailSender, _emailPassword);
                                client.Send(emailMessage);
                                client.Disconnect(true);
                            }
                        }
                    }
                });

                newThread.Start();

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
