(function () {
    appModule.controller('crm.couponManage.checkCoupon', [
        '$scope', '$state', '$stateParams', '$timeout', '$uibModal', 'uiGridConstants', 'abp.services.app.coupon',
        function ($scope, $state, $stateParams, $timeout, $uibModal, uiGridConstants, couponService) {
            var vm = this;
            vm.loading = false;
            vm.couponCode = "";

            vm.requestParams = {
                permission: '',
                role: '',
                skipCount: 0,
                maxResultCount: app.consts.grid.defaultPageSize,
                sorting: null
            };

            vm.checkCouponGridOptions = {
                enableHorizontalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                enableVerticalScrollbar: uiGridConstants.scrollbars.WHEN_NEEDED,
                paginationPageSizes: app.consts.grid.defaultPageSizes,
                paginationPageSize: app.consts.grid.defaultPageSize,
                useExternalPagination: true,
                useExternalSorting: true,
                enableSorting: false,
                appScopeProvider: vm,
                rowTemplate: '<div ng-repeat="(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name" class="ui-grid-cell" ng-class="{ \'ui-grid-row-header-cell\': col.isRowHeader, \'text-muted\': !row.entity.isActive }"  ui-grid-cell></div>',
                columnDefs: [
                    {
                        name: "券号",
                        field: 'couponCode',
                        minWidth: 80
                    },
                    {
                        name: "券名",
                        field: 'couponName',
                        minWidth: 140
                    },
                    {
                        name: "有效期",
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '  <span>{{row.entity.startTime.substring(0, 10)}} 至 {{row.entity.endTime.substring(0, 10)}}</span>' +
                        '</div>',
                        minWidth: 200
                    },
                    {
                        name: "是否使用",
                        field: 'isUse',
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '  <span ng-show="row.entity.isUse" class="label label-danger">已使用</span>' +
                        '  <span ng-show="!row.entity.isUse" class="label label-success">未使用</span>' +
                        '</div>',
                        minWidth: 40
                    },
                    {
                        name: "核销人",
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '  <span ng-show="row.entity.isUse">{{row.entity.modifyMan}}</span>' +
                        '</div>',
                        minWidth: 40
                    },
                    {
                        name: "核销时间",
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '  <span ng-show="row.entity.isUse">{{row.entity.modifyTime.substring(0, 19).replace("T"," ")}}</span>' +
                        '</div>',
                        minWidth: 160
                    }
                ],
                data: []
            };

            vm.getCheckCouponList = function () {
                document.getElementById("CouponCode").focus();
                vm.couponCode = "";
                vm.loading = true;
                couponService.getCheckCouponList($.extend({ filter: "" }, vm.requestParams))
                    .then(function (result) {
                        vm.checkCouponGridOptions.totalItems = result.data.totalCount;
                        vm.checkCouponGridOptions.data = result.data.items;
                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            vm.onEnter = function (e) {
                var keycode = window.event ? e.keyCode : e.which;
                if (keycode == 13) {
                    vm.checkCoupon();
                }
            };

            vm.checkCoupon = function () {

                if (vm.couponCode == "") {
                    abp.notify.warn("请填写券号！");
                    return;
                }

                couponService.getCheckCouponName({
                    CouponCode: vm.couponCode
                }).then(function (result) {
                    abp.message.confirm(
                        abp.utils.formatString("核销优惠券\n\r\n\r{0}", result.data),
                        function (isConfirmed) {
                            $timeout(function () {
                                if (isConfirmed) {
                                    vm.saving = true;
                                    couponService.checkCoupon({
                                        CouponCode: vm.couponCode
                                    }).then(function () {
                                        abp.notify.success("操作成功");
                                        vm.couponCode = "";
                                    }).finally(function () {
                                        vm.getCheckCouponList();
                                    });
                                }
                            }, 200);
                        }
                    );
                });


            };

            vm.getCheckCouponList();

        }
    ]);
})();