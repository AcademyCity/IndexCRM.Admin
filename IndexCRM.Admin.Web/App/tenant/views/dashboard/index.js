(function () {
    appModule.controller('tenant.views.dashboard.index', [
        '$scope', 'abp.services.app.tenantDashboard',
        function ($scope, tenantDashboardService) {
            var vm = this;
            
            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

        }
    ]);
})();