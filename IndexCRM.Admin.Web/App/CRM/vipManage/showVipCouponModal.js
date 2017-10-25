(function () {
    appModule.controller('crm.vipManage.showVipCouponModal', [
        "$scope", "$uibModalInstance", "$timeout", "$uibModal", "uiGridConstants", "abp.services.app.coupon", 'vipInfo',
        function ($scope, $uibModalInstance, $timeout, $uibModal, uiGridConstants, couponService, vipInfo) {

            var vm = this;
            vm.saving = false;
            vm.loading = false;

            vm.vipId = vipInfo.vipId;
            vm.vipName = vipInfo.vipName;

            vm.vipPointRecordGridOptions = {
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

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };

            vm.getVipPointRecordList = function () {
                vm.loading = true;
                couponService.getVipCouponList({
                    vipId: vm.vipId
                    }).then(function (result) {
                        vm.vipPointRecordGridOptions.totalItems = result.data.totalCount;
                        vm.vipPointRecordGridOptions.data = result.data.items;
                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            vm.getVipPointRecordList();
        }
    ]);
})();