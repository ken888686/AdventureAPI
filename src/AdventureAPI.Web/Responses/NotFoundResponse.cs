﻿namespace AdventureAPI.Web.Responses;

public class NotFoundResponse(object data)
    : ApiResponse<object>(data, StatusCodes.Status404NotFound)
{
}
