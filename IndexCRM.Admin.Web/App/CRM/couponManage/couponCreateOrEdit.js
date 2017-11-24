(function () {
    appModule.controller('crm.couponManage.couponCreateOrEdit', [
        '$scope', '$state', '$stateParams', '$uibModal', 'abp.services.app.language', 'uiGridConstants',
        function ($scope, $state, $stateParams, $uibModal, languageService, uiGridConstants) {
            var vm = this;
            vm.loading = false;

        }
    ]);
})();