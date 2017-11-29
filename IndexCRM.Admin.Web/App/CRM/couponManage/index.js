(function () {

    appModule.controller('crm.couponManage.index', [
        '$scope', '$state', '$uibModal', '$stateParams', 'uiGridConstants', 'abp.services.app.coupon',
        function ($scope, $state, $uibModal, $stateParams, uiGridConstants, couponService) {
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
                        '      <li ><a ng-click="grid.appScope.changePoint(row.entity)">删除</a></li>' +
                        '    </ul>' +
                        '  </div>' +
                        '</div>'
                    },
                    {
                        name: "优惠券名称",
                        field: 'couponName',
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '  <img ng-src="{{row.entity.profilePictureId}}" width="22" height="22" class="img-rounded img-profile-picture-in-grid" />' +
                        '  {{COL_FIELD CUSTOM_FILTERS}} &nbsp;' +
                        '</div>',
                        minWidth: 140
                    },
                    {
                        name: "剩余数量",
                        field: 'couponNum',
                        minWidth: 200
                    },
                    {
                        name: "兑换积分",
                        field: 'couponPoint',
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '  <span class="label label-success">{{row.entity.couponPoint}}</span>' +
                        '</div>',
                        minWidth: 40
                    },
                    {
                        name: app.localize('Birthday'),
                        field: 'vipBirthday',
                        minWidth: 200
                    },
                    {
                        name: app.localize('PhoneNumber'),
                        field: 'vipPhone',
                        minWidth: 100
                    },
                    {
                        name: app.localize('Point'),
                        field: 'vipPoint',
                        minWidth: 40
                    },
                    {
                        name: app.localize('Status'),
                        field: 'status',
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '  <span ng-show="row.entity.status==0" class="label label-default">未激活</span>' +
                        '  <span ng-show="row.entity.status==1" class="label label-success">使用中</span>' +
                        '  <span ng-show="row.entity.status==2" class="label label-danger">冻结中</span>' +
                        '</div>',
                        minWidth: 40
                    },
                    //{
                    //    name: app.localize('LastLoginTime'),
                    //    field: 'lastLoginTime',
                    //    cellFilter: 'momentFormat: \'YYYY-MM-DD HH:mm:ss\'',
                    //    minWidth: 100
                    //},
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

            vm.deleteCoupon = function (vip) {
                couponService.deleteCoupon({
                    id: vip.id
                }).then(function (result) {
                    vm.getVipList();
                    abp.notify.success("操作成功！");
                });
            }

            vm.toCreateCoupon = function () {
                $state.go('createCoupon', {
                    couponConfigId: ""
                });
            }

            vm.getCouponConfigList();

        }]);
})();