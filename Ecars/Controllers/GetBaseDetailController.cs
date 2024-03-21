using AutoMapper;
using Ecars.Database.Repository;
using Ecars.Model;
using Ecars.Model.Dto_s;
using Ecars.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Ecars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseDetailController : ControllerBase
    {
        protected ApiResponse _APIRes;
        private readonly IRepository<BaseDetail> _Repository;
        private ApiResponseHelper _ApiResponseHelper;
        private readonly IMapper _mapper;
        public BaseDetailController(IRepository<BaseDetail> Repository, IMapper mapper)
        {
            _APIRes = new ApiResponse();
            _Repository = Repository;
            _ApiResponseHelper = new ApiResponseHelper();
            _mapper = mapper;
        }

        //Get All Base details.
        [HttpGet]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Route("GetAllBaseDetails")]

        public async Task<ApiResponse> GetAllBaseDetails()
        {
            try
            {
                var allBaseDetails = await _Repository.GetAllAsync();

                if (allBaseDetails == null || !allBaseDetails.Any())
                {
                    var Response = _ApiResponseHelper.NotFoundResponse("No data found");
                    return Response;
                }

                return _ApiResponseHelper.OkResponse(allBaseDetails);
            }
            catch (Exception ex)
            {
                return _ApiResponseHelper.HandleException(ex);
            }
        }

        //Get Model by id
        [HttpGet("{id}")]
        //[Route("GetBaseDetailById")]
        public async Task<ApiResponse> GetBaseDetailById(int id)
        {

            try
            {
                var getDetailsById = await _Repository.GetAllAsyncById(id);

                if (getDetailsById == null)
                {
                    var Response = _ApiResponseHelper.NotFoundResponse("No car with given id:" + id + " was found");
                    return Response;
                }

                return _ApiResponseHelper.OkResponse(getDetailsById);
            }
            catch (Exception ex)
            {
                return _ApiResponseHelper.HandleException(ex);
            }

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        //[Route("PostBaseDetail")]

        public async Task<ApiResponse> PostBaseDetail([FromBody] PostBaseDetail_DTO postBaseDetail_DTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return ApiResponseHelper.BadRequest(ModelState);
                }

                BaseDetail baseDetails = _mapper.Map<BaseDetail>(postBaseDetail_DTO);

                await _Repository.CreateAsync(baseDetails);
                await _Repository.SaveAsync();
                _APIRes.result = _mapper.Map<PostBaseDetail_DTO>(baseDetails);
                _APIRes.HttpStatusCode = HttpStatusCode.Created;
                return _APIRes;


            }
            catch (Exception ex)
            {
                return _ApiResponseHelper.HandleException(ex);
            }

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ApiResponse> DeleteBaseDetailById(int id)
        {
            try
            {
                var BaseDetailById = await _Repository.GetAllAsyncById(id);
                if (BaseDetailById == null)
                {
                    var response = _ApiResponseHelper.NotFoundResponse("No Model with given id:" + id + " was found");
                    return response;
                }
                else
                {
                    await _Repository.DeleteAsync(id);
                    await _Repository.SaveAsync();

                    // Return OK response after successful deletion
                    return _ApiResponseHelper.OkResponse("The model was successfully deleted");
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
                return _ApiResponseHelper.HandleException(ex);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ApiResponse> EditBaseDetailById(int id, [FromBody] PostBaseDetail_DTO postBaseDetail_DTO)
        {
            try
            {
                var BaseDetailById = await _Repository.GetAllAsyncById(id);
                if (BaseDetailById == null)
                {
                    var response = _ApiResponseHelper.NotFoundResponse("No Model with given id:" + id + " was found");
                    return response;
                }

                var updatedBaseDetailById = _mapper.Map<BaseDetail>(postBaseDetail_DTO);
                updatedBaseDetailById.Id = id;
                await _Repository.UpdateASync(updatedBaseDetailById);
                await _Repository.SaveAsync();

                return _ApiResponseHelper.OkResponse("The model was successfully Updated");
            }
            catch (Exception ex)
            {
                // Handle the exception
                return _ApiResponseHelper.HandleException(ex);
            }
        }

    }
}
