namespace Kader_System.Services.Services.Auth
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<UserContextService> _logger;

        // Cached user ID
        private string? _cachedUserId;

        public UserContextService(IHttpContextAccessor httpContextAccessor, ILogger<UserContextService> logger)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public string? UserId
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_cachedUserId))
                {
                    return _cachedUserId;
                }

                // Retrieve the current HttpContext
                var context = _httpContextAccessor.HttpContext;

                // Check if HttpContext or User is null
                if (context?.User == null)
                {
                    _logger.LogWarning("HttpContext or User is null.");
                    return null;
                }


                // Retrieve the UserId claim
                var userIdClaim = context.User.FindFirstValue(RequestClaims.UserId);

                // Check if the claim is null or empty
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    _logger.LogWarning("UserId claim is missing or empty.");
                    return null;
                }



                _logger.LogWarning("UserId claim is not a valid integer: {UserIdClaim}", userIdClaim);
                return userIdClaim;
            }
        }

        public bool IsAdmin()
        {
            // Ensure UserId is evaluated
            var userId = UserId;

            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }

            return SuperAdmins.Ids.Contains(userId);
        }
    }
}
