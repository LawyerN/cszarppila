using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using FootballScoreApp.Models;

namespace FootballScoreApp.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiAuthAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("Username", out var usernameValues) ||
                !context.HttpContext.Request.Headers.TryGetValue("ApiToken", out var tokenValues))
            {
                context.Result = new UnauthorizedObjectResult("Brak wymaganych nagłówków: Username lub ApiToken.");
                return;
            }

            var username = usernameValues.FirstOrDefault();
            var token = tokenValues.FirstOrDefault();

            // 2. Pobieramy DbContext za pomocą Service Locator (Wstrzykiwanie zależności w filtrach)
            var dbContext = context.HttpContext.RequestServices.GetRequiredService<AppDbContext>();

            // 3. Sprawdzamy czy użytkownik z takim tokenem istnieje
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == username && u.ApiToken == token);

            if (user == null)
            {
                context.Result = new UnauthorizedObjectResult("Nieprawidłowy Username lub ApiToken.");
                return;
            }

            await next();
        }
    }
}