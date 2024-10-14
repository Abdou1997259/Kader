using Kader_System.DataAccess.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace Kader_System.DataAccess.DbMiddlewares
{
    public class ClientDatabaseMiddleware(RequestDelegate next, IConfiguration configuration)
    {

        private readonly RequestDelegate _next = next;
        private readonly IConfiguration _configuration = configuration;

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("client_id", out var clientId))
            {
                if (!int.TryParse(clientId, out var client_id))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Invalid client_id: Must be a number");
                    return;
                }

                string authorizationConnectionString = _configuration.GetConnectionString("KaderAuthorizationConnection");
                using var authorizationContext = new KaderAuthorizationContext(
                    new DbContextOptionsBuilder<KaderAuthorizationContext>()
                        .UseSqlServer(authorizationConnectionString)
                        .Options);

                var test = await authorizationContext.Clients.ToListAsync();
                var client = await authorizationContext.Clients
                    .Where(c => c.ClientCode == client_id && c.ClientActive == true)
                    .Select(x => x.ClientCode)
                    .FirstOrDefaultAsync();

                if (client == null)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized: client_id doesn't exist");
                    return;
                }
                string connStringWithClineId = CreateKaderSystemConnectionString(client_id);
                context.Items["KaderSystemConnectionString"] = connStringWithClineId;

                // Set the new connection string in the DbContext options
                var dbContext = context.RequestServices.GetRequiredService<KaderDbContext>();
                dbContext.Database.SetConnectionString(connStringWithClineId);
                var cs = dbContext.Database.GetConnectionString();
            }

            await _next(context);
        }

        private string CreateKaderSystemConnectionString(int clientId)
        {
            var builder = new SqlConnectionStringBuilder(_configuration.GetConnectionString("KaderSystemConnection"))
            {
                InitialCatalog = $"KaderSystem_{clientId}"
            };
            return builder.ConnectionString;
        }
    }

}
