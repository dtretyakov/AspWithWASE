
define("vm.issues", ["knockout", "toastr", "datacontext"],
    function(ko, toastr, datacontext) {
        return function() {
            var self = this;

            var issuesRepository = new datacontext("/api/issues");

            self.categories = ko.observableArray(["Bug", "Story", "Task"]);

            self.newIssue = {
                category: ko.observable(self.categories()[0]),
                email: ko.observable(""),
                description: ko.observable("")
            };

            self.issues = {
                category: ko.observable(self.categories()[0]),
                list: ko.observableArray([])
            };

            self.loadIssues = function() {
                self.loading = ko.observable(true);

                issuesRepository.get("?$filter=Category%20eq%20'" + self.issues.category() + "'")
                    .done(function(data) {
                        self.issues.list(data);
                    })
                    .fail(function() {
                        toastr.error("Failed to load list of issues.");
                    }).always(function() {
                        self.loading(false);
                    });
            };

            self.issues.category.subscribe(self.loadIssues);

            self.addIssue = function() {
                var issue = ko.toJS(self.newIssue);
                issuesRepository.post(issue)
                    .done(function(data) {
                        toastr.success(data.category + " #" + data.id + " has been added.");

                        if (data.category == self.issues.category()) {
                            self.issues.list.push(data);
                        }

                        self.newIssue.category(self.categories()[0]);
                        self.newIssue.email("");
                        self.newIssue.description("");
                    })
                    .fail(function() {
                        toastr.error("Failed to add new issue.");
                    });
            };

            self.deleteIssue = function(issue) {
                if (!confirm("Do you really want to delete issue #" + issue.id + "?")) {
                    return;
                }

                issuesRepository.delete(issue.id)
                    .done(function() {
                        toastr.success("Issue #" + issue.id + " has been succefully deleted.");
                        self.issues.list.remove(issue);
                    })
                    .fail(function() {
                        toastr.error("Failed to delete issue #" + issue.id);
                    });
            };

            self.loadIssues();
        };
    });