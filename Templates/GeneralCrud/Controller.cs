using AutoMapper;
using Bpf.Api.Areas.Access.Models.Common;
using Bpf.Api.Controllers;
using Bpf.Api.Entities.{!AREA};
using Bpf.Api.Infrastructure;
using Bpf.Api.Services.{!AREA};
using Bpf.Api.Shared.Models;
using Bpf.Common.Canonicals.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bpf.Api.Areas.{!AREA}.Controllers
{
    [Area("{!AREA}")]
    [Route("api/{culture}/[area]/{!URLS}")]
    [ApiController]
    public class {!FIELD}Controller : BaseController
    {
        private readonly {!FIELD}Service _{!FIELD_CC}Service;

        public {!FIELD}Controller({!FIELD}Service {!FIELD_CC}Service,
            Captcha captchaService,
            IMapper mapper) : base(captchaService, mapper)
        {
            _{!FIELD_CC}Service = {!FIELD_CC}Service;
        }

        [HttpPost("get-filter-{!URLS}")]
        [PolicyAuthFilter(AuthorizedModules = new ModuleEnum[] { ModuleEnum.{!FIELDS} }, AccessLevel = AccessLevelEnum.List)]
        public async Task<IActionResult> GetByFilter([FromBody] BaseSearchFilterModel filter)
        {
            var culture = GetAuthenticatedUserCulture();
            var serviceResponse = await _{!FIELD_CC}Service.GetByFilter(filter, culture);

            if (serviceResponse.HasError)
            {
                return GetApiErrorResponse(serviceResponse.ErrorMessage);
            }

            var apiResponse = new ApiResponse<PagedResponse<GeneralFilterResponseModel>>
            {
                Result = serviceResponse.Result
            };
            apiResponse.Result.PageOffset = filter.PageOffset;

            return Ok(apiResponse);
        }

        [HttpGet("{id}")]
        [PolicyAuthFilter(AuthorizedModules = new ModuleEnum[] { ModuleEnum.{!FIELDS} }, AccessLevel = AccessLevelEnum.Read)]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var serviceResponse = await _{!FIELD_CC}Service.GetById(id);
            if (serviceResponse.HasError)
            {
                return GetApiErrorResponse(serviceResponse.ErrorMessage);
            }

            var apiResponse = new ApiResponse<GeneralModel>
            {
                Result = _mapper.Map<GeneralModel>(serviceResponse.Result)
            };

            return Ok(apiResponse);
        }

        [HttpGet("get-{!URLS}-items/{filter?}")]
        [PolicyAuthFilter(AuthorizedModules = new ModuleEnum[] { ModuleEnum.{!OTHER_ENUM}, ModuleEnum.{!FIELDS} }, AccessLevel = AccessLevelEnum.AddEdit)]
        public async Task<IActionResult> GetSelectItemsByFilter(string filter = null)
        {
            var serviceResponse = await _{!FIELD_CC}Service.GetSelectItemsByFilter(filter);

            if (serviceResponse.HasError)
            {
                return GetApiErrorResponse(serviceResponse.ErrorMessage);
            }

            var apiResponse = new ApiResponse<List<SelectItem>>
            {
                Result = serviceResponse.Result
            };

            return Ok(apiResponse);
        }

        [HttpPost]
        [PolicyAuthFilter(AuthorizedModules = new ModuleEnum[] { ModuleEnum.{!OTHER_ENUM}, ModuleEnum.{!FIELDS} }, AccessLevel = AccessLevelEnum.AddEdit)]
        public async Task<IActionResult> Add([FromBody] GeneralModel {!FIELD_CC}Model)
        {
            var captchaResponse = await IsCaptchaTokenValid({!FIELD_CC}Model.CaptchaToken);
            if (captchaResponse.HasError)
            {
                return GetApiErrorResponse(captchaResponse.ErrorMessage);
            }

            var {!FIELD_CC}Entity = _mapper.Map<{!FIELD}>({!FIELD_CC}Model);
            {!FIELD_CC}Entity.ModifiedByUserId = GetAuthenticatedUserId();

            var serviceResponse = await _{!FIELD_CC}Service.Add({!FIELD_CC}Entity);
            if (serviceResponse.HasError)
            {
                return GetApiErrorResponse(serviceResponse.ErrorMessage);
            }

            var apiResponse = new ApiResponse<GeneralModel>
            {
                Result = _mapper.Map<GeneralModel>(serviceResponse.Result)
            };

            return Ok(apiResponse);
        }

        [HttpPut("{id}")]
        [PolicyAuthFilter(AuthorizedModules = new ModuleEnum[] { ModuleEnum.{!FIELDS} }, AccessLevel = AccessLevelEnum.AddEdit)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] GeneralModel {!FIELD_CC}Model)
        {
            var captchaResponse = await IsCaptchaTokenValid({!FIELD_CC}Model.CaptchaToken);
            if (captchaResponse.HasError)
            {
                return GetApiErrorResponse(captchaResponse.ErrorMessage);
            }

            var {!FIELD_CC}Entity = _mapper.Map<{!FIELD}>({!FIELD_CC}Model);
            {!FIELD_CC}Entity.ModifiedByUserId = GetAuthenticatedUserId();
            {!FIELD_CC}Entity.{!FIELD}Id = id;

            var serviceResponse = await _{!FIELD_CC}Service.Update({!FIELD_CC}Entity);
            if (serviceResponse.HasError)
            {
                return GetApiErrorResponse(serviceResponse.ErrorMessage);
            }

            var apiResponse = new ApiResponse<GeneralModel>
            {
                Result = _mapper.Map<GeneralModel>(serviceResponse.Result)
            };

            return Ok(apiResponse);
        }

        [HttpDelete("{id}")]
        [PolicyAuthFilter(AuthorizedModules = new ModuleEnum[] { ModuleEnum.{!FIELDS} }, AccessLevel = AccessLevelEnum.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var authenticatedUserId = GetAuthenticatedUserId();
            var serviceResponse = await _{!FIELD_CC}Service.Delete(id, authenticatedUserId);

            if (serviceResponse.HasError)
            {
                return GetApiErrorResponse(serviceResponse.ErrorMessage);
            }

            var apiResponse = new ApiResponse<bool>
            {
                Result = serviceResponse.Result
            };

            return Ok(apiResponse);
        }
    }
}
