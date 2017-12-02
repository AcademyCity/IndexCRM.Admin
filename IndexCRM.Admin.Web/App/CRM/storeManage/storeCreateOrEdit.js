(function () {
    appModule.controller('crm.storeManage.storeCreateOrEdit', [
        '$scope', '$state', '$stateParams', '$uibModal', 'abp.services.app.store',
        function ($scope, $state, $stateParams, $uibModal, storeService) {
            var vm = this;
            vm.loading = false;


            vm.store = null;
            vm.storeId = $stateParams.storeId;

            vm.save = function () {
                vm.saving = true;
                storeService.createOrUpdateStore({
                    store: vm.store
                }).then(function () {
                    if (vm.storeId == "") {
                        abp.notify.info("创建成功！");
                    }
                    else {
                        abp.notify.info("修改成功！");    
                    }
                    vm.back();
                }).finally(function () {
                    vm.saving = false;
                });
            };

            vm.back = function () {
                $state.go('storeManage', {

                });
            };

            function init() {
                storeService.getStoreForEdit({
                    StoreId: vm.storeId
                }).then(function (result) {
                    vm.store = result.data;
                    if (result.data.id != null) {
                        vm.store.isShow = vm.store.isShow + "";
                    }
                    else {
                        vm.store.isShow = "true";
                    }
                });
            }

            init();
        }
    ]);
})();