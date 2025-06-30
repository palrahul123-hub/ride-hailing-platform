using Microsoft.AspNetCore.SignalR;
using RideHailing.Application.Common;
using System.Collections.Concurrent;

namespace RideHailing.API.Hubs
{
    public class LocationHub : Hub
    {

        public ConcurrentDictionary<string, DriverHub> driverDictionary = new ConcurrentDictionary<string, DriverHub>();

        public override async Task OnConnectedAsync()
        {
            HttpContext httpContext = Context.GetHttpContext();

            string latitude = httpContext.Request.Query["Latitude"].ToString();
            string longitude = httpContext.Request.Query["Longitude"].ToString();

            HandleConnectionAsync(Context, latitude, longitude);
            await base.OnConnectedAsync();
        }

        public async Task UpdateDriverLocation(string latitude, string longitude)
        {
            HandleConnectionAsync(Context, latitude, longitude);

        }

        private void HandleConnectionAsync(HubCallerContext context, string latitude, string longitude)
        {

            driverDictionary.AddOrUpdate(context.ConnectionId, _context => new DriverHub
            {
                ConnectionId = context.ConnectionId,
                ConnectedAt = DateTime.UtcNow,
                Latitude = latitude,
                Longitude = longitude
            },
            (key, existingValue) =>
            {
                existingValue.ConnectedAt = DateTime.UtcNow;
                existingValue.Latitude = latitude;
                existingValue.Longitude = longitude;
                return existingValue;
            });
        }



    }
}
