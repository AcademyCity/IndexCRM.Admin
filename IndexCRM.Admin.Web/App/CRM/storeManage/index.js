(function () {

    appModule.controller('crm.storeManage.index', [
        '$scope', '$state', '$uibModal', '$stateParams', "$timeout", 'uiGridConstants', 'abp.services.app.store',
        function ($scope, $state, $uibModal, $stateParams, $timeout, uiGridConstants, storeService) {
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

            vm.storeGridOptions = {
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
                        '      <li ><a ng-click="grid.appScope.editStore(row.entity)">修改门店</a></li>' +
                        '      <li ><a ng-click="grid.appScope.deleteStore(row.entity)">删除门店</a></li>' +
                        '    </ul>' +
                        '  </div>' +
                        '</div>'
                    },
                    {
                        name: '店号',
                        field: 'storeNo',
                        minWidth: 140
                    },
                    {
                        name: '店名',
                        field: 'storeName',
                        minWidth: 100
                    },
                    {
                        name: '门店电话',
                        field: 'storePhone',
                        minWidth: 100
                    },
                    {
                        name: '门店地址',
                        field: 'storeAddr',
                        minWidth: 100
                    },
                    {
                        name: '门店坐标',
                        field: 'storeLocation',
                        minWidth: 100
                    },
                    {
                        name: '是否显示',
                        field: 'isShow',
                        cellTemplate:
                        '<div class=\"ui-grid-cell-contents\">' +
                        '  <span ng-show="row.entity.isShow==1" class="label label-success">是</span>' +
                        '  <span ng-show="row.entity.isShow==0" class="label label-danger">否</span>' +
                        '</div>',
                        maxWidth: 100
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

            vm.getStoreList = function () {
                vm.loading = true;
                storeService.getStoreList($.extend({ filter: vm.filterText }, vm.requestParams))
                    .then(function (result) {
                        vm.storeGridOptions.totalItems = result.data.totalCount;
                        vm.storeGridOptions.data = result.data.items;
                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            vm.editStore = function (store) {
                $state.go('createStore', {
                    storeId: store.id
                });
            };

            vm.deleteStore = function (store) {
                abp.message.confirm(
                    abp.utils.formatString("删除门店\n\r\n\r门店名称:{0}", store.storeName),
                    function (isConfirmed) {
                        $timeout(function () {
                            if (isConfirmed) {
                                vm.saving = true;
                                storeService.deleteStore({
                                    storeId: store.id
                                }).then(function () {
                                    abp.notify.success("操作成功");
                                }).finally(function () {
                                    vm.getStoreList();
                                });
                            }
                        }, 200);
                    }
                );
            };

            vm.toCreateStore = function () {
                $state.go('createStore', {
                    storeId: ""
                });
            };

            vm.getStoreList();

        }]);
})();