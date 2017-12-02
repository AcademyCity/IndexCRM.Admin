using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Threading;
using Abp.UI;
using IndexCRM.Admin.Authorization;
using IndexCRM.Admin.Function;
using System.Linq.Dynamic;
using Abp.Linq.Extensions;
using Abp.Extensions;
using System.Configuration;
using IndexCRM.Admin.CRM.storeManage.Dto;

namespace IndexCRM.Admin.CRM.storeManage
{
    [AbpAuthorize(AppPermissions.CRM_StoreManage)]
    public class StoreAppService : AdminAppServiceBase, IStoreAppService
    {

        private readonly IRepository<Store, string> _storeRepository;

        public StoreAppService(
            IRepository<Store, string> storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<GetStoreForEditDto> GetStoreForEdit(GetStoreInput input)
        {
            if (string.IsNullOrEmpty(input.StoreId))
            {
                //Creating a new coupon
                var storeDto = new GetStoreForEditDto();

                return storeDto;
            }
            else
            {
                //Editing an existing coupon
                var couponConfig = _storeRepository.FirstOrDefault(u => u.Id == input.StoreId);

                var couponConfigDto = couponConfig.MapTo<GetStoreForEditDto>();

                return couponConfigDto;
            }
        }

        public async Task CreateOrUpdateStore(GetStoreForEditInput input)
        {
            if (string.IsNullOrEmpty(input.Store.Id))
            {
                await CreateStoreAsync(input);
            }
            else
            {
                await UpdateStoreAsync(input);
            }
        }

        protected virtual async Task UpdateStoreAsync(GetStoreForEditInput input)
        {
            var store = await _storeRepository.FirstOrDefaultAsync(u => u.Id == input.Store.Id);

            //Update user properties
            input.Store.MapTo(store); //Passwords is not mapped (see mapping configuration)
            store.ModifyMan = AsyncHelper.RunSync(() => UserManager.GetUserByIdAsync((long)AbpSession.UserId)).Name;
            store.ModifyTime = DateTime.Now;

            await _storeRepository.UpdateAsync(store);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        protected virtual async Task CreateStoreAsync(GetStoreForEditInput input)
        {
            var store = input.Store.MapTo<Store>();

            store.Id = Guid.NewGuid().ToString().ToUpper();
            store.IsDelete = false;
            store.AddMan = AsyncHelper.RunSync(() => UserManager.GetUserByIdAsync((long)AbpSession.UserId)).Name;
            store.AddTime = DateTime.Now;
            store.ModifyMan = AsyncHelper.RunSync(() => UserManager.GetUserByIdAsync((long)AbpSession.UserId)).Name;
            store.ModifyTime = DateTime.Now;

            await _storeRepository.InsertAsync(store);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public async Task<PagedResultDto<GetStoreForEditDto>> GetStoreList(GetStoreInput input)
        {
            var store = _storeRepository.GetAll()
                .Where(u => u.IsDelete == false)
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), u => u.StoreNo == input.Filter);

            var storeCount = await store.CountAsync();
            var storeList = await store
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();


            var storeListDto = storeList.MapTo<List<GetStoreForEditDto>>();

            return new PagedResultDto<GetStoreForEditDto>(
                storeCount,
                storeListDto
                );
        }

        public async Task DeleteStore(GetStoreInput input)
        {
            //Editing an existing coupon
            var couponConfig = _storeRepository.FirstOrDefault(u => u.Id == input.StoreId);
            couponConfig.IsDelete = true;
            await _storeRepository.UpdateAsync(couponConfig);
        }

    }
}
