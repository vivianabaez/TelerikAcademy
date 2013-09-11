﻿namespace CoolBlog.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using CoolBlog.WebApi.Models;

    public class BaseApiController : ApiController
    {
        private static Dictionary<string, HttpStatusCode> ErrorToStatusCodes = new Dictionary<string, HttpStatusCode>();

        static BaseApiController()
        {
            ErrorToStatusCodes["ERR_INV_USR"] = HttpStatusCode.Unauthorized;
            ErrorToStatusCodes["ERR_INV_AUTH"] = HttpStatusCode.Unauthorized;
            ErrorToStatusCodes["INV_USR_AUTH"] = HttpStatusCode.Unauthorized;
            ErrorToStatusCodes["INV_GAME_PASS"] = HttpStatusCode.Unauthorized;
            ErrorToStatusCodes["ERR_DUP_USR"] = HttpStatusCode.Conflict;
            ErrorToStatusCodes["ERR_DUP_NICK"] = HttpStatusCode.Conflict;
            ErrorToStatusCodes["INV_USR_LEN"] = HttpStatusCode.BadRequest;
            ErrorToStatusCodes["INV_USR_CHARS"] = HttpStatusCode.BadRequest;
            ErrorToStatusCodes["INV_NICK_CHARS"] = HttpStatusCode.BadRequest;
            ErrorToStatusCodes["INV_NICK_LEN"] = HttpStatusCode.BadRequest;
            ErrorToStatusCodes["ERR_GEN_SVR"] = HttpStatusCode.InternalServerError;
        }

        public BaseApiController()
        {
        }

        protected HttpResponseMessage PerformOperation(Action action)
        {
            try
            {
                action();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ServerErrorException ex)
            {
                return BuildErrorResponse(ex.Message, ex.ErrorCode);
            }
            catch (Exception ex)
            {
                var errCode = "ERR_GEN_SVR";
                return BuildErrorResponse(ex.Message, errCode);
            }
        }

        protected HttpResponseMessage PerformOperation<T>(Func<T> action)
        {
            try
            {
                var result = action();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (ServerErrorException ex)
            {
                return BuildErrorResponse(ex.Message, ex.ErrorCode);
            }
            catch (Exception ex)
            {
                var errCode = "ERR_GEN_SVR";
                return BuildErrorResponse(ex.Message, errCode);
            }
        }

        private HttpResponseMessage BuildErrorResponse(string message, string errCode)
        {
            var httpError = new HttpError(message);
            httpError["errCode"] = errCode;
            var statusCode = ErrorToStatusCodes[errCode];
            return Request.CreateErrorResponse(statusCode, httpError);
        }
    }
}