function NotifyModel() {

    var self = this;
    self.isLoading = ko.observable(false);

    // pagging
    self.currentPage = ko.observable(1);
    self.recordPerPage = ko.observable(20);
    self.totalPage = ko.observable(0);
    self.totalRecord = ko.observable(0);
    self.mode = ko.observable(null);
    self.type = ko.observable(null);
    self.isRead = ko.observable(false);
    self.items = ko.observableArray([]);

    self.isRead.subscribe(function () {
        self.search(1);
    });

    var firstMode = true;
    self.mode.subscribe(function () {
        if (firstMode) {
            firstMode = false;
            return;
        }
        self.search(1);
    });

    var firstType = true;
    self.type.subscribe(function () {
        if (firstType) {
            firstType = false;
            return;
        }
        self.search(1);
    });

    self.renderPager = function () {
        self.totalPage(Math.ceil(self.totalRecord() / self.recordPerPage()));

        $("#sumaryPagerPackage").html(self.totalRecord() === 0 ? getLanguageText('ScriptNoNotifyAny') : "Hiển thị " + ((self.currentPage() - 1) * self.recordPerPage() + 1) + " đến " + (self.currentPage() * self.recordPerPage() < self.totalRecord() ?
            self.currentPage() * self.recordPerPage() : self.totalRecord()) + " của " + self.totalRecord() + " notify");

        $("#pagerPackage").pager({ pagenumber: self.currentPage(), pagecount: self.totalPage(), totalrecords: self.totalRecord(), buttonClickCallback: self.pageClick });
    };

    self.pageClick = function (currentPage) {
        $("#pagerPackage").pager({ pagenumber: currentPage, pagecount: self.totalPage(), buttonClickCallback: self.search });

        self.search(currentPage);
    };

    self.search = function (currentPage) {
        self.currentPage(currentPage);
        self.isLoading(true);

        $.get("/notify/search",
            {
                mode: self.mode(),
                type: self.type(),
                isRead: self.isRead(),
                pageIndex: self.currentPage(),
                recordPerPage: self.recordPerPage()
            }, function (data) {
                self.isLoading(false);
                self.items(data.items);
                self.totalRecord(data.totalRecord);
                self.renderPager();
            });
    }

    self.search(1);
}

var modelView = new NotifyModel();

ko.applyBindings(modelView);