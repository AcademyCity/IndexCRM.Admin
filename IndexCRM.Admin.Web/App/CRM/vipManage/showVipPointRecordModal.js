(function () {
    appModule.controller('crm.vipManage.showVipPointRecordModal', [
        "$scope", "$uibModalInstance", "$timeout", "$uibModal","uiGridConstants", "abp.services.app.point", 'vipInfo',
        function ($scope, $uibModalInstance, $timeout, $uibModal,uiGridConstants, pointService, vipInfo) {

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
                        name: "变化数量",
                        field: 'pointChange',
                        cellTemplate:
                            '<div class=\"ui-grid-cell-contents\">' +
                            '  <span ng-show="row.entity.pointChange>0" class="label label-success">{{row.entity.pointChange}}</span>' +
                            '  <span ng-show="row.entity.pointChange<=0" class="label label-danger">{{row.entity.pointChange}}</span>' +
                            '</div>',
                        maxWidth: 100
                    },
                    {
                        name: "变化原因",
                        field: 'pointExplain',
                        minWidth: 200
                    },
                    {
                        name: app.localize('CreationTime'),
                        field: 'addTime',
                        cellFilter: 'momentFormat: \'YYYY-MM-DD HH:mm:ss\'',
                        minWidth: 100
                    },
                    {
                        name: "交易单号",
                        field: 'posNo',
                        minWidth: 100
                    }
                ],
                data: []
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };

            vm.getVipPointRecordList = function () {
                vm.loading = true;
                pointService.getVipPointRecordList({
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