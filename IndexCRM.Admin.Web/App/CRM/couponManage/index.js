(function () {

    appModule.controller('crm.vipManage.vip.index', [
        '$scope', '$uibModal', '$stateParams', 'uiGridConstants', 'abp.services.app.vip',
        function ($scope, $uibModal, $stateParams, uiGridConstants, vipService) {
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

            vm.vipGridOptions = {
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
                            '      <li ng-show="row.entity.status==1"><a ng-click="grid.appScope.ocDisableVip(row.entity)">冻结</a></li>' +
                            '      <li ng-show="row.entity.status==2"><a ng-click="grid.appScope.ocDisableVip(row.entity)">解冻</a></li>' +
                            '      <li ><a ng-click="grid.appScope.changePoint(row.entity)">修改积分</a></li>' +
                            '      <li ><a ng-click="grid.appScope.showVipPointRecord(row.entity)">积分记录</a></li>' +
                            '      <li ><a ng-click="grid.appScope.sendCoupon(row.entity)">赠送优惠券</a></li>' +
                            '      <li ><a ng-click="grid.appScope.showVipCoupon(row.entity)">查看优惠券</a></li>' +
                            '    </ul>' +
                            '  </div>' +
                            '</div>'
                    },
                    {
                        name: app.localize('VipCode'),
                        field: 'vipCode',
                        minWidth: 100
                    },
                    {
                        name: app.localize('NickName'),
                        field: 'vipName',
                        minWidth: 200
                    },
                    {
                        name: app.localize('Sex'),
                        field: 'vipSex',
                        cellTemplate:
                            '<div class=\"ui-grid-cell-contents\">' +
                            '  <span ng-show="row.entity.vipSex==0">女</span>' +
                            '  <span ng-show="row.entity.vipSex==1">男</span>' +
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

            vm.getVipList = function () {
                vm.loading = true;
                vipService.getVipList($.extend({ filter: vm.filterText }, vm.requestParams))
                    .then(function (result) {
                        vm.vipGridOptions.totalItems = result.data.totalCount;
                        vm.vipGridOptions.data = addRoleNamesField(result.data.items);
                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            function addRoleNamesField(users) {
                for (var i = 0; i < users.length; i++) {
                    var user = users[i];
                    user.getRoleNames = function () {
                        var roleNames = '';
                        for (var j = 0; j < this.roles.length; j++) {
                            if (roleNames.length) {
                                roleNames = roleNames + ', ';
                            }
                            roleNames = roleNames + this.roles[j].roleName;
                        };

                        return roleNames;
                    }
                }
                return users;
            }

            vm.ocDisableVip = function (vip) {
                vipService.ocDisableVip({
                    id: vip.id
                }).then(function (result) {
                    vm.getVipList();
                    abp.notify.success("操作成功！");
                });
            }

            vm.changePoint = function (vip) {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/CRM/vipManage/changePointModal.cshtml',
                    controller: 'crm.vipManage.changePointModal as vm',
                    backdrop: 'static',
                    resolve: {
                        vipInfo: function () {
                            return {
                                vipId: vip.id,
                                vipName: vip.vipName,
                                vipPoint: vip.vipPoint
                            };
                        }
                    }
                });

                modalInstance.result.then(function (result) {
                    vm.getVipList();
                });
            }

            vm.showVipPointRecord = function (vip) {
                $uibModal.open({
                    templateUrl: '~/App/CRM/vipManage/showVipPointRecordModal.cshtml',
                    controller: 'crm.vipManage.showVipPointRecordModal as vm',
                    backdrop: 'static',
                    size: 'lg',
                    resolve: {
                        vipInfo: function () {
                            return {
                                vipId: vip.id,
                                vipName: vip.vipName
                            };
                        }
                    }
                });
            }

            vm.sendCoupon = function (vip) {
                var modalInstance = $uibModal.open({
                    templateUrl: '~/App/CRM/vipManage/sendCouponModal.cshtml',
                    controller: 'crm.vipManage.sendCouponModal as vm',
                    backdrop: 'static',
                    resolve: {
                        vipInfo: function () {
                            return {
                                vipId: vip.id,
                                vipName: vip.vipName,
                                vipPoint: vip.vipPoint
                            };
                        }
                    }
                });

                modalInstance.result.then(function (result) {
                    vm.getVipList();
                });
            }

            vm.showVipCoupon = function (vip) {
                $uibModal.open({
                    templateUrl: '~/App/CRM/vipManage/showVipCouponModal.cshtml',
                    controller: 'crm.vipManage.showVipCouponModal as vm',
                    backdrop: 'static',
                    size: 'lg',
                    resolve: {
                        vipInfo: function () {
                            return {
                                vipId: vip.id,
                                vipName: vip.vipName
                            };
                        }
                    }
                });
            }
            
            vm.toCreateCoupon = function () {
                $state.go('couponManage.createCoupon', {

                });
            }

            vm.exportToExcel = function () {
                vipService.getUsersToExcel({})
                    .then(function (result) {
                        app.downloadTempFile(result.data);
                    });
            };

            vm.getVipList();

        }]);
})();