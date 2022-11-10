define(['knockout'], function (ko) {
    //private part
    return function (params) {

        let bookmarks = ko.observableArray(
            [{ nConst: "", userId: "" }]);

        let nconst = ko.observable();
        let userId = params.userId;
        let responseMessage = ko.observable();

        let deleteBookmark = url => fetch("api/bookmark/" + userId() + "/" + nconst(), { method: "DELETE" });

        let createBookmark = function () {

            let headers = new Headers();
            headers.append("Content-Type", "application/json");
            fetch("api/bookmark", {
                method: "POST", body: JSON.stringify({ nConst: nconst(), userId: +userId() }), headers
            })
                .then(response => response.json())
        }
        
        let getBookmarks = function () {
            fetch("api/bookmark/" + userId())


                .then(function (response) {
                    if (response.status === 404 || response.status === 400) {
                        responseMessage("Invalid request! Please type a valid userId!");
                        bookmarks("");
                        throw new Error(response.status + " Bookmark Not found ");
                    }
                    if (response.status === 200) {
                        responseMessage("Bookmark retrieved succesfully!");
                    }
                    return response.json();
                }
                )
                .then(function (data) {
                    bookmarks(data);
                });
        }

        return {
            nconst,
            userId,
            bookmarks, 
            getBookmarks, 
            responseMessage,
            deleteBookmark, 
            createBookmark
        };
    }
});