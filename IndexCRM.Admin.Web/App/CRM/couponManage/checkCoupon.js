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
                console.log(vm.couponConfig.startTime + "--" + vm.couponConfig.startTime);
                vm.couponConfig.couponImg = vm.couponImg;
                if (vm.couponConfig.couponImg == "") {
                    abp.notify.warn("请上传图片！");
                    return;
                }
                if (vm.couponConfig.validityMode == 1) {
                    if (vm.couponConfig.startTime == null || vm.couponConfig.endTime == null) {
                        abp.notify.warn("请填写有效期！");
                        return;
                    }
                    if (vm.couponConfig.startTime >= vm.couponConfig.endTime) {
                        abp.notify.warn("开始时间不能大于结束时间！");
                        return;
                    }
                }
                if (vm.couponConfig.validityMode == 2) {
                    if (vm.couponConfig.effectDate == "" && vm.couponConfig.validDate == "") {
                        abp.notify.warn("请填写有效期！");
                    }
                    if (vm.couponConfig.validDate <= 0) {
                        abp.notify.warn("有效期不能小于等于0！");
                        return;
                    }
                    if (vm.couponConfig.effectDate < 0) {
                        abp.notify.warn("生效日不能小于0！");
                        return;
                    }
                }
                if (vm.couponConfig.couponName == null || vm.couponConfig.couponPoint == null
                    || vm.couponConfig.couponNum == null || vm.couponConfig.couponExplain == null) {
                    abp.notify.warn("请完善信息！");
                    return;
                }

                vm.saving = true;
                couponService.createOrUpdateCoupon({
                    couponConfig: vm.couponConfig
                }).then(function () {
                    if (vm.couponConfigId == "") {
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
                    console.log(result);
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
                        vm.couponConfig.isShow = vm.couponConfig.isShow + "";
                        vm.couponImg = vm.couponConfig.couponImg;
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