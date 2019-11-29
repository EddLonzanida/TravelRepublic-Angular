using Microsoft.AspNetCore.Mvc;

namespace TravelRepublic.Tests.Utils.Extensions
{
    public static class ActionResultExtensions
    {
        public static TValue GetValue<TValue>(this ActionResult controllerActionResult)
            where TValue : class
        {
            if (controllerActionResult is OkObjectResult result)
            {
                return result.Value as TValue;
            }

            return null;
        }

        public static TValue GetValue<TValue>(this ActionResult<TValue> controllerActionResult)
            where TValue : class
        {
            switch (controllerActionResult.Result)
            {
                case OkObjectResult result:

                    return result.Value as TValue;

                case CreatedAtActionResult createdAtActionResult:

                    return createdAtActionResult.Value as TValue;

                default:
                    return null;
            }
        }

        public static int GetStatusCode(this ActionResult controllerActionResult)
        {
            if (controllerActionResult is StatusCodeResult result)
            {
                return result.StatusCode;
            }

            return -1;
        }

        public static int GetStatusCode<TValue>(this ActionResult<TValue> controllerActionResult)
        {

            if (controllerActionResult.Result is StatusCodeResult result)
            {
                return result.StatusCode;
            }

            if (controllerActionResult.Result is OkObjectResult okObjectResult)
            {
                return okObjectResult.StatusCode ?? -1;
            }

            if (controllerActionResult.Result is CreatedAtActionResult createdAtActionResult)
            {
                return createdAtActionResult.StatusCode ?? -1;
            }

            return -1;
        }
    }
}
