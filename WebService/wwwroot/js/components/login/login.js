define(['knockout'], (ko) => {
    return function (params) {

        let User = ko.observableArray(
            [{ language: "", name: "", username: "", age: "" }]);
        let responseMessage = ko.observable();
        let password = ko.observable();
        let userId = params.userId;
        let showContent = params.showContent;
        let name = ko.observable();
        let age = ko.observable('');
        let username = ko.observable();
        let language = ko.observable();



        let getUsers = function () {
            fetch("api/users/" + userId() + "/" + password())
                .then(function (response) {
                        if (response.status === 404 || response.status === 400) {
                            responseMessage("Invalid userId or Password! Please type  valid credentials!");
                            User("");
                            throw new Error(response.status + " Authentication failed ");
                        }
                        if (response.status === 200) {
                            showContent(true);
                            responseMessage("Login succesful!");
                        }
                        return response.json();
                    }
                )
                .then(function (data) {
                    User(data);
                });
        }

        let createUsers = function () {

            let headers = new Headers();
            headers.append("Content-Type", "application/json");
            fetch("api/users", {
                method: "POST", body: JSON.stringify({ age: +age(), name: name(), username: username(), language: language(), password: password()}), headers
            })
                .then(response => response.json()
                )
        }
        
        return {
         getUsers,userId, responseMessage, password, User, showContent, createUsers, name, age, username, language
        };
    } 
}); 