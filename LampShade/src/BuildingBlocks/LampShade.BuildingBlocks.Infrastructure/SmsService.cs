using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace _0_Framework.Application.SMS
{
    public class SmsService : ISmsService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public SmsService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient { BaseAddress = new Uri("https://api.sms.ir/") };
        }

        public void Send(string nummber, string message)
        {
            var token = GetToken();
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var linesResponse = _httpClient.GetAsync("v1/send/lines").Result;
            if (!linesResponse.IsSuccessStatusCode) return;

            var linesJson = linesResponse.Content.ReadAsStringAsync().Result;
            var linesDoc = JsonDocument.Parse(linesJson);
            var linesArray = linesDoc.RootElement.GetProperty("lines");
            if (linesArray.GetArrayLength() == 0) return;

            var line = linesArray.EnumerateArray().Last().GetProperty("lineNumber").ToString();

            var smsObject = new
            {
                mobileNumbers = new[] { nummber },
                messages = new[] { message },
                lineNumber = line,
                sendDateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
                canContinueInCaseOfError = true
            };

            var response = _httpClient.PostAsJsonAsync("v1/send/individual", smsObject).Result;
            if (response.IsSuccessStatusCode) return;

            line = linesArray.EnumerateArray().First().GetProperty("lineNumber").ToString();
            smsObject = new
            {
                mobileNumbers = new[] { nummber },
                messages = new[] { message },
                lineNumber = line,
                sendDateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
                canContinueInCaseOfError = true
            };
            _httpClient.PostAsJsonAsync("v1/send/individual", smsObject).Wait();
        }

        private string GetToken()
        {
            var smsSecrets = _configuration.GetSection("SmsSecrets");
            var requestBody = new
            {
                secretKey = smsSecrets["SecretKey"],
                system = "web-service"
            };

            var response = _httpClient.PostAsJsonAsync("v1/send/token", requestBody).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var doc = JsonDocument.Parse(json);
            return doc.RootElement.GetProperty("token").ToString();
        }
    }
}