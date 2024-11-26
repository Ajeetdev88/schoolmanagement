using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLMProject.Application.IService
{
    public interface IEmailService
    {

        Task<int> SendmailAsync(string email, string subject, string body);
        Task<string> GenerateEmailBody(string customerName, string otp, string otpid);
        Task<string> GenerateEmailVerificationEmailBody(string customerName, string otp, string otpid);
        public Task<string> GenerateDistributorshipRequestEmailBody(string customerName)
        {
            // Create the email body
            var emailBody = $@"
                <html>
                <head>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            line-height: 1.6;
                        }}
                        .container {{
                            max-width: 600px;
                            margin: 0 auto;
                            padding: 20px;
                            background-color: #f4f4f4;
                            border: 1px solid #ddd;
                        }}
                        .header {{
                            font-size: 24px;
                            font-weight: bold;
                            margin-bottom: 20px;
                        }}
                        .content {{
                            font-size: 16px;
                            margin-bottom: 20px;
                        }}
                        .footer {{
                            font-size: 14px;
                            color: #777;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>Distributorship Request Submitted</div>
                        <div class='content'>
                            Dear {customerName},
                            <br /><br />
                            Thank you for your interest in becoming a distributor. We have received your distributorship request.
                            Our team will review your application and get back to you shortly.
                            <br /><br />
                            If you have any questions in the meantime, feel free to contact our support team.
                        </div>
                        <div class='footer'>
                            Best regards,
                            <br />
                            The [Your Company Name] Team
                        </div>
                    </div>
                </body>
                </html>";

            return Task.FromResult(emailBody);
        }
        Task<string> GeneratePasswordResetEmailBody(string customerName, string resetUrl);
        Task<string> GenerateRejectEmailBody(string customerName, string resetUrl, string remark);
        Task<string> GenerateDistributorshipRejectEmailBody(string customerName, string resetUrl, string remark);
        Task<string> GenerateKYCApproveEmailBody(string customerName, string resetUrl);

        Task<string> GenerateDistributorshipApproveEmailBody(string customerName, string resetUrl);
        Task<string> GenerateAdminStaffEmailBody(string customerName, string resetUrl, string Password, string email);

        Task<string> GenerateContactUsEmailBody(string firstName, string lastName, string phoneNumber, string email,string message);

    }

}
