(function () {
    appModule.controller('crm.vipManage.sendCouponModal', [
        "$scope", "$uibModalInstance", "$timeout", "$uibModal", "abp.services.app.coupon", 'vipInfo',
        function ($scope, $uibModalInstance, $timeout, $uibModal, couponService, vipInfo) {

            var vm = this;
            vm.saving = false;

            vm.vipId = vipInfo.vipId;
            vm.vipName = vipInfo.vipName;
            vm.vipPoint = vipInfo.vipPoint;
            vm.Explain = "后台处理";
            vm.couponConfigList = null;

            vm.save = function () {
                if (vm.sendCouponId == "") {
                    abp.notify.warn("请选择赠送优惠券！");
                    return;
                }
                vm.saving = true;

                vm.sendCouponName = null;
                for (var i = 0; i < vm.couponConfigList.length; i++) {
                    if (vm.couponConfigList[i].id == vm.sendCouponId) {
                        vm.sendCouponName = vm.couponConfigList[i].couponName;
                        break;  // 循环被终止
                    }
                }

                abp.message.confirm(
                    abp.utils.formatString("赠送优惠券\n\r\n\r会员昵称:{0}\n\r优惠券名称:{1}  ", vm.vipName, vm.sendCouponName),
                    function (isConfirmed) {
                        $timeout(function () {
                            if (isConfirmed) {
                                vm.saving = true;
                                couponService.sendCoupon({
                                    vipId: vm.vipId,
                                    couponConfigId: vm.sendCouponId,
                                    explain: vm.Explain
                                }).then(function () {
                                    abp.notify.success("操作成功");
                                    $uibModalInstance.close();
                                }).finally(function () {
                                    vm.saving = false;
                                });
                            } else {
                                vm.saving = false;
                            }
                        }, 200);
                    }
                );


            };

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };

            vm.getCouponConfigList = function () {
                couponService.getCouponConfigList()
                    .then(function (result) {
                        vm.couponConfigList = result.data;
                        vm.couponConfigList.unshift({ id: "", couponName: "请选择" });
                        vm.sendCouponId = vm.couponConfigList[0].id;
                    }).finally(function () {
                        vm.loading = false;
                    });
            }

            vm.getCouponConfigList();

        }
    ]);
})();