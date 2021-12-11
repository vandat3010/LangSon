var bootstrapApp = function () {
    //iCheckIt();
    //select2fy();
    datepickerify();

    //triggerCancelButton();

    //slimScroll();
    //Enable sidebar dinamic menu
    //dynamicMenu();
}

//store in viewcontentloaded event so we can perform callbacks from angular
window['viewContentLoaded'] = bootstrapApp;
$(document).ready(function () {
    bootstrapApp();
});

/* 
     * dynamic tree activate menu
     */
var dynamicMenu = function () {
    var url = window.location;
    // Will only work if string in href matches with location
    //$('.treeview-menu li a[href="' + url + '"]').addClass('active');
    // Will also work for relative and absolute hrefs
   
    $('.treeview-menu li a').filter(function () {
        return this.href == url;
    }).parent().parent().parent().addClass('active menu-open');

    $('.treeview-menu li a').filter(function () {
        return this.href == url;
    }).parent().addClass('active');

};
var datepickerify = function () {
    $("input[datepicker]")
        .each(function () {
            //var defaultValue = $(this).val();
            $(this)
                .datepicker({
                    autoclose: true,
                    forceParse: false,
                    todayHighlight: true,
                    format: 'dd/mm/yyyy',
                    language: 'vi'
                });

            //$(this).datepicker("setDate", new Date(defaultValue));
        });

}

var triggerCancelButton = function () {
    $("button[ui-sref]").one("click", function () {
        var defaultValue = $(this).attr('ui-sref');
        if (defaultValue) {
            var arr = defaultValue.split('.');
            if (arr.length > 1)
                location.href = '/' + arr[1];
            else location.href = '/';
        }
    });

}
var iCheckIt = function () {
    jQuery("input.icheck").iCheck({
        checkboxClass: 'icheckbox_square-blue',
        radioClass: 'iradio_square-blue',
        increaseArea: '20%' // optional
    });
}
var select2 = function (selector) {
    selector.select2({
        escapeMarkup: function (markup) { return markup; },
        templateResult: function (data) {
            if (data.loading) {
                return data.text;
            }
            var markup = "";
            if (data.children) {
                markup = "<div class='select2-treeview'><div class='select2-treeview-triangle select2-treeview-down'></div><span>" + data.text + "</span></div>";
            } else {
                markup = "<div class='select2-treeview-item'><span>" + data.text + "</span></div>";
            }
            return markup;
        },
        templateSelection: function (data) {
            return data.text;
        },
        /*
        line:5335
        https://github.com/maliming/select2-treeview/blob/master/select2.js#L5335
        
        if (self.isOpen()) {
          self.options.options.queryComplete(self, params.term);
        }
        */
        queryComplete: function (select2, term) {
            
            //Register the parent element click event
            select2.$results.children().click(function () {
                
                //Triangle Transform position
                var triangle = $(this).find(".select2-treeview-triangle");
                if (triangle.hasClass("select2-treeview-down")) {
                    triangle.removeClass("select2-treeview-down").addClass("select2-treeview-right");
                } else {
                    triangle.removeClass("select2-treeview-right").addClass("select2-treeview-down");
                }
                
                //Toggle child elements are hidden or displayed
                $(this).children("ul").toggle();

            }).click();// Shrink all groups

            var highlighted = select2.$results.find('.select2-results__option--highlighted');
            
            //Expand the grouping of the selected columns
            highlighted.parent().show();
            
            //Toggle the triangles of the selected section
            var triangle = highlighted.parent().parent().find(".select2-treeview-triangle");
            triangle.removeClass("select2-treeview-right").addClass("select2-treeview-down");
            
            //The scroll bar position
            // 35 = $(".select2-search--dropdown").outerHeight()
            // 29 = (".select2-results__option--highlighted").outerHeight()
            select2.$results.scrollTop(highlighted[0].offsetTop - 35 - 29);
        }
    });
}
//select2tree
var select2tree = function (selectorId, selectedArray) {
    $("#" + selectorId).select2tree();
    if (selectedArray)
        $("#" + selectorId).val(selectedArray).trigger('change');
}
var select2treefy = function () {
    $("select.select2tree").each(function () {       
        select2tree($(this).attr('id'));
    });
}

var select2fy = function () {
    $("select.select2").each(function () {
        //if ($(this).data("select2fy"))
        //    return;
        //$(this).select2();
        select2($(this));
        //$(this).data("select2fy", true);
    });
}
var select2TriggerChange = function (element) {
    $(element).trigger('change');
}
var select2fyWithAutoComplete = function (type, element, valueName, textName, withDefault, onchange) {
    if (!type)
        return;
    if (element.data("select2fy"))
        return;
    element.select2({
        ajax: {
            url: "/autocomplete/" + type + "/get",
            delay: 250,
            data: function (params) {
                return {
                    search: params.term, // search term
                    count: 5
                };
            },
            processResults: function (data, params) {
                //the result is returned in camel case to convert type to equivalent camel case string
                type = type[0].toUpperCase() + type.substring(1);
                if (data.success) {
                    var results = data.responseData.AutoComplete[type] || [];

                    var items = results.map(function (i) {
                        return {
                            id: i[valueName],
                            text: i[textName]
                        };
                    });
                    if (withDefault && items.length == 0)
                        items.push({
                            id: Math.ceil((-50000000) * Math.random()), //negative because select2 won't select id with 0
                            text: params.term
                        });
                    return {
                        results: items
                    };
                }
                return { results: [] };
            }
        },
        placeholder: getLanguageText('ScriptEnterTheName') + type,
        cache: false,
        escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
        minimumInputLength: 1
    }).data("select2fy", true);

    if (onchange)
        element.on('select2:select', function (evt) {
            onchange(evt);
        });
}

var stylizeBrowserPrompts = function () {
    //we replace existing browser prompts with our custom ones
    //window.mobConfirm = function (message, callback) {
    //    bootbox.confirm(message, callback);
    //}
}();

var slimScroll = function () {
    jQuery(".slim-scroll")
        .each(function () {
            jQuery(this).slimScroll();
        });
}