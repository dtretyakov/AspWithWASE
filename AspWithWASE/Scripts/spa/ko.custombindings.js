
require(["jquery", "knockout", "moment", "md5"], function ($, ko, moment, md5) {
    ko.bindingHandlers.dateString = {
        update: function (element, valueAccessor) {
            var valueUnwrapped = ko.utils.unwrapObservable(valueAccessor());
            var formattedTime = moment(valueUnwrapped).format("YYYY-MM-DD HH:mm");

            $(element).text(formattedTime);
        }
    };
    
    ko.bindingHandlers.gravatar = {
        update: function (element, valueAccessor) {
            var valueUnwrapped = ko.utils.unwrapObservable(valueAccessor());
            var src = "http://www.gravatar.com/avatar/" + md5(valueUnwrapped.trim().toLowerCase()) + ".jpg?s=40";

            $(element).attr("src", src);
        }
    };
});