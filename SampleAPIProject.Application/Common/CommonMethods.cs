using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MLMProject.Application.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MLMProject.Application.Common
{
    public static class CommonMethods
    {

        public static Dictionary<string,string> GenerateOtpForLogin(HttpContext httpContext)
        {
            Dictionary<string,string> dic = new Dictionary<string,string>();
            string otpId = GenerateOtpId();
            string otp = GenerateOtp();
            httpContext.Session.SetString("OtpId", otpId);
            httpContext.Session.SetString("Otp", otp);
            httpContext.Session.SetString("OtpTimestamp", DateTime.UtcNow.ToString());
            dic["otp"] = otp;
            dic["otpId"] = otpId;
            return dic;
        }
        public static Dictionary<string, string> GenerateOtpForEmailVerify(HttpContext httpContext)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string otpId = GenerateOtpId();
            string otp = GenerateOtp();
            httpContext.Session.SetString("OtpId", otpId);
            httpContext.Session.SetString("Otp", otp);
            httpContext.Session.SetString("OtpTimestamp", DateTime.UtcNow.ToString());
            dic["otp"] = otp;
            dic["otpId"] = otpId;
            return dic;
        }
        public static Dictionary<string,string> ValidateOtp(string otp, HttpContext httpContext)
        {
            Dictionary<string,string> dic = new Dictionary<string,string>();
            var storedOtpId = httpContext.Session.GetString("OtpId");
            var storedOtp = httpContext.Session.GetString("Otp");
            var timestamp = httpContext.Session.GetString("OtpTimestamp");

            if (storedOtpId == null || storedOtp == null || timestamp == null)
            {
                dic["Status"] = "Failed";
                dic["Message"] = "OTP or OTP ID is not available or has expired.";
                return dic;
            }

            var otpGeneratedTime = DateTime.Parse(timestamp);
            if (DateTime.UtcNow > otpGeneratedTime.AddMinutes(5))
            {
                dic["Status"] = "Failed";
                dic["Message"] = "OTP has expired.";
                return dic;
            }
      
            if (storedOtp == otp)
            {
                dic["Status"] = "Success";
                dic["Message"] = "OTP is valid.";
                return dic;
            }
            else
            {
                dic["Status"] = "Failed";
                dic["Message"] = "Invalid OTP.";
                return dic;
            }
        }

        public static string GenerateOtpId()
        {
            // Generate a unique OTP ID, for example, a GUID
            return Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper().ToString();
        }

        public static string GenerateOtp()
        {
            Random r = new Random();
            string OTP = r.Next(100000, 999999).ToString();
            return OTP;
            //  return "123456";
        }

        public static string EncryptPassword(string password)
        {
            string EncrptKey = "2013;[pnuLIT)WebCodeExpert";
            byte[] byKey = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
            byKey = System.Text.Encoding.UTF8.GetBytes(EncrptKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(password);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }
        public static string DecryptPassword(string encryptpass)
        {
            try
            {
                encryptpass = encryptpass.Replace(" ", "+");
                string DecryptKey = "2013;[pnuLIT)WebCodeExpert";
                byte[] byKey = { };
                byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
                byte[] inputByteArray = new byte[encryptpass.Length];

                byKey = System.Text.Encoding.UTF8.GetBytes(DecryptKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(encryptpass.Replace(" ", "+"));
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GenerateOrderNumber()
        {
            // Get the current date-time in UTC for precision
            string dateTimePart = DateTime.UtcNow.ToString("yyyyMMddHHmmss");

            byte[] randomBytes = new byte[4]; // 4 bytes = 32 bits of randomness
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }

            // Convert the random bytes to a hexadecimal string
            string randomPart = BitConverter.ToString(randomBytes).Replace("-", "").ToLower();

            // Combine the date-time and random parts
            string orderNumber = $"ORD-{dateTimePart}-{randomPart.ToUpper()}";

            return orderNumber;
        }

        private static readonly HttpClient httpClient = new HttpClient();
        public static async Task<string> UploadImageAsync(IFormFile file, string type)
        {
            if (file == null || file.Length == 0)
            {
                return "No file uploaded.";
            }

            try
            {
                // Prepare to send a form-data request to the API
                using var form = new MultipartFormDataContent();
                var fileContent = new StreamContent(file.OpenReadStream());
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                form.Add(fileContent, "file", file.FileName);

                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("x-api-key", "UVhKemFHRmtRV3hwUVc1ellYSnA="); // Add your API key

                var apiUrl = "https://img.devmoral.shop/api/uploads/upload-kyc-files"; // Your API endpoint
                var apiResponse = await httpClient.PostAsync(apiUrl, form);

                if (apiResponse.IsSuccessStatusCode)
                {
                    var responseData = await apiResponse.Content.ReadAsStringAsync();
                    Console.WriteLine($"API Response: {responseData}");

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true // Ignore case sensitivity
                    };
                    var jsonResponse = JsonSerializer.Deserialize<ApiResponse>(responseData, options);
                    if (jsonResponse != null && jsonResponse.Message == "File uploaded successfully!")
                    {
                        var imgUrl = ResponseConstants.imageUrl + jsonResponse.Filepath;
                        return imgUrl;
                    }
                    else
                    {
                        return "File uploaded, but the API response was not successful.";
                    }
                }
                else
                {
                    // Log or handle API response failure
                    var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                    return $"API call failed with status code {apiResponse.StatusCode}: {errorResponse}";
                }
            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
            }
        }

        public class ApiResponse
        {
            public string Message { get; set; }
            public string Filepath { get; set; }
            public string Mimetype { get; set; }
        }
        public static async Task<string> DeleteImage(string file, string type)
        {
            string ftpServer = "ftp://win8239.site4now.net";
            string URLS = "http://emscontent.workanthem.com";
            string ftpUser = "emscontent";
            string ftpPassword = "Anthem@nh22";
            string uploadsFolder = "";
            if (type == "KYC")
            {
                uploadsFolder = "/uploads/22cyk1202/";
            }
            else if (type == "PRODUCTS")
            {
                uploadsFolder = "/uploads/2212prdct02/";
            }
            else
            {
                uploadsFolder = "/uploads/2234rehto02/";
            }

            if (file == null || file.Length == 0)
            {
                return ("No file uploaded.");
            }

            try
            {
               
                // Create the FTP URI with the 'uploads' folder
                var ftpUri = new Uri(new Uri(ftpServer), $"{uploadsFolder}{file}");
                var request = (FtpWebRequest)WebRequest.Create(ftpUri);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;
                using (var response = (FtpWebResponse)await request.GetResponseAsync())
                {
                    return ($"File deleted successfully. Status: {response.StatusDescription}");
                }
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }
        }
        public static async Task<string> UpdateImageAsync(string existingFileName, IFormFile newFile, string type)
        {
            string ftpServer = "ftp://win8239.site4now.net";
            string baseURL = "http://emscontent.workanthem.com";
            string ftpUser = "emscontent";
            string ftpPassword = "Anthem@nh22";
            string uploadsFolder = "";
            if (type == "KYC")
            {
                uploadsFolder = "/uploads/22cyk1202/";
            }
            else if (type == "PRODUCTS")
            {
                uploadsFolder = "/uploads/2212prdct02/";
            }
            else
            {
                uploadsFolder = "/uploads/2234rehto02/";
            }

            if (newFile == null || newFile.Length == 0)
            {
                return ("No new file uploaded.");
            }

            try
            {
                // First, delete the existing image (if it exists)
                if (!string.IsNullOrEmpty(existingFileName))
                {
                    string deleteResult = await DeleteImage(existingFileName,type);
                    if (deleteResult != "File deleted successfully.")
                    {
                        return ($"Failed to delete existing file: {deleteResult}");
                    }
                }

                // Generate unique file name with timestamp for the new file
                var timestamp = DateTime.Now.ToString("ddMMyyyy_HHmmss_fff");
                var fileName = $"{StringToBase64(newFile.FileName)}{Path.GetExtension(newFile.FileName)}";
                //var fileName = $"{Path.GetFileNameWithoutExtension(newFile.FileName)}_{timestamp}{Path.GetExtension(newFile.FileName)}";

                // Create the FTP URI for the new image
                var ftpUri = new Uri(new Uri(ftpServer), $"{uploadsFolder}{fileName}");
                var request = (FtpWebRequest)WebRequest.Create(ftpUri);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                // Upload the new file to the FTP server
                using (var requestStream = await request.GetRequestStreamAsync())
                using (var fileStream = newFile.OpenReadStream())
                {
                    await fileStream.CopyToAsync(requestStream);
                }

                // Get response and construct the URL for the uploaded file
                using (var response = (FtpWebResponse)await request.GetResponseAsync())
                {
                    // Construct the URL of the uploaded image
                    var uploadedFileUrl = $"{baseURL}{uploadsFolder}{fileName}";
                    return (uploadedFileUrl);
                }
            }
            catch (Exception ex)
            {
                return ($"Error updating file: {ex.Message}");
            }
        }
        public static string StringToBase64(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                throw new ArgumentException("Input string cannot be null or empty.");
            }

            // Convert the string to a byte array
            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);

            // Convert the byte array to Base64
            return Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64ToString(string base64Encoded)
        {
            if (string.IsNullOrEmpty(base64Encoded))
            {
                throw new ArgumentException("Base64 string cannot be null or empty.");
            }

            // Convert the Base64 string back to a byte array
            byte[] base64Bytes = Convert.FromBase64String(base64Encoded);

            // Convert the byte array back to a string
            return System.Text.Encoding.UTF8.GetString(base64Bytes);
        }
    }
}
