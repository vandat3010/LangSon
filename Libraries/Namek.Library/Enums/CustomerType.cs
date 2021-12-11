using Namek.Resources.MultiLanguage;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Namek.Core.LocalizedAttributes;

namespace Namek.Library.Enums
{
    /// <summary>
    ///     Phân loại khách hàng
    /// </summary>
    public enum CustomerLevel
    {
        /// <summary>
        ///     0: Standard
        /// </summary>
        //[Description("Tiêu chuẩn")] [Display(Name = "Standard")] Standard,
        [LocalizedEnumDescription(ResourceConstants.Enum.CustomerTypeStandard)] Standard = 0,

        /// <summary>
        ///     1: VIP
        /// </summary>
        //[Description("VIP")] [Display(Name = "Vip")] Vip,
        [LocalizedEnumDescription(ResourceConstants.Enum.CustomerTypeVip)] Vip = 1,

        /// <summary>
        ///     2: Super
        /// </summary>
        //[Description("Super")] [Display(Name = "Super")] Super
        [LocalizedEnumDescription(ResourceConstants.Enum.CustomerTypeSuper)] Super = 2,
    }

    public enum LoyaltyTransactionType
    {
        /// <summary>
        /// 0: Trạng thái Điểm không còn khả dụng
        /// </summary>
        [LocalizedEnumDescription(ResourceConstants.Enum.LoyaltyUnavailablePoint)] UnavailablePoint = 0,

        /// <summary>
        /// 1: Trạng thái Điểm vẫn còn khả dụng
        /// </summary>
        [LocalizedEnumDescription(ResourceConstants.Enum.LoyaltyAvailablePoint)] AvailablePoint = 1,

        /// <summary>
        /// 2: Trạng thái đổi quà
        /// </summary>
        [LocalizedEnumDescription(ResourceConstants.Enum.LoyaltyChangeGif)] ChangeGif = 2,

        /// <summary>
        /// 3: Trạng thái điểm hết hạn
        /// </summary>
        [LocalizedEnumDescription(ResourceConstants.Enum.LoyaltyOutOfDate)] OutOfDate = 3,

    }
}