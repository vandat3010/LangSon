function getListBackupTimes() {
    var period = 15;
    var time = moment(new Date(1970, 0, 1, 0, 0, 0, 0));
    var listTimes = [];
    for (var i = 0; i < 24 * (60 / period); i++) {
        listTimes.push({
            key: parseInt(time.format("HHmm")),
            value: time.format("HH:mm")
        });
        time.add(period, 'm');
    }
    return listTimes;
}