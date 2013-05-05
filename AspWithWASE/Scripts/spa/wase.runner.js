
requirejs.config({
    paths: {
        jquery: "scripts/jquery-2.0.0",
        knockout: "scripts/knockout-2.2.1",
        toastr: "scripts/toastr",
        md5: "scripts/md5",
        moment: "scripts/moment"
    },
});

require(["jquery", "knockout", "vm.issues"], function($, ko, issues) {
    $(function() {
        ko.applyBindings(issues());
    });
});