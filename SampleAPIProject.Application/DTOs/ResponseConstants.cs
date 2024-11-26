using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLMProject.Application.DTOs
{
    public class ResponseConstants
    {
        public const string commonUrl = "devmoral.com";
        public const string imageUrl = "https://img.devmoral.shop/";


        #region AuthMessages


        #endregion AuthMessages
        //Required

        public const string InvalidEmail = "Invalid Email.";
        public const string PasswordRequired = "Invalid Password.";

        //Invalid
        public const string InvalidCredentials= "Invalid Email or Password.";

        public const string AccountNotActive = "Your account is inactive. Please contact the admin.";
        public const string AccountDeleted = "Account does not exists.";

        public const string CredentialsRequired = "Email address and password are required.";

        public const string ResetPasswordFailed = "Unable to reset Password. Try Again!";

        //Email
        public const string FPEmailSent = "Email sent! Please check your email.";

        //success
        public const string ChangePasswordSuccess = "Password changed successfully.";
        public const string Multiplelogin = "Account is locked due to multiple failed login attempts contact with admin or reset password.";
        public const string NoAccess = "Unauthorized Access.";
        public const string FillOtpToVerify = "Please enter your OTP to verify.";
        public const string ExpireOTP = "OTP or OTP ID is not available or has expired.";
        public const string ExpiredOTP = "OTP has expired.";
        public const string ValidOTP = "OTP is valid.";
        public const string InValidOTP = "Invalid OTP.";
        public const string InvalidUserdata = "Invalid user data.";
        public const string InvalidAddressdata = "Invalid user data.";

        public const string PTA = "Please try again.";
        public const string OTPEmailCheck = "Please check your email and enter OTP to verify email.";
        public const string NotSendOTP = "Unable to send OTP.";
        public const string EmailCheckResetLink = "Please check your email and reset password using provided url.";
        public const string NoAccessToResetPass = "You have no access to forgot password.";
        public const string OldPassNoMatch = "Old Password does not matched.";
        public const string ErrorUpdateData = "Error updating user.";
        public const string UserNotFound = "User not found.";
        public const string UserTypeNotFound = "User Type not found.";
        public const string AdminNotFound = "Admin not found.";
        public const string UserDeleted = "User Deleted Successfully.";
        public const string UserActivate = "User Activate Successfully.";
        public const string UserDeactivate = "User Deactivate Successfully.";
        public const string UserLock = "User Locked Successfully.";
        public const string UserUnlock = "User Unlocked Successfully.";
        public const string AdminDeleted = "Admin Deleted Successfully.";
        public const string AdminActivate = "Admin Activate Successfully.";
        public const string AdminDeactivate = "Admin Deactivate Successfully.";
        public const string NoDataFound = "No Data Found.";
        public const string DistributorRequestSuccess = "Distributorship request have been submitted. Please check your email.";
        public const string DistributorRequestFailed = "Unable to submit Distribution Request.";
        //Addresses 
        public const string AddressDeleted = "Address removed Successfully.";
        public const string DefaultAddressSuccess = "Default Address changed.";
        public const string AddressAdded = "Address added Successfully.";
        public const string AddressUpdated = "Address updated Successfully.";
        //image upload type
        public const string kyc = "kyc";
        public const string product = "product";
        public const string homepage = "homepage";
        //Brand
        public const string BrandAdded = "Brand added Successfully.";
        public const string BrandUpdated = "Brand updated Successfully.";
        public const string BrandDeleted = "Brand deleted Successfully.";
        public const string BrandNameAlreadyExists = "Brand name already exists.";
        //Category

        public const string CategoryAdded = "Category added Successfully.";
        public const string CategoryUpdated = "Category updated Successfully.";
        public const string CategoryDeleted = "Category deleted Successfully.";
        public const string CategoryNameAlreadyExists = "Category already exists.";

        //Tags
        public const string TagAdded = "Tag added Successfully.";
        public const string TagUpdated = "Tag updated Successfully.";
        public const string TagDeleted = "Tag deleted Successfully.";
        public const string TagNameAlreadyExists = "Tag name already exists.";
        //Collection
        public const string CollectionAdded = "Collection added Successfully.";
        public const string CollectionUpdated = "Collection updated Successfully.";
        public const string CollectionDeleted = "Collection deleted Successfully.";
        public const string CollectionNameAlreadyExists = "Collection name already exists.";
        //ProductType
        public const string ProductTypeAdded = "Product Type added Successfully.";
        public const string ProductTypeUpdated = "Product Type updated Successfully.";
        public const string ProductTypeDeleted = "Product Type deleted Successfully.";
        public const string ProductTypeAlreadyExists = "Product type already exists.";
        //AttributeDefinition
        public const string AttributeDefAdded = "Attribute Definition added Successfully.";
        public const string AttributeDefUpdated = "Attribute Definition updated Successfully.";
        public const string AttributeDefDeleted = "Attribute Definition deleted Successfully.";
        public const string AttributeDefAlreadyExists = "Attribute Definition already exists.";

        //AttributeValue
        public const string AttributeValueAdded = "Attribute Value added Successfully.";
        public const string AttributeValueUpdated = "Attribute Value updated Successfully.";
        public const string AttributeValueDeleted = "Attribute Value deleted Successfully.";
        public const string AttributeValueAlreadyExists = "Attribute value already exists.";

        //ProductImage
        public const string ProductImageAdded = "Product Image added Successfully.";
        public const string ProductImageUpdated = "Product Image updated Successfully.";
        public const string ProductImageDeleted = "Product Image deleted Successfully.";

        //Product
        public const string ProductAdded = "Product added Successfully.";
        public const string ProductUpdated = "Product updated Successfully.";
        public const string ProductDeleted = "Product deleted Successfully.";

        //Variant
        public const string VariantAdded = "Variant added Successfully.";
        public const string VariantUpdated = "Variant updated Successfully.";
        public const string VariantDeleted = "Variant deleted Successfully.";
        //Variant
        public const string InventoryAdded = "Inventory added Successfully.";
        public const string InventoryUpdated = "Inventory updated Successfully.";
        //Variant
        public const string CartAdded = "Cart added Successfully.";
        public const string CartUpdated = "Cart updated Successfully.";
        public const string CartDeleted = "Cart Item deleted Successfully.";

        //Variant
        public const string OrderAdded = "Order placed Successfully.";
        public const string OrderCancelled = "Order cancelled Successfully.";

        public const string UnableToDelete = "Unable to delete data as it is referenced in another record.";



    }
}
