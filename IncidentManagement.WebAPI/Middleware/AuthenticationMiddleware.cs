using IncidentManagement.BusinessLogic.User;

namespace IncidentManagement.WebAPI.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (!httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                await _next(httpContext);
                return;
            }

            var requestToken = httpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(requestToken))
            {
                await _next(httpContext);
                return;
            }

            try
            {
                var userRepository = httpContext.RequestServices.GetRequiredService<IUserRepository>();

                var validatedToken = await userRepository.ValidateToken(requestToken);

                if (validatedToken == null)
                {
                    await _next(httpContext);
                    return;
                }

                var userName = validatedToken?.Claims.FirstOrDefault(c => c.Type == "UserName");

                if (userName == null)
                {
                    await _next(httpContext);
                    return;
                }


                //if user is not null then we can add the user to the context.
                //Meaning its a valid token and we can use it for Authorization 
                var userModel = await userRepository.GetUserByUserName(userName.Value);

                httpContext.Items["User"] = userModel;

                await _next(httpContext);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}
