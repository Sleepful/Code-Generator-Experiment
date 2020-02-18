using Bpf.Api.Areas.Access.Models.Common;
using Bpf.Api.Infrastructure;
using Bpf.Api.Shared;
using Bpf.Api.Shared.Exceptions;
using Bpf.Api.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bpf.Api.Entities.{!AREA};

namespace Bpf.Api.Services.{!AREA}
{
    public class {!FIELD}Service : BaseService
    {
        public {!FIELD}Service(IStringLocalizer<SharedResource> sharedLocalizer,
            DatabaseContext context,
            ILogger<{!FIELD}Service> logger) : base(sharedLocalizer, context, logger) { }

        public async Task<ServiceResponse<PagedResponse<GeneralFilterResponseModel>>> GetByFilter(BaseSearchFilterModel filter, string languageName)
        {
            async Task<PagedResponse<GeneralFilterResponseModel>> FuncAsync()
            {
                return await ExecutePagedStoredProcedureAsync<GeneralFilterResponseModel>(cmd => {
                    SetParameterCommand(cmd, "pFilter", filter.SearchString);
                    SetParameterCommand(cmd, "pIsActive", filter.IsActive);
                    SetParameterCommand(cmd, "pLanguageName", languageName);
                    SetParameterCommand(cmd, "pPageSize", filter.PageSize);
                    SetParameterCommand(cmd, "pPageOffset", filter.PageOffset);

                    cmd.CommandText = "[{AREA}].[uspRetrieve{!FIELDS}ByFilter]";
                });
            };
            return await ExecuteAsync(FuncAsync);
        }

        public async Task<ServiceResponse<{!FIELD}>> GetById(int {!FIELD_CC}Id)
        {
            async Task<{!FIELD_CC}> FuncAsync()
            {
                var {!FIELD_CC} = await _context.{!FIELDS}.FindAsync({!FIELD_CC}Id);
                ValidateDatabaseEntity({!FIELD_CC});
                return {!FIELD_CC};
            };
            return await ExecuteAsync(FuncAsync);
        }

        public async Task<ServiceResponse<List<SelectItem>>> GetSelectItemsByFilter(string filter)
        {
            async Task<List<SelectItem>> FuncAsync()
            {
                var query = _context.{!FIELDS}.AsQueryable();

                if (string.IsNullOrWhiteSpace(filter))
                {
                    query = query.Where(x => x.IsActive)
                        .OrderByDescending(x => x.IsSystemDefault)
                        .OrderByDescending(x => x.{!FIELD}Id)
                        .Take(GlobalHelper.General.MaxInitTakeSearchFilter);
                }
                else
                {
                    query = query.Where(x => x.{!FIELD}Name.Contains(filter) && x.IsActive).OrderBy(x => x.{!FIELD}Name).Take(GlobalHelper.General.MaxTakeSearchFilter);
                }

                var result = await query.Select(x => new SelectItem
                {
                    Id = x.{!FIELD}Id.ToString(),
                    Name = x.{!FIELD}Name
                }).ToListAsync();

                return result;
            };
            return await ExecuteAsync(FuncAsync);
        }

        public async Task<ServiceResponse<{!FIELD}>> Add({!FIELD} {!FIELD_CC})
        {
            async Task<{!FIELD}> FuncAsync()
            {
                var database{!FIELD} = await _context.{!FIELDS}
                    .FirstOrDefaultAsync(x => x.{!FIELD}Name == {!FIELD_CC}.{!FIELD}Name);
                ValidateDatabaseEntityName(database{!FIELD});

                await _context.{!FIELDS}.AddAsync({!FIELD_CC});
                await _context.SaveChangesAsync();

                return {!FIELD_CC};
            };
            return await ExecuteAsync(FuncAsync);
        }

        public async Task<ServiceResponse<{!FIELD}>> Update({!FIELD} {!FIELD_CC})
        {
            async Task<{!FIELD}> FuncAsync()
            {
                var database{!FIELD} = await _context.{!FIELDS}
                    .FirstOrDefaultAsync(
                    x => x.{!FIELD}Id != {!FIELD_CC}.{!FIELD}Id 
                    && x.{!FIELD}Name == {!FIELD_CC}.{!FIELD}Name);
                ValidateDatabaseEntityName(database{!FIELD});

                var {!FIELD_CC}Entity = await _context.{!FIELDS}.FindAsync({!FIELD_CC}.{!FIELD}Id);
                ValidateDatabaseEntity({!FIELD_CC}Entity);

                {!FIELD_CC}Entity.IsActive = {!FIELD_CC}.IsActive;

                if (!{!FIELD_CC}Entity.IsSystemDefault) {
                    {!FIELD_CC}Entity.{!FIELD}Name = {!FIELD_CC}.{!FIELD}Name;
                }
                else if ({!FIELD_CC}Entity.{!FIELD}Name != {!FIELD_CC}.{!FIELD}Name)
                {
                    throw new AppValidationException(_sharedLocalizer["DefaultRecordCannotBeModified"].Value);
                }
                {!FIELD_CC}Entity.Description = {!FIELD_CC}.Description;
                {!FIELD_CC}Entity.ModifiedByUserId = {!FIELD_CC}.ModifiedByUserId;

                _context.Entry({!FIELD_CC}Entity).OriginalValues["RowVersion"] = {!FIELD_CC}.RowVersion;
                await _context.SaveChangesAsync();
                return {!FIELD_CC}Entity;
            };
            return await ExecuteAsync(FuncAsync);
        }

        public async Task<ServiceResponse<bool>> Delete(int {!FIELD_CC}Id, int modifiedByUserId)
        {
            async Task<bool> FuncAsync()
            {
                var {!FIELD_CC}Entity = await _context.{!FIELDS}.FindAsync({!FIELD_CC}Id);
                ValidateDatabaseEntity({!FIELD_CC}Entity);

                if ({!FIELD_CC}Entity.IsSystemDefault)
                {
                    throw new AppValidationException(_sharedLocalizer["DefaultRecordCannotBeModified"].Value);
                }

                {!FIELD_CC}Entity.ModifiedByUserId = modifiedByUserId;
                _context.{!FIELDS}.Remove({!FIELD_CC}Entity);

                await _context.SaveChangesAsync();
                return true;
            };
            return await ExecuteAsync(FuncAsync);
        }
    }
}
