(function () {

    appModule.controller('crm.couponManage.index', [
        '$scope', '$state', '$uibModal', '$stateParams', "$timeout", 'uiGridConstants', 'abp.services.app.coupon',
        function ($scope, $state, $uibModal, $stateParams, $timeout, uiGridConstants, couponService) {
            var vm = this;

            $scope.$on('$viewContentLoaded', function () {
                App.initAjax();
            });

            vm.loading = false;
            vm.advancedFiltersAreShown = false;
            vm.filterText = $stateParams.filterText || '';
            vm.currentUserId = abp.session.userId;

            vm.permissions = {

            };

            vm.requestParams = {
                permission: '',
                role: '',
                skipCount: 0,
                maxResultCount: app.consts.grid.defaultPageSize,
                sorting: null
            };

            vm.couponConfigGridOptions = {
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
                        name: app.localize('Actions'),
                        enableSorting: false,
                        width: 120,
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '  <div class="btn-group dropdown" uib-dropdown="" dropdown-append-to-body>' +
                        '    <button class="btn btn-xs btn-primary blue" uib-dropdown-toggle="" aria-haspopup="true" aria-expanded="false"><i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span></button>' +
                        '    <ul uib-dropdown-menu>' +
                        '      <li ><a ng-click="grid.appScope.editCouponConfig(row.entity)">修改优惠券</a></li>' +
                        '      <li ><a ng-click="grid.appScope.deleteCouponConfig(row.entity)">删除优惠券</a></li>' +
                        '    </ul>' +
                        '  </div>' +
                        '</div>'
                    },
                    {
                        name: '优惠券名称',
                        field: 'couponName',
                        minWidth: 140
                    },
                    {
                        name: '优惠券数量',
                        field: 'couponNum',
                        minWidth: 100
                    },
                    {
                        name: '已发送数量',
                        field: 'sendCouponNum',
                        minWidth: 100
                    },
                    {
                        name: '兑换积分',
                        field: 'couponPoint',
                        minWidth: 100
                    },
                    {
                        name: '有效期模式',
                        field: 'validityMode',
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '  <span ng-show="row.entity.validityMode==1" class="label label-success">定时</span>' +
                        '  <span ng-show="row.entity.validityMode==2" class="label label-warning">时长</span>' +
                        '</div>',
                        maxWidth: 100
                    },
                    {
                        name: '开始时间/生效日',
                        field: 'startTime',
                        cellFilter: 'momentFormat: \'YYYY-MM-DD HH:mm\'',
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '  <span ng-show="row.entity.validityMode==1">{{COL_FIELD CUSTOM_FILTERS}}</span>' +
                        '  <span ng-show="row.entity.validityMode==2">{{row.entity.effectDate}}</span>' +
                        '</div>',
                        minWidth: 100
                    },
                    {
                        name: '结束时间/有效时长',
                        field: 'endTime',
                        cellFilter: 'momentFormat: \'YYYY-MM-DD HH:mm\'',
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '  <span ng-show="row.entity.validityMode==1">{{COL_FIELD CUSTOM_FILTERS}}</span>' +
                        '  <span ng-show="row.entity.validityMode==2">{{row.entity.validDate}}</span>' +
                        '</div>',
                        minWidth: 100
                    },

                    {
                        name: '商城排序',
                        field: 'sort',
                        maxWidth: 100
                    },
                    {
                        name: '创建人',
                        field: 'addMan',
                        minWidth: 100
                    },
                    {
                        name: app.localize('CreationTime'),
                        field: 'addTime',
                        cellFilter: 'momentFormat: \'YYYY-MM-DD HH:mm:ss\'',
                        minWidth: 100
                    }
                ],
                data: []
            };

            vm.getCouponConfigList = function () {
                vm.loading = true;
                couponService.getCouponConfigList($.extend({ filter: vm.filterText }, vm.requestParams))
                    .then(function (result) {
                        vm.couponConfigGridOptions.totalItems = result.data.totalCount;
                        vm.couponConfigGridOptions.data = result.data.items;
                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            vm.editCouponConfig = function (couponConfig) {
                $state.go('createCoupon', {
                    couponConfigId: couponConfig.id
                });
            };

            vm.deleteCouponConfig = function (couponConfig) {
                abp.message.confirm(
                    abp.utils.formatString("删除优惠券\n\r\n\r优惠券名称:{0}", couponConfig.couponName),
                    function (isConfirmed) {
                        $timeout(function () {
                            if (isConfirmed) {
                                vm.saving = true;
                                couponService.deleteCouponConfig({
                                    CouponConfigId: couponConfig.id
                                }).then(function () {
                                    abp.notify.success("操作成功");
                                }).finally(function () {
                                    vm.getCouponConfigList();
                                });
                            }
                        }, 200);
                    }
                );
            };

            vm.toCreateCoupon = function () {
                $state.go('createCoupon', {
                    couponConfigId: ""
                });
            };

            vm.getCouponConfigList();

        }]);
})();