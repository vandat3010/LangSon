using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Namek.Library.Enums
{
    public enum StatusCodeEnum
    {
        /// <summary>
        ///     Bạn chưa có giỏ hàng nào
        /// </summary>
        [Description("Bạn chưa có giỏ hàng nào")] [Display(Name = "ShoppingCartHasNoItem")] ShoppingCartHasNoItem = -3,

        /// <summary>
        ///     Chưa có gói dịch vụ nào trong giỏ hàng
        /// </summary>
        [Description("Chưa có gói dịch vụ nào trong giỏ hàng")] [Display(Name = "ShoppingCartHasNoServicePack")]
        ShoppingCartHasNoServicePack = -4,

        /// <summary>
        ///     Đặt hàng không thành công
        /// </summary>
        [Description("Đặt hàng không thành công")] [Display(Name = "ShoppingCartOrderFail")] ShoppingCartOrderFail = -5,
        [Description("Tài khoản đã hết tiền, không thể thanh toán gọi dịch vụ")] [Display(Name = "ShoppingCartHasNoMoney")] ShoppingCartHasNoMoney = -6,  
        [Description("Tiền trong tài khoản nhỏ hơn tiền thanh toán đơn hàng")] [Display(Name = "ShoppingCartHasNoOrderBiggerBalanceAvailable")] ShoppingCartHasNoOrderBiggerBalanceAvailable = -7,

    }
}