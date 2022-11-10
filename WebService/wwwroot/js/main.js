require.config({
    baseUrl: "js",
    paths: {
        knockout: "lib/knockout/knockout-latest.debug",
        text: "lib/require-text/text.min",
        jquery: "lib/jquery/jquery.min",
        bootstrap: "../css/lib/twitter-bootstrap/js/bootstrap.bundle.min",
    },
    shim: {
        bootstrap: ['jquery']
    }
});

require(['knockout', 'text'], (ko) => {
    ko.components.register("login", {
        viewModel: { require: "components/login/login" },
        template: { require: "text!components/login/login.html" }
    });
});

require(['knockout', 'text'], (ko) => {
    ko.components.register("edituser", {
        viewModel: { require: "components/edituser/edituser" },
        template: { require: "text!components/edituser/edituser.html" }
    });
});

require(['knockout', 'text'], (ko) => {
    ko.components.register("searchhistory", {
        viewModel: { require: "components/searchhistory/searchhistory" },
        template: { require: "text!components/searchhistory/searchhistory.html" }
    });
});

require(['knockout', 'text'], (ko) => {
    ko.components.register("search", {
        viewModel: { require: "components/search/search" },
        template: { require: "text!components/search/search.html" }
    });
});

require(['knockout', 'text'], (ko) => {
    ko.components.register("bookmark", {
        viewModel: { require: "components/bookmark/bookmark" },
        template: { require: "text!components/bookmark/bookmark.html" }
    });
});

require(['knockout', 'text'], (ko) => {
    ko.components.register("rating", {
        viewModel: { require: "components/rating/rating" },
        template: { require: "text!components/rating/rating.html" }
    });
});

require(['knockout', 'FrontPageviewModel', 'bootstrap'], function (ko, fpvm) {
    ko.applyBindings(fpvm);
});