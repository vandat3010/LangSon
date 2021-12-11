namespace Namek.Library.Entity.Emails
{
    public class EmailTokenNames
    {
        public const string MessageContent = "{{Message.Content}}";

        public const string ActivationUrl = "{{User.ActivationUrl}}";

        public const string RecoveryUrl = "{{User.RecoveryUrl}}";

        public const string ConfirmLink = "{{User.ConfirmLink}}";

        public const string BookingId = "{{Booking.BookingId}}";

        public const string DiscountCodes = "{{GiftCode.Codes}}";

        public const string DiscountCode = "{{GiftCode.Code}}";

        public const string DiscountAmount = "{{GiftCode.Value}}";

        public const string DiscountQuantity = "{{GiftCode.Quantity}}";

        public const string DiscountCondition = "{{GiftCode.Condition}}";
    }
}