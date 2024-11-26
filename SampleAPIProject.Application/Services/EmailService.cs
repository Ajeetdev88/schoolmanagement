using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using MLMProject.Application.IService;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace MLMProject.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SendmailAsync(string email, string subject, string body)
        {
            try
            {
                var smtpSettings = _configuration.GetSection("SmtpSettings").Get<SmtpSettings>();
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(smtpSettings.SenderName, smtpSettings.SenderEmail));
                message.To.Add(new MailboxAddress("", email)); // Add recipient email address here
                message.Subject = subject;
                message.Body = new TextPart(TextFormat.Html) { Text = body };
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(smtpSettings.Server, smtpSettings.Port, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(smtpSettings.UserName, smtpSettings.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
                int res = 1;
                return res;
            }
            catch (Exception ex)
            {
                int res = 0;
                return res;
            }
        }

        public async Task<string> GenerateEmailBody(string customerName, string otp,string otpid)
        {
            return $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>OTP Verification</title>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            margin: 0;
                            padding: 0;
                            background-color: #f4f4f4;
                        }}
                        .container {{
                            width: 100%;
                            max-width: 600px;
                            margin: 0 auto;
                            background-color: #ffffff;
                            padding: 20px;
                            border-radius: 8px;
                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        }}
                        .header {{
                            text-align: center;
                            padding: 10px 0;
                            border-bottom: 1px solid #dddddd;
                        }}
                        .header img {{
                            max-width: 150px;
                        }}
                        .content {{
                            padding: 20px;
                            text-align: center;
                        }}
                        .content h1 {{
                            color: #333333;
                        }}
                        .content p {{
                            color: #555555;
                            line-height: 1.5;
                        }}
                        .otp {{
                            font-size: 24px;
                            font-weight: bold;
                            color: #007bff;
                            margin: 20px 0;
                        }}
                        .footer {{
                            text-align: center;
                            padding: 10px 0;
                            border-top: 1px solid #dddddd;
                            font-size: 12px;
                            color: #888888;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <img src='https://admin.devmoral.com/@/assets/logo-dark-DZIRIzfk.png' alt='MulitiKart Logo'>
                        </div>
                        <div class='content'>
                            <h1>OTP Verification</h1>
                            <p>Dear {customerName},</p>
                            <p>Thank you for using our service. To verify your login, please use the following One-Time Password (OTP)</p>
                            <div class='otp'>OTP : {otp}</div>
                            <div class='otp'>OTP-ID : {otpid}</div>
                            <p>This OTP is valid for 10 minutes. If you did not request this OTP, please ignore this email.</p>
                        </div>
                        <div class='footer'>
                            <p>&copy; 2024 Ecom-Marketing Site. All rights reserved.</p>
                        </div>
                    </div>
                </body>
                </html>
                ";
        }
        public async Task<string> GeneratePasswordResetEmailBody(string customerName, string resetUrl)
        {
            return $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Password Reset</title>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            margin: 0;
                            padding: 0;
                            background-color: #f4f4f4;
                        }}
                        .container {{
                            width: 100%;
                            max-width: 600px;
                            margin: 0 auto;
                            background-color: #ffffff;
                            padding: 20px;
                            border-radius: 8px;
                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        }}
                        .header {{
                            text-align: center;
                            padding: 10px 0;
                            border-bottom: 1px solid #dddddd;
                        }}
                        .header img {{
                            max-width: 150px;
                        }}
                        .content {{
                            padding: 20px;
                            text-align: center;
                        }}
                        .content h1 {{
                            color: #333333;
                        }}
                        .content p {{
                            color: #555555;
                            line-height: 1.5;
                        }}
                        .reset-link {{
                            display: inline-block;
                            margin: 20px 0;
                            padding: 10px 20px;
                            font-size: 18px;
                            color: white !important;
                            background-color: #007bff;
                            text-decoration: none;
                            border-radius: 5px;
                        }}
                        .footer {{
                            text-align: center;
                            padding: 10px 0;
                            border-top: 1px solid #dddddd;
                            font-size: 12px;
                            color: #888888;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <img src='https://admin.devmoral.com/@/assets/logo-dark-DZIRIzfk.png' alt='E-commerce Logo'>
                        </div>
                        <div class='content'>
                            <h1>Password Reset Request</h1>
                            <p>Dear {customerName},</p>
                            <p>We received a request to reset your password. Click the button below to reset your password:</p>
                            <a href='{resetUrl}' class='reset-link'>Reset Password</a>
                            <p>If you did not request a password reset, please ignore this email.</p>
                        </div>
                        <div class='footer'>
                            <p>&copy; 2024 MulitKart Site. All rights reserved.</p>
                        </div>
                    </div>
                </body>
                </html>
                ";
        }
        public async Task<string> GenerateKYCApproveEmailBody(string customerName, string resetUrl)
        {
            return $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>KYC Approved</title>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            margin: 0;
                            padding: 0;
                            background-color: #f4f4f4;
                        }}
                        .container {{
                            width: 100%;
                            max-width: 600px;
                            margin: 0 auto;
                            background-color: #ffffff;
                            padding: 20px;
                            border-radius: 8px;
                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        }}
                        .header {{
                            text-align: center;
                            padding: 10px 0;
                            border-bottom: 1px solid #dddddd;
                        }}
                        .header img {{
                            max-width: 150px;
                        }}
                        .content {{
                            padding: 20px;
                            text-align: center;
                        }}
                        .content h1 {{
                            color: #333333;
                        }}
                        .content p {{
                            color: #555555;
                            line-height: 1.5;
                        }}
                        .reset-link {{
                            display: inline-block;
                            margin: 20px 0;
                            padding: 10px 20px;
                            font-size: 18px;
                            color: white !important;
                            background-color: #007bff;
                            text-decoration: none;
                            border-radius: 5px;
                        }}
                        .footer {{
                            text-align: center;
                            padding: 10px 0;
                            border-top: 1px solid #dddddd;
                            font-size: 12px;
                            color: #888888;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
    <div class='header'>
        <img src='https://admin.devmoral.com/@/assets/logo-dark-DZIRIzfk.png' alt='E-commerce Logo'>
    </div>
    <div class='content'>
        <h1>KYC Approved</h1>
        <p>Dear {customerName},</p>
        <p>
            Congratulations! We are pleased to inform you that your KYC has been successfully approved. You now have full access to all our platform's features and services.
        </p>
        <p>
            If you have any questions or need further assistance, please don't hesitate to contact our support team.
        </p>
        <a href='{resetUrl}' class='reset-link'>Log In Now</a>
    </div>
    <div class='footer'>
        <p>&copy; 2024 Ecom-Marketing Site. All rights reserved.</p>
    </div>
</div>

                </body>
                </html>
                ";
        }
        public async Task<string> GenerateDistributorshipApproveEmailBody(string customerName, string resetUrl)
        {
            return $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Distributorship Approved</title>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            margin: 0;
                            padding: 0;
                            background-color: #f4f4f4;
                        }}
                        .container {{
                            width: 100%;
                            max-width: 600px;
                            margin: 0 auto;
                            background-color: #ffffff;
                            padding: 20px;
                            border-radius: 8px;
                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        }}
                        .header {{
                            text-align: center;
                            padding: 10px 0;
                            border-bottom: 1px solid #dddddd;
                        }}
                        .header img {{
                            max-width: 150px;
                        }}
                        .content {{
                            padding: 20px;
                            text-align: center;
                        }}
                        .content h1 {{
                            color: #333333;
                        }}
                        .content p {{
                            color: #555555;
                            line-height: 1.5;
                        }}
                        .reset-link {{
                            display: inline-block;
                            margin: 20px 0;
                            padding: 10px 20px;
                            font-size: 18px;
                            color: white !important;
                            background-color: #007bff;
                            text-decoration: none;
                            border-radius: 5px;
                        }}
                        .footer {{
                            text-align: center;
                            padding: 10px 0;
                            border-top: 1px solid #dddddd;
                            font-size: 12px;
                            color: #888888;
                        }}
                    </style>
                </head>
                                   <body>
                      <div class='container'>
                        <div class='header'>
                          <img src='https://admin.devmoral.com/@/assets/logo-dark-DZIRIzfk.png' alt='E-commerce Logo'>
                        </div>
                        <div class='content'>
                          <h1>Distributorship Approved</h1>
                          <p>Dear {customerName},</p>
                          <p>
                            Congratulations! We are thrilled to inform you that your request for distributorship has been successfully approved. You are now officially a distributor on our platform, with access to exclusive features and opportunities.
                          </p>
                          <p>
                            If you have any questions or need further assistance, please don't hesitate to contact our support team.
                          </p>
                          <a href='{resetUrl}' class='reset-link'>Log In Now</a>
                        </div>
                        <div class='footer'>
                          <p>&copy; 2024 Ecom-Marketing Site. All rights reserved.</p>
                        </div>
                      </div>
                    </body>

                </html>
                ";
        }
        public async Task<string> GenerateRejectEmailBody(string customerName, string resetUrl,string remark)
        {
            return $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>KYC Rejected</title>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            margin: 0;
                            padding: 0;
                            background-color: #f4f4f4;
                        }}
                        .container {{
                            width: 100%;
                            max-width: 600px;
                            margin: 0 auto;
                            background-color: #ffffff;
                            padding: 20px;
                            border-radius: 8px;
                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        }}
                        .header {{
                            text-align: center;
                            padding: 10px 0;
                            border-bottom: 1px solid #dddddd;
                        }}
                        .header img {{
                            max-width: 150px;
                        }}
                        .content {{
                            padding: 20px;
                            text-align: center;
                        }}
                        .content h1 {{
                            color: #333333;
                        }}
                        .content p {{
                            color: #555555;
                            line-height: 1.5;
                        }}
                        .reset-link {{
                            display: inline-block;
                            margin: 20px 0;
                            padding: 10px 20px;
                            font-size: 18px;
                            color: white !important;
                            background-color: #007bff;
                            text-decoration: none;
                            border-radius: 5px;
                        }}
                        .footer {{
                            text-align: center;
                            padding: 10px 0;
                            border-top: 1px solid #dddddd;
                            font-size: 12px;
                            color: #888888;
                        }}
                    </style>
                </head>
                <body>
                   <div class='container'>
    <div class='header'>
        <img src='https://admin.devmoral.com/@/assets/logo-dark-DZIRIzfk.png' alt='E-commerce Logo'>
    </div>
    <div class='content'>
        <h1>KYC Rejected</h1>
        <p>Dear {customerName},</p>
        <p>
            We have reviewed your KYC submission and regret to inform you that your KYC has been rejected due to the following reason: <strong>{remark}</strong>.
        </p>
        <p>
            Please log in to your account to review the details and make the necessary corrections. We encourage you to resubmit your KYC documents at your earliest convenience to avoid any disruptions in your account services.
        </p>
        <a href='{resetUrl}' class='reset-link'>Log In Now</a>
    </div>
    <div class='footer'>
        <p>&copy; 2024 Ecom-Marketing Site. All rights reserved.</p>
    </div>
</div>

                </body>
                </html>
                ";
        }

        public async Task<string> GenerateDistributorshipRejectEmailBody(string customerName, string resetUrl, string remark)
        {
            return $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Distributorship Rejected</title>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            margin: 0;
                            padding: 0;
                            background-color: #f4f4f4;
                        }}
                        .container {{
                            width: 100%;
                            max-width: 600px;
                            margin: 0 auto;
                            background-color: #ffffff;
                            padding: 20px;
                            border-radius: 8px;
                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        }}
                        .header {{
                            text-align: center;
                            padding: 10px 0;
                            border-bottom: 1px solid #dddddd;
                        }}
                        .header img {{
                            max-width: 150px;
                        }}
                        .content {{
                            padding: 20px;
                            text-align: center;
                        }}
                        .content h1 {{
                            color: #333333;
                        }}
                        .content p {{
                            color: #555555;
                            line-height: 1.5;
                        }}
                        .reset-link {{
                            display: inline-block;
                            margin: 20px 0;
                            padding: 10px 20px;
                            font-size: 18px;
                            color: white !important;
                            background-color: #007bff;
                            text-decoration: none;
                            border-radius: 5px;
                        }}
                        .footer {{
                            text-align: center;
                            padding: 10px 0;
                            border-top: 1px solid #dddddd;
                            font-size: 12px;
                            color: #888888;
                        }}
                    </style>
                </head>
               <body>
                  <div class='container'>
                    <div class='header'>
                      <img src='https://admin.devmoral.com/@/assets/logo-dark-DZIRIzfk.png' alt='E-commerce Logo'>
                    </div>
                    <div class='content'>
                      <h1>Distributorship Rejected</h1>
                      <p>Dear {customerName},</p>
                      <p>
                        We have reviewed your distributorship application and regret to inform you that it has been rejected due to the following reason: <strong>{remark}</strong>.
                      </p>
                      <p>
                        Please log in to your account to review the details and address the issue. We encourage you to make the necessary adjustments and resubmit your application at your earliest convenience.
                      </p>
                      <a href='{resetUrl}' class='reset-link'>Log In Now</a>
                    </div>
                    <div class='footer'>
                      <p>&copy; 2024 Ecom-Marketing Site. All rights reserved.</p>
                    </div>
                  </div>
                </body>

                </html>
                ";
        }

        public async Task<string> GenerateAdminStaffEmailBody(string customerName, string resetUrl,string Password,string email)
        {
            return $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Admin Staff Reset Password</title>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            margin: 0;
                            padding: 0;
                            background-color: #f4f4f4;
                        }}
                        .container {{
                            width: 100%;
                            max-width: 600px;
                            margin: 0 auto;
                            background-color: #ffffff;
                            padding: 20px;
                            border-radius: 8px;
                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        }}
                        .header {{
                            text-align: center;
                            padding: 10px 0;
                            border-bottom: 1px solid #dddddd;
                        }}
                        .header img {{
                            max-width: 150px;
                        }}
                        .content {{
                            padding: 20px;
                            text-align: center;
                        }}
                        .content h1 {{
                            color: #333333;
                        }}
                        .content p {{
                            color: #555555;
                            line-height: 1.5;
                        }}
                        .reset-link {{
                            display: inline-block;
                            margin: 20px 0;
                            padding: 10px 20px;
                            font-size: 18px;
                            color: white !important;
                            background-color: #007bff;
                            text-decoration: none;
                            border-radius: 5px;
                        }}
                        .footer {{
                            text-align: center;
                            padding: 10px 0;
                            border-top: 1px solid #dddddd;
                            font-size: 12px;
                            color: #888888;
                        }}
                    </style>
                </head>
                <body>
                   <div class='container'>
    <div class='header'>
        <img src='https://admin.devmoral.com/@/assets/logo-dark-DZIRIzfk.png' alt='E-commerce Logo'>
    </div>
    <div class='content'>
        <h1>Password Reset Successful</h1>
        <p>Dear {customerName},</p>
        <p>
            We have successfully reset your password as per your request. Your new password for accessing your account associated with the email <strong>{email}</strong> is: <strong>{Password}</strong>.
        </p>
        <p>
            For your security, we recommend that you log in immediately and update your password to something you can easily remember but is also secure.
        </p>
        <a href='{resetUrl}' class='reset-link'>Log In Now</a>
    </div>
    <div class='footer'>
        <p>&copy; 2024 Ecom-Marketing Site. All rights reserved.</p>
    </div>
</div>

                </body>
                </html>
                ";
        }


        public async Task<string> GenerateEmailVerificationEmailBody(string customerName, string otp, string otpId)
        {
            return $@"
        <!DOCTYPE html>
        <html lang='en'>
        <head>
            <meta charset='UTF-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            <title>Email Verification</title>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    margin: 0;
                    padding: 0;
                    background-color: #f4f4f4;
                }}
                .container {{
                    width: 100%;
                    max-width: 600px;
                    margin: 0 auto;
                    background-color: #ffffff;
                    padding: 20px;
                    border-radius: 8px;
                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                }}
                .header {{
                    text-align: center;
                    padding: 10px 0;
                    border-bottom: 1px solid #dddddd;
                }}
                .header img {{
                    max-width: 150px;
                }}
                .content {{
                    padding: 20px;
                    text-align: center;
                }}
                .content h1 {{
                    color: #333333;
                }}
                .content p {{
                    color: #555555;
                    line-height: 1.5;
                }}
                .otp {{
                   
                            font-size: 24px;
                            font-weight: bold;
                            color: #007bff;
                            margin: 20px 0;
                }}
                .footer {{
                    text-align: center;
                    padding: 10px 0;
                    border-top: 1px solid #dddddd;
                    font-size: 12px;
                    color: #888888;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <div class='header'>
                    <img src='https://admin.devmoral.com/@/assets/logo-dark-DZIRIzfk.png' alt='E-commerce Logo'>
                </div>
                <div class='content'>
                    <h1>Email Verification</h1>
                    <p>Dear {customerName},</p>
                    <p>Thank you for registering with us. To complete the verification of your email address, please use the following One-Time Password </p>
                    <div class='otp'>OTP : {otp}</div>
                    <div class='otp'>OTP-ID : {otpId}</div>
                    <p>Alternatively, you can use the OTP ID to verify your email:</p>
                   
                    <p>If you did not request this verification, please ignore this email.</p>
                </div>
                <div class='footer'>
                    <p>&copy; 2024 MulitKart Site. All rights reserved.</p>
                </div>
            </div>
        </body>
        </html>
        ";
        }


        public async Task<string> GenerateContactUsEmailBody(string firstName, string lastName, string phoneNumber, string email, string message)
        {
            return $@"
        <!DOCTYPE html>
        <html lang='en'>
        <head>
            <meta charset='UTF-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            <title>New Contact Us Message</title>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    margin: 0;
                    padding: 0;
                    background-color: #f4f4f4;
                }}
                .container {{
                    width: 100%;
                    max-width: 600px;
                    margin: 0 auto;
                    background-color: #ffffff;
                    padding: 20px;
                    border-radius: 8px;
                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                }}
                .header {{
                    text-align: center;
                    padding: 10px 0;
                    border-bottom: 1px solid #dddddd;
                }}
                .header img {{
                    max-width: 150px;
                }}
                .content {{
                    padding: 20px;
                    text-align: left;
                }}
                .content h1 {{
                    color: #333333;
                    font-size: 24px;
                }}
                .content p {{
                    color: #555555;
                    line-height: 1.6;
                }}
                .user-info {{
                    margin-top: 20px;
                }}
                .footer {{
                    text-align: center;
                    padding: 10px 0;
                    border-top: 1px solid #dddddd;
                    font-size: 12px;
                    color: #888888;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <div class='header'>
                    <img src='https://admin.devmoral.com/@/assets/logo-dark-DZIRIzfk.png' alt='E-commerce Logo'>
                </div>
                <div class='content'>
                    <h1>New Contact Us Message</h1>
                    <p>
                        You have received a new message from the contact form on your website. Here are the details:
                    </p>
                    <div class='user-info'>
                        <p><strong>Full Name:</strong> {firstName} {lastName}</p>
                        <p><strong>Phone Number:</strong> {phoneNumber}</p>
                        <p><strong>Email:</strong> {email}</p>
                        <p><strong>Message:</strong> {message}</p>
                    </div>
                    <p>Please review and respond to the user as soon as possible.</p>
                </div>
                <div class='footer'>
                    <p>&copy; 2024 Ecom-Marketing Site. All rights reserved.</p>
                </div>
            </div>
        </body>
        </html>
    ";
        }




        public class SmtpSettings
        {
            public string Server { get; set; }
            public int Port { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string SenderName { get; set; }
            public string SenderEmail { get; set; }
        }

    }
}
