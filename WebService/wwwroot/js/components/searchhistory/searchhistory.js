define(['knockout'], function (ko) {
    return function (params) {

        let userId = params.userId;

        let mysearch = ko.observableArray(
            [{ storedInput: "", dateTime: "" }]);

        let getSearch = function () {
            fetch("api/search/" + userId())
                .then(function (response) {
                    if (response.status === 404) {
                        mysearch(""); //hides the input and datetime if status code is 404
                    }
                    return response.json();
                })
                .then(function (data) {
                    mysearch(data);
                });
        }
        
        return {
            getSearch,
            userId,
            mysearch
        };
    }
});