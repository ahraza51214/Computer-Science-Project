define(['knockout'], function (ko) {
    return function (params) {

        resultSearch = ko.observableArray(
            [{ search: "PrimaryTitles", count: "" }]
        );

        let SearchInput = ko.observable();
        let page = ko.observable(0);
        let pageSize = ko.observable(50);
        let responseMessage = ko.observable();
        let userId = ko.observable(0); 
   

        next = function () {
            page(page() + 1);
        }

        prev = function () {
            if (page() < 1) {
                throw new Error("Page cannot be negative");
            }
            page(page() - 1);
        }

        let createSearch = function () {

            let headers = new Headers();
            headers.append("Content-Type", "application/json");
            fetch("api/search/" + userId() + "?page=" + page() + "&pageSize=" + pageSize(), {
                method: "POST", body: JSON.stringify({ userid: +userId(), searchInput: SearchInput() }), headers
            })
                .then(function (response) {
                    if (response.status === 404) {
                        responseMessage(" No search matches found!");
                        resultSearch("");
                        throw new Error(response.status + " No search matches found ");
                    }
                    responseMessage("");
                    return response.json();
                })
                .then(function (data) {
                    resultSearch(data);
                });
        }
        
        return {
            SearchInput, createSearch, page, pageSize, next, prev, responseMessage, userId
        };
    }
});