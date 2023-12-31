﻿using ConcordiaCurriculumManager.Services;
using Microsoft.AspNetCore.Authorization;

namespace ConcordiaCurriculumManager.Security.Requirements.Handlers;

public class GroupMasterHandler : AuthorizationHandler<GroupMasterOrAdminRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<GroupMasterHandler> _logger;
    private readonly IGroupService _groupService;

    public GroupMasterHandler(ILogger<GroupMasterHandler> logger, IGroupService groupService, IHttpContextAccessor contextAccessor)
    {
        _logger = logger;
        _groupService = groupService;
        _httpContextAccessor = contextAccessor;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, GroupMasterOrAdminRequirement requirement)
    {
        _logger.LogInformation("Evaluting GroupMasterHandler");

        if (_httpContextAccessor.HttpContext is null)
        {
            // This is not an Http Request or there is no group Id. Abstain
            _logger.LogWarning("Context is null");
            return;
        }

        if (context.User.Identity is null || !context.User.Identity.IsAuthenticated)
        {
            context.Fail();
            return;
        }

        if (!_httpContextAccessor.HttpContext.Request.RouteValues.TryGetValue("groupId", out var groupId))
        {
            // There is no groupId in the request, but it might an endpoint that allows any group master
            var groupsMastered = context.User.Claims.FirstOrDefault(c => c.Type.Equals(Claims.GroupMaster));
            if (groupsMastered != null)
            {
                context.Succeed(requirement);
                return;
            }
        }

        var userId = context.User.Claims.FirstOrDefault(c => c.Type.Equals(Claims.Id));

        if (userId is null || !Guid.TryParse(userId.Value, out var parsedUserId))
        {
            _logger.LogWarning("User is authenticated without a valid Id claim");
            context.Fail();
            return;
        }

        Guid.TryParse(groupId?.ToString(), out var parsedGroupId);
        var isGroupMaster = await _groupService.IsGroupMaster(parsedUserId, parsedGroupId);

        if (isGroupMaster)
        {
            context.Succeed(requirement);
        }
    }
}
