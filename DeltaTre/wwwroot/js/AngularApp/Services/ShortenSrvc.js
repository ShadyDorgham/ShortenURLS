app.service('sorternSrvc', function ($http) {
    this.saveUrl = function (originalUrl) {
        debugger;
        return $http({
            url: "/Home/SaveUrl/",
            method: "POST",
            params: { "OriginalUrl":  originalUrl}
        });
    }

    this.getValidUrls = function() {
        return $http({
            url: "/Home/GetValidUrls/",
            method: "GET"
        });
    }

});