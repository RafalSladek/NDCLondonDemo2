tt.app.controller("StartController", startController);

function startController($scope, $http) {
    var baseUrl = "/api/articles";
    
    $scope.totalItems = 0;
    $scope.maxSize = 10;

    $scope.setPage = function (pageNo) {
        $scope.currentPage = pageNo;
        $scope.loadList();
    };

    $scope.loadList = function () {
        $http({ method: "GET", url: "/api/articles?" + "$top=" + $scope.maxSize + "&$skip=" + $scope.currentPage })
        .success(function (data) {
            $scope.articles = data;
        });
    };

    $scope.loadDetails = function (article) {
        $http({ method: "GET", url: baseUrl + "/" + article.Id })
            .success(function (data) {
                $scope.currentArticle = data;
            });
    };

    $scope.addNewArticle = function () {
        $scope.currentArticle = {};
    };

    $scope.saveArticle = function () {
        $http({ method: "POST", url: baseUrl, data: $scope.currentArticle })
            .success(function (data) {
                $scope.currentArticle = null;
                $scope.loadList();
            });
    };

    $scope.deleteArticle = function (article) {
        $http({ method: "DELETE", url: baseUrl + "/" + article.Id })
            .success(function (data) {
                $scope.loadList();
            });
    };

    $scope.setPage(0);
    $scope.loadList();
}