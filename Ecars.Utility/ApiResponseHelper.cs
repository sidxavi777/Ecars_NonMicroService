using System.Net;
using Ecars.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace Ecars.Utility
{
    public class ApiResponseHelper
    {
        public ApiResponse NotFoundResponse(string message)
        {
            var apiResponse = new ApiResponse
            {
                IsSuccess = false,
                HttpStatusCode = HttpStatusCode.NotFound,
                ErrorMessages = new List<string> { message },
                result = null
            };
            return apiResponse;
        }

        public ApiResponse OkResponse(object result)
        {
            var apiResponse = new ApiResponse
            {
                IsSuccess = true,
                HttpStatusCode = HttpStatusCode.OK,
                ErrorMessages = null,
                result = result
            };
            return apiResponse;
        }

        public ApiResponse HandleException(Exception ex)
        {
            var apiResponse = new ApiResponse
            {
                IsSuccess = false,
                HttpStatusCode = HttpStatusCode.BadRequest,
                ErrorMessages = new List<string> { ex.Message },
                result = null
            };
            return apiResponse;
        }

        public ApiResponse HandleException(List<string> ex)
        {
            var apiResponse = new ApiResponse
            {
                IsSuccess = false,
                HttpStatusCode = HttpStatusCode.BadRequest,
                ErrorMessages = ex,
                result = null
            };
            return apiResponse;
        }
        public ApiResponse HandleException(string ex)
        {
            var apiResponse = new ApiResponse
            {
                IsSuccess = false,
                HttpStatusCode = HttpStatusCode.BadRequest,
                ErrorMessages = new List<string> { ex},
                result = null
            };
            return apiResponse;
        }

        public static ApiResponse BadRequest(ModelStateDictionary modelState)
        {
            var errorMessages = modelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
            var apiResponse = new ApiResponse
            {
                IsSuccess = false,
                HttpStatusCode = HttpStatusCode.BadRequest,
                ErrorMessages = errorMessages,
                result = null
            };
            return apiResponse;
        }
    }

}
