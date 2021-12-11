namespace Namek.Library.Entity.Emails
{
    public class EmailTemplateNames
    {
        public const string Master = "Master";

        // booking
        public const string UserBooked = "User.Booked";

        public const string UserBookedToMerchant = "User.Booked.ToMerchant";

        public const string UserBookedToAdmin = "User.Booked.Admin";

        public const string UserBookedConfirmLink = "User.Booked.ConfirmLink";

        public const string UserBookedMerchantConfirm = "User.Booked.MerchantConfirmed";

        public const string UserBookedMerchantDenied = "User.Booked.MerchantDenied";

        public const string UserBookedMerchantRecommend = "User.Booked.MerchantRecommend";

        public const string UserBookedCompleted = "User.Booked.Completed";

        public const string UserBookedCanceled = "User.Booked.Canceled";

        #region customer register

        public const string UserRegisteredMessage = "User.Registered";

        public const string UserRegisteredMessageToAdmin = "User.Registered.Admin";

        public const string UserActivatedMessage = "User.Activated";

        public const string UserActivatedSendDiscountMessage = "User.ActivatedSendDiscount";

        public const string UserActivationLinkMessage = "User.ActivationLink";

        public const string PasswordRecoveryLinkMessage = "Common.PasswordRecovery";

        public const string PasswordChangedMessage = "Common.PasswordChanged";

        public const string UserDeactivatedMessage = "User.Deactivated";

        public const string UserDeactivatedMessageToAdmin = "User.Deactivated.Admin";

        public const string UserAccountDeletedMessage = "User.AccountDeleted";

        public const string UserAccountDeletedMessageToAdmin = "User.AccountDeleted.Admin";

        #endregion
    }
}