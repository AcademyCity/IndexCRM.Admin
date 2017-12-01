(function () {
    appModule.controller('crm.vipManage.sendPoint', [
        '$scope', '$state', '$stateParams', '$timeout', '$uibModal', 'uiGridConstants', 'abp.services.app.point',
        function ($scope, $state, $stateParams, $timeout, $uibModal, uiGridConstants, pointService) {
            var vm = this;
            vm.loading = false;
            vm.amount = null;
            vm.vipCode = null;
            vm.Explain = "消费赠送";

            vm.requestParams = {
                permission: '',
                role: '',
                skipCount: 0,
                maxResultCount: app.consts.grid.defaultPageSize,
                sorting: null
            };

            vm.sendPointGridOptions = {
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
                        name: "会员编号",
                        field: 'vipCode',
                        minWidth: 100
                    },
                    {
                        name: "赠送数量",
                        field: 'pointChange',
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '  <span ng-show="row.entity.pointChange>0" class="label label-success">{{row.entity.pointChange}}</span>' +
                        '  <span ng-show="row.entity.pointChange<=0" class="label label-danger">{{row.entity.pointChange}}</span>' +
                        '</div>',
                        maxWidth: 100
                    },
                    {
                        name: "赠送原因",
                        field: 'pointExplain',
                        minWidth: 100
                    },
                    {
                        name: "赠送人",
                        field: 'addMan',
                        minWidth: 100
                    },
                    {
                        name: "赠送时间",
                        field: 'addTime',
                        cellFilter: 'momentFormat: \'YYYY-MM-DD HH:mm:ss\'',
                        minWidth: 100
                    }
                ],
                data: []
            };

            vm.getSendPointList = function () {
                document.getElementById("PointNum").focus();
                vm.couponCode = "";
                vm.loading = true;
                pointService.getSendPointRecordList($.extend({ filter: "" }, vm.requestParams))
                    .then(function (result) {
                        vm.sendPointGridOptions.totalItems = result.data.totalCount;
                        vm.sendPointGridOptions.data = result.data.items;
                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            vm.onEnter = function (e) {
                var keycode = window.event ? e.keyCode : e.which;
                if (keycode == 13) {
                    vm.sendPoint();
                }
            };

            vm.sendPoint = function () {

                if (vm.amount == null) {
                    abp.notify.warn("请填写积分数量！");
                    return;
                }
                else {
                    if (vm.amount <= 0) {
                        abp.notify.warn("积分数量不能小于等于零！");
                        return;
                    }
                }

                if (vm.vipCode == null) {
                    abp.notify.warn("请填写会员编号！");
                    return;
                }

                abp.message.confirm(
                    abp.utils.formatString("赠送积分\n\r\n\r{0}", vm.amount),
                    function (isConfirmed) {
                        $timeout(function () {
                            if (isConfirmed) {
                                vm.saving = true;
                                pointService.changePoint({
                                    vipId: vm.vipCode,
                                    amount: vm.amount,
                                    explain: vm.Explain
                                }).then(function () {
                                    abp.notify.success("操作成功");
                                }).finally(function () {
                                    vm.getSendPointList();
                                });
                            }
                        }, 200);
                    }
                );
            };

            vm.getSendPointList();

        }
    ]);
})();