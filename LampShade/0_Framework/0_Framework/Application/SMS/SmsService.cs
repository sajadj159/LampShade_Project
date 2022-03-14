using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using SmsIrRestful;

namespace _0_Framework.Application.SMS
{
    public class SmsService:ISmsService
    {
        private readonly IConfiguration _configuration;

        public SmsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Send(string nummber, string message)
        {
            var token = GetToken();
            var smsLineNumber = new SmsLine().GetSmsLines(token);
            if (smsLineNumber==null)    return;

            var line = smsLineNumber.SMSLines.Last().LineNumber.ToString();
            var date = new MessageSendObject
            {
                Messages = new List<string> {message}.ToArray(),
                MobileNumbers = new List<string>() {nummber}.ToArray(),
                LineNumber = line,
                SendDateTime = DateTime.Now,
                CanContinueInCaseOfError = true
            };
            var messageSendResponseObject = new MessageSend().Send(token,date);
            if (messageSendResponseObject.IsSuccessful)return;

            line = smsLineNumber.SMSLines.First().LineNumber.ToString();
            date.LineNumber = line;
            new MessageSend().Send(token, date);
        }

        private string GetToken()
        {
            var smsSecrets = _configuration.GetSection("SmsSecrets");
            var tokenService = new Token();
            return  tokenService.GetToken(smsSecrets["ApiKey"],smsSecrets["SecretKey"]);
        }
    }
}