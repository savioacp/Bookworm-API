using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Bookworm_API.Services
{
    public class MailSendingService
    {
        private static MailSendingService _instance;

        public static MailSendingService Instance => _instance ?? (_instance = new MailSendingService());

        public HttpResponseMessage SendMail(string email, string subject, string content)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"https://api.mailgun.net/v3/{Environment.GetEnvironmentVariable("MAILGUN_DOMAIN")}");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", $"api:{Environment.GetEnvironmentVariable("MAILGUN_APIKEY")}");

                return client.PostAsync("/messages", new FormUrlEncodedContent(new []
                {
                    new KeyValuePair<string, string>("from", "savioacp testing"),
                    new KeyValuePair<string, string>("to", email),
                    new KeyValuePair<string, string>("subject", subject),
                    new KeyValuePair<string, string>("text", content), 
                })).Result;
            }
        }
    }
}