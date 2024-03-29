﻿using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace backend.Business.Services
{
    public static class EmailService
    {
        private const string ApiKeyName = "SEND_GRID_FP_API_KEY";

        public static async Task<Response> SendEmail(EmailAddress to, string subject, string bodyText, string bodyHtml)
        {
            var from = new EmailAddress("eventsmailsystem@gmail.com", "events system");
            // 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
            var apiKey = "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";
                // Environment.GetEnvironmentVariable(ApiKeyName);
            var client = new SendGridClient(apiKey);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, bodyText, bodyHtml);
            return await client.SendEmailAsync(msg);
        }
    }
}