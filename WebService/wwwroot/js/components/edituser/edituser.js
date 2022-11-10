define(['knockout'], function (ko) {
    return function (params) {
        
        let userId = params.userId
        let name = ko.observable();
        let age = ko.observable('');
        let username = ko.observable();
        let language = ko.observable();
        let password = ko.observable();
        
        let deleteUser = url => fetch("api/users/" + userId(), { method: "DELETE" });

        let updateUsers = function () {
            let headers = new Headers();
            headers.append("Content-Type", "application/json");
            fetch("api/users/" + userId(), {
                method: "PUT", body: JSON.stringify({ age: +age(), name: name(), username: username(), language: language(), password: password()}), headers
            })
                .then(response => response.json())
        }
        
        return {
            age,
            language,
            username,
            name,
            userId,
            deleteUser,
            updateUsers,
            password,
        };
    }
});