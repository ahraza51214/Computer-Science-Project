define(['knockout'], (ko) => {
    let currentComponent = ko.observable('login');
    let menuElements = ["Login", "EditUser", "Bookmark", "Rating", "SearchHistory"];
    let showContent = ko.observable(false);
    let userId = ko.observable(0);
   

    let changeComponent = element => {
        currentComponent(element.toLowerCase());
    }

    let isActive = element => {
        return element.toLowerCase() === currentComponent() ? "active" : "";
    }
    
    return {
        currentComponent,
        menuElements,
        showContent,
        
        changeComponent,
        isActive,  userId
    };
});