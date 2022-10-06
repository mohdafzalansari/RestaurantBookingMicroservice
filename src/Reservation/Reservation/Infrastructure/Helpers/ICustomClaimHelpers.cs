using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace  Reservation.Infrastructure.Common.Helpers
{
    public interface ICustomClaimHelpers
    {
        public int TenantId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

    }
    public class CustomClaimHelpers : ICustomClaimHelpers
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private ClaimsPrincipal claimsPrincipal;
        public CustomClaimHelpers(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            claimsPrincipal = _contextAccessor.HttpContext.User;
        }

        private int tenantId;
        private string email;
        private string role;

        public int TenantId
        {
            get => tenantId = Convert.ToInt32(_contextAccessor.HttpContext.User.Claims.First(i => i.Type == "TenantId").Value);
            set => tenantId = value;
        }
        public string Email { get => email; set => email = _contextAccessor.HttpContext.User.Claims.First(i => i.Type == "email").Value; }
        public string Role { get => role; set => role = _contextAccessor.HttpContext.User.Claims.First(i => i.Type == "role").Value; }
    }
}
