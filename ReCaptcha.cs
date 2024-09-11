using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace OZQ_gayendri
{
    public class ReCaptcha
    {
        public static bool ValidateRecaptcha(string recaptchaResponse)
        {
            string secretKey = "6LeIxAcTAAAAAGG-vFI1TnRWxMZNFuojJ4WifJWe";
            string apiUrl = "https://www.google.com/recaptcha/api/siteverify";
            string responseString;

            using (var client = new WebClient())
            {
                var parameters = new System.Collections.Specialized.NameValueCollection
            {
                { "secret", secretKey },
                { "response", recaptchaResponse }
            };

                var response = client.UploadValues(apiUrl, "POST", parameters);
                responseString = System.Text.Encoding.UTF8.GetString(response);
            }

            dynamic responseObject = JObject.Parse(responseString);
            return Convert.ToBoolean(responseObject.success);
        }
    }
}