using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadandDownloadFiles.Helpers
{
 
    public class AppSettings
    {
        // Properties for JWT Token Signature
        public string Site { get; set; }
        public string Audience { get; set; }
        public string ExpireTime { get; set; }
        public string Secret { get; set; }

        // Properties for SendGrid 
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }

        // Properties for Twilio
        public string TwilioAccountSid { get; set; }
        public string TwilioAuthToken { get; set; }
        public string TwilioPhoneNumber { get; set; }

        // Properties for SmartyStreets
        public string SmartyStreetsAuthID { get; set; }
        public string SmartyStreetsAuthToken { get; set; }

        // for getting Email Verification key.
        public string APIkey { get; set; }

        // for getting Stripe key
        public string PublishableKeyGR { get; set; }
        public string SecretKeyGR { get; set; }
        public string PublishableKeyST { get; set; }
        public string SecretKeyST { get; set; }

        // for FOrte API keys
        public string ForteSTLAPIAccessID { get; set; }
        public string ForteSTLAPISourceKey { get; set; }
        public string ForteSTLTestISOID { get; set; }

        public string ForteGRAPIAccessID { get; set; }
        public string ForteGRAPISourceKey { get; set; }
        public string ForteGRTestISOID { get; set; }

        public string SMTPServerName { get; set; }
        public string SMTPUserName { get; set; }
        public string SMTPPassword { get; set; }
        public string SMTPPort { get; set; }
        public string EmailAccount { get; set; }
        public string EnableSsl { get; set; }
        public string UploadedFilePath { get; set; }
        public string SaleTaxAPIKay { get; set; }
        public string esignaturesioSecretKey { get; set; }
    }
}
