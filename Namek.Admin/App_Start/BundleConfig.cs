using System.Web.Optimization;

namespace Namek.Admin
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));
            //Added for toaster
            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                       "~/Scripts/toastr.js*",
                       "~/Scripts/toastrImp.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css",
                "~/assets/global/plugins/select2/css/select2-bootstrap.min.css",
                "~/assets/global/plugins/select2/css/select2.min.css",
                "~/assets/global/plugins/select2/js/select2.full.js",
                "~/assets/global/plugins/select2/js/select2.full.min.js"
                ));


            //Modify for toastr
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css",
                                                                "~/Content/toastr.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            #region Fornt End

            bundles.Add(new ScriptBundle("~/scripts/front-end/base").Include(
                "~/assets/front-end/js/jquery.min.js",
                "~/assets/front-end/js/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/scripts/cloud-server-detail").Include(
                "~/Scripts/front-end/knockout-3.4.2.debug.js",
                "~/Scripts/front-end/knockout.mapping-latest.debug.js",
                "~/Scripts/front-end/lodash.js",
                "~/Scripts/front-end/plugins/globinfo/globalize.js",
                "~/Scripts/front-end/plugins/globinfo/globalize.culture.vi-VN.js",
                "~/Scripts/front-end/plugins/input-mask/jquery.inputmask.js",
                "~/Scripts/front-end/plugins/input-mask/jquery.inputmask.numeric.extensions.js",
                "~/Scripts/front-end/toastr.js",
                "~/Scripts/front-end/plugins/sweetalert2/sweetalert2.js",
                "~/Scripts/front-end/common/common.js",
                "~/assets/front-end/plugins/ion.Ranger/ion.rangeSlider.min.js",
                "~/Scripts/front-end/modelView/cloudServerModel.js"));

            bundles.Add(new ScriptBundle("~/scripts/trial-cloud-server-detail").Include(
                "~/Scripts/front-end/knockout-3.4.2.debug.js",
                "~/Scripts/front-end/knockout.mapping-latest.debug.js",
                "~/Scripts/front-end/lodash.js",
                "~/Scripts/front-end/plugins/globinfo/globalize.js",
                "~/Scripts/front-end/plugins/globinfo/globalize.culture.vi-VN.js",
                "~/Scripts/front-end/plugins/input-mask/jquery.inputmask.js",
                "~/Scripts/front-end/plugins/input-mask/jquery.inputmask.numeric.extensions.js",
                "~/Scripts/front-end/toastr.js",
                "~/Scripts/front-end/plugins/sweetalert2/sweetalert2.js",
                "~/Scripts/front-end/common/common.js",
                "~/assets/front-end/plugins/ion.Ranger/ion.rangeSlider.min.js",
                "~/Scripts/front-end/modelView/cloudServerTrial.js"));

            bundles.Add(new ScriptBundle("~/scripts/trial-switch-to-real").Include(
                "~/Scripts/front-end/knockout-3.4.2.debug.js",
                "~/Scripts/front-end/knockout.mapping-latest.debug.js",
                "~/Scripts/front-end/lodash.js",
                "~/Scripts/front-end/plugins/globinfo/globalize.js",
                "~/Scripts/front-end/plugins/globinfo/globalize.culture.vi-VN.js",
                "~/Scripts/front-end/plugins/input-mask/jquery.inputmask.js",
                "~/Scripts/front-end/plugins/input-mask/jquery.inputmask.numeric.extensions.js",
                "~/Scripts/front-end/toastr.js",
                "~/Scripts/front-end/plugins/sweetalert2/sweetalert2.js",
                "~/Scripts/front-end/common/common.js",
                "~/assets/front-end/plugins/ion.Ranger/ion.rangeSlider.min.js",
                "~/Scripts/front-end/modelView/cloudServerSwitchToReal.js"));

            bundles.Add(new ScriptBundle("~/scripts/shoppingcart").Include(
                "~/Scripts/front-end/toastr.js",
                "~/Scripts/front-end/plugins/sweetalert2/sweetalert2.js",
                "~/Scripts/front-end/js/shopping-cart.js"));

            bundles.Add(new ScriptBundle("~/scripts/shoppingcart-confirm").Include(
                "~/Scripts/front-end/knockout-3.4.2.debug.js",
                "~/Scripts/front-end/toastr.js",
                "~/assets/front-end/plugins/tags-input/bootstrap-tagsinput.min.js",
                "~/Scripts/promise.min.js",
                "~/Scripts/front-end/plugins/sweetalert2/sweetalert2.js",
                "~/Scripts/front-end/modelView/shoppingCartConfirmModel.js"));

            bundles.Add(new ScriptBundle("~/scripts/shoppingcart-payment").Include(
                "~/Scripts/front-end/knockout-3.4.2.debug.js",
                "~/Scripts/front-end/toastr.js",
                "~/Scripts/front-end/plugins/sweetalert2/sweetalert2.js",
                "~/Scripts/front-end/modelView/shoppingCartPaymentModel.js"));

            bundles.Add(new ScriptBundle("~/scripts/domain").Include(
                "~/Scripts/front-end/knockout-3.4.2.debug.js",
                "~/Scripts/front-end/knockout.mapping-latest.debug.js",
                "~/Scripts/front-end/lodash.js",
                "~/Scripts/front-end/plugins/globinfo/globalize.js",
                "~/Scripts/front-end/plugins/globinfo/globalize.culture.vi-VN.js",
                "~/Scripts/front-end/plugins/input-mask/jquery.inputmask.js",
                "~/Scripts/front-end/plugins/input-mask/jquery.inputmask.numeric.extensions.js",
                "~/Scripts/front-end/toastr.js",
                "~/Scripts/front-end/plugins/sweetalert2/sweetalert2.js",
                "~/Scripts/front-end/common/common.js",
                "~/Scripts/front-end/modelView/domainModel.js"));

            bundles.Add(new ScriptBundle("~/scripts/domain-confirm").Include(
                "~/Scripts/front-end/knockout-3.4.2.debug.js",
                "~/Scripts/front-end/knockout.mapping-latest.debug.js",
                "~/Scripts/front-end/lodash.js",
                "~/Scripts/front-end/plugins/globinfo/globalize.js",
                "~/Scripts/front-end/plugins/globinfo/globalize.culture.vi-VN.js",
                "~/Scripts/front-end/plugins/input-mask/jquery.inputmask.js",
                "~/Scripts/front-end/plugins/input-mask/jquery.inputmask.numeric.extensions.js",
                "~/Scripts/front-end/moment-with-locales.min.js",
                "~/Scripts/front-end/toastr.js",
                "~/Scripts/front-end/plugins/sweetalert2/sweetalert2.js",
                "~/Scripts/front-end/common/common.js",
                "~/Scripts/front-end/modelView/domainConfirmModel.js"));

            bundles.Add(new ScriptBundle("~/scripts/domain-register").Include(
                "~/Scripts/front-end/knockout-3.4.2.debug.js",
                "~/Scripts/front-end/knockout.mapping-latest.debug.js",
                "~/Scripts/front-end/lodash.js",
                "~/Scripts/front-end/plugins/globinfo/globalize.js",
                "~/Scripts/front-end/plugins/globinfo/globalize.culture.vi-VN.js",
                "~/Scripts/front-end/plugins/input-mask/jquery.inputmask.js",
                "~/Scripts/front-end/plugins/input-mask/jquery.inputmask.numeric.extensions.js",
                "~/Scripts/front-end/toastr.js",
                "~/Scripts/front-end/plugins/sweetalert2/sweetalert2.js",
                "~/Scripts/front-end/moment-with-locales.min.js",
                "~/Scripts/front-end/common/common.js",
                "~/Scripts/front-end/modelView/domainRegisterModel.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/font-awesome.css",
                "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/assets/front-end/css/base").Include(
                //"~/assets/front-end/css/bootstrap.min.css",
                //"~/assets/front-end/css/font-awesome.min.css",
                "~/assets/front-end/css/contents/common.css",
                "~/assets/front-end/css/contents/vps.css"));

            bundles.Add(new StyleBundle("~/styles/cloud-server-detail").Include(
                "~/assets/front-end/css/toastr.css",
                "~/Scripts/front-end/plugins/sweetalert2/sweetalert2.css",
                "~/assets/front-end/plugins/ion.Ranger/ion.rangeSlider.css",
                "~/assets/front-end/plugins/ion.Ranger/ion.rangeSlider.skin.css"));

            bundles.Add(new StyleBundle("~/styles/shoppingcart").Include(
                "~/assets/front-end/css/toastr.css",
                "~/Scripts/front-end/plugins/sweetalert2/sweetalert2.css"));

            bundles.Add(new StyleBundle("~/styles/shoppingcart-confirm").Include(
                "~/assets/front-end/plugins/tags-input/bootstrap-tagsinput.css",
                "~/assets/front-end/css/toastr.css",
                "~/Scripts/front-end/plugins/sweetalert2/sweetalert2.css"));

            bundles.Add(new StyleBundle("~/styles/shoppingcart-payment").Include(
                "~/assets/front-end/css/toastr.css",
                "~/Scripts/front-end/plugins/sweetalert2/sweetalert2.css"));


            bundles.Add(new ScriptBundle("~/scripts/start-cloud-detail").Include(
             "~/Scripts/front-end/knockout-3.4.2.debug.js",
             "~/Scripts/front-end/knockout.mapping-latest.debug.js",
             "~/Scripts/front-end/lodash.js",
             "~/Scripts/front-end/plugins/globinfo/globalize.js",
             "~/Scripts/front-end/plugins/globinfo/globalize.culture.vi-VN.js",
             "~/Scripts/front-end/plugins/input-mask/jquery.inputmask.js",
             "~/Scripts/front-end/plugins/input-mask/jquery.inputmask.numeric.extensions.js",
             "~/Scripts/front-end/toastr.js",
             "~/Scripts/front-end/plugins/sweetalert2/sweetalert2.js",
             "~/Scripts/front-end/common/common.js",
             "~/Scripts/common/common.js",
             "~/assets/front-end/plugins/ion.Ranger/ion.rangeSlider.min.js",
             "~/Scripts/front-end/modelView/startCloudModel.js"));



            bundles.Add(new ScriptBundle("~/scripts/trial-start-cloud-detail").Include(
                "~/Scripts/front-end/knockout-3.4.2.debug.js",
                "~/Scripts/front-end/knockout.mapping-latest.debug.js",
                "~/Scripts/front-end/lodash.js",
                "~/Scripts/front-end/plugins/globinfo/globalize.js",
                "~/Scripts/front-end/plugins/globinfo/globalize.culture.vi-VN.js",
                "~/Scripts/front-end/plugins/input-mask/jquery.inputmask.js",
                "~/Scripts/front-end/plugins/input-mask/jquery.inputmask.numeric.extensions.js",
                "~/Scripts/front-end/toastr.js",
                "~/Scripts/front-end/plugins/sweetalert2/sweetalert2.js",
                "~/Scripts/front-end/common/common.js",
                "~/assets/front-end/plugins/ion.Ranger/ion.rangeSlider.min.js",
                "~/Scripts/front-end/modelView/startCloudTrial.js"));


            bundles.Add(new ScriptBundle("~/scripts/trial-start-cloud-switch-to-real").Include(
             "~/Scripts/front-end/knockout-3.4.2.debug.js",
             "~/Scripts/front-end/knockout.mapping-latest.debug.js",
             "~/Scripts/front-end/lodash.js",
             "~/Scripts/front-end/plugins/globinfo/globalize.js",
             "~/Scripts/front-end/plugins/globinfo/globalize.culture.vi-VN.js",
             "~/Scripts/front-end/plugins/input-mask/jquery.inputmask.js",
             "~/Scripts/front-end/plugins/input-mask/jquery.inputmask.numeric.extensions.js",
             "~/Scripts/front-end/toastr.js",
             "~/Scripts/front-end/plugins/sweetalert2/sweetalert2.js",
             "~/Scripts/front-end/common/common.js",
             "~/assets/front-end/plugins/ion.Ranger/ion.rangeSlider.min.js",
             "~/Scripts/front-end/modelView/startCloudSwitchToReal.js"));
            #endregion
        }
    }
}