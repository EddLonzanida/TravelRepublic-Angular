using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace TravelRepublic.Api.Helpers
{
    public static class CustomApiConventions
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Index([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix),
                                  ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object request)
        {
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Get([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix),
                                  ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object request)
        {
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void IndexWithParent([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix),
                                            ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object parentId
            , [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix),
               ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object request)
        {
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Suggestions([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix),
                                        ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object search)
        {
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void SuggestionsWithParent([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix),
                                                  ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object parentId,
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix), 
             ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object search)
        {
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Details([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix),
                                    ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object id)
        {
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Create([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix),
                                   ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object request)
        {
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Post([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix),
                                   ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object request)
        {
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Edit([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix),
                                 ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object request)
        {
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Delete([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix),
                                   ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object id)
        {
        }
    }
}
