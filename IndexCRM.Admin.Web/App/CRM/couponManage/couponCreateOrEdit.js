(function () {
    appModule.controller('crm.couponManage.couponCreateOrEdit', [
        '$scope', '$state', '$stateParams', '$uibModal', 'abp.services.app.coupon',
        function ($scope, $state, $stateParams, $uibModal, couponService) {
            var vm = this;
            vm.loading = false;


            vm.couponConfig = null;
            vm.couponConfigId = $stateParams.couponConfigId;
            vm.couponImg = "";

            vm.save = function () {
                vm.couponConfig.couponImg = vm.couponImg;
                if (vm.couponConfig.couponImg == "") {
                    abp.notify.warn("请上传图片！");
                }
                if (vm.couponConfig.validityMode == 1) {
                    if (vm.couponConfig.startTime != "" && vm.couponConfig.endTime != "") {
                        abp.notify.warn("请填写有效期！");
                    }
                }
                if (vm.couponConfig.validityMode == 2) {
                    if (vm.couponConfig.effectDate != "" && vm.couponConfig.validDate != "") {
                        abp.notify.warn("请填写有效期！");
                    }
                }

                //vm.saving = true;
                //couponService.createOrUpdateUser({
                //    user: vm.user
                //}).then(function () {
                //    abp.notify.info(app.localize('SavedSuccessfully'));
                //vm.back();
                //}).finally(function () {
                //    vm.saving = false;
                //});
            };

            vm.back = function () {
                $state.go('couponManage', {

                });
            };

            vm.changePicture = function () {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/CRM/couponManage/changePicture.cshtml',
                    controller: 'crm.couponManage.changePicture as vm',
                    backdrop: 'static',
                    resolve: {
                        couponImg: function () {
                            return vm.couponImg;
                        }
                    }
                });

                modalInstance.result.then(function (result) {
                    vm.couponImg = result;
                });
            };

            function init() {
                couponService.getCouponConfigForEdit({
                    CouponConfigId: vm.couponConfigId
                }).then(function (result) {
                    vm.couponConfig = result.data;
                    if (result.data.id != null) {
                        vm.couponConfig.startTime = vm.couponConfig.startTime.replace("T", " ").substr(0, 16);
                        vm.couponConfig.endTime = vm.couponConfig.endTime.replace("T", " ").substr(0, 16);
                    }
                    else {
                        vm.couponConfig.validityMode = "1";
                        vm.couponConfig.isShow = "true";
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