"use strict";
app.controller("ShortenCtrl",
    ["$scope", "sorternSrvc", function($scope, sorternSrvc) {


        $scope.OriginalUrl = "";
        $scope.ValidUrlsList = [];


        $scope.textBox = {
            url: {
                bindingOptions: {
                    value: "OriginalUrl"
                },
                placeholder: "Enter your Link here"
            }
        };


        $scope.saveUrl = function() {
            debugger;
            $scope.OriginalUrl.trim();

            if ($scope.OriginalUrl === '' || $scope.OriginalUrl === null) {
                DevExpress.ui.notify("Sorry Url cannot be empty", "error", 10000);
                return;
            }

            sorternSrvc.saveUrl($scope.OriginalUrl).then(function(response) {
                DevExpress.ui.notify(response.data, "success", 10000);
                $scope.getValidUrls();
                $scope.OriginalUrl = "";
            }).catch(function(response) {
                DevExpress.ui.notify(response.data, "error", 10000);
            });
        };


        $scope.getValidUrls = function() {
            sorternSrvc.getValidUrls().then(function(data) {
                $scope.ValidUrlsList = data.data;
                debugger;
            });
        };
        $scope.getValidUrls();


        $scope.dataGridValidUrls = {
            bindingOptions: {
                dataSource: "ValidUrlsList"
            },
            columns: [{ dataField: "Id", visible: false }, { dataField: "OriginalUrl" },
                { dataField: "ShortenUrl", width: 150 }, { dataField: 'InsertDate', dataType: 'date', format: 'dd/MM/yyyy' , width: 150}],
            showBorders: true,
            rowAlternationEnabled: true,
            showRowLines: true,
            showColumnLines: true
        };
    }]);