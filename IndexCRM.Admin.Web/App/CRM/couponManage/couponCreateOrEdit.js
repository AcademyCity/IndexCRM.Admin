(function () {
    appModule.controller('crm.couponManage.couponCreateOrEdit', [
        '$scope', '$state', '$stateParams', '$uibModal', 'abp.services.app.coupon',
        function ($scope, $state, $stateParams, $uibModal, couponService) {
            var vm = this;
            vm.loading = false;


            vm.couponConfig = null;
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

            vm.changePicture = function () {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/CRM/couponManage/changePicture.cshtml',
                    controller: 'crm.couponManage.changePicture as vm',
                    backdrop: 'static'
                });

                modalInstance.result.then(function (result) {
                    console.log(result);
                });
            };

            function init() {
                console.log("couponConfigId:" + vm.couponConfigId);
                couponService.getCouponConfigForEdit({
                    CouponConfigId: vm.couponConfigId
                }).then(function (result) {
                    if (result.data != null) {
                        vm.couponConfig = result.data;
                        vm.couponConfig.startTime = vm.couponConfig.startTime.replace("T", " ").substr(0, 16);
                        vm.couponConfig.endTime = vm.couponConfig.endTime.replace("T", " ").substr(0, 16);
                        vm.couponConfig.couponExplain = "1.shdksaj\r\n2.sadas";
                    }
                });
            }

            init();

            $("#StartTime").daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                autoUpdateInput: true,
                timePicker24Hour: true,
                timePicker: true,
                minDate: new Date().getFullYear() + '-01-01',
                maxDate: new Date().getFullYear() + 20 + '-12-31',
                "locale": {
                    format: 'YYYY-MM-DD HH:mm',
                    applyLabel: "应用",
                    cancelLabel: "取消",
                }
            }, function (start, end, label) {
                beginTimeTake = start;
                if (!this.startDate) {
                    this.element.val('');
                } else {
                    this.element.val(this.startDate.format(this.locale.format));
                }
            });

            $("#EndTime").daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                autoUpdateInput: true,
                timePicker24Hour: true,
                timePicker: true,
                minDate: new Date().getFullYear() + '-01-01',
                maxDate: new Date().getFullYear() + 20 + '-12-31',
                "locale": {
                    format: 'YYYY-MM-DD HH:mm',
                    applyLabel: "应用",
                    cancelLabel: "取消",
                }
            }, function (start, end, label) {
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