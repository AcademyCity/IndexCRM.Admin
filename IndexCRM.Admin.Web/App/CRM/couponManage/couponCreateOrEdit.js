(function () {
    appModule.controller('crm.couponManage.couponCreateOrEdit', [
        '$scope', '$state', '$stateParams', '$uibModal', 'abp.services.app.coupon',
        function ($scope, $state, $stateParams, $uibModal, couponService) {
            var vm = this;
            vm.loading = false;

            vm.user = null;
            vm.couponConfigId = $stateParams.couponConfigId;

            vm.save = function () {

                vm.saving = true;
                userService.createOrUpdateUser({
                    user: vm.user
                }).then(function () {
                    abp.notify.info(app.localize('SavedSuccessfully'));

                }).finally(function () {
                    vm.saving = false;
                });
            };

            vm.cancel = function () {

            };

            function init() {
                console.log("couponConfigId:" + vm.couponConfigId);
                couponService.getCouponConfigForEdit({
                    CouponConfigId: vm.couponConfigId
                }).then(function (result) {
                    console.log(result.data);
                    if (result.data != null) {
                        vm.couponConfig = result;

                    }
                });
            }

            init();

            $('#config-demo').daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                autoUpdateInput: false,
                timePicker24Hour: true,
                timePicker: true,
                "locale": {
                    format: 'YYYY-MM-DD HH:mm',
                    applyLabel: "应用",
                    cancelLabel: "取消",
                    resetLabel: "重置",
                }
            },
                function (start, end, label) {
                    beginTimeTake = start;
                    if (!this.startDate) {
                        this.element.val('');
                    } else {
                        this.element.val(this.startDate.format(this.locale.format));
                    }
                });


        }
    ]);
})();