using System.Threading.Channels;
using Trading.TradingEngine.Orders.Models;

namespace Trading.Api.Registrations;

public static class OrderChannelRegistration
{
    private const int Capacity = 100;

    public static IServiceCollection AddOrderChannel(this IServiceCollection services)
    {
        var channel = Channel.CreateBounded<ProcessedOrder>(new BoundedChannelOptions(Capacity)
        {
            FullMode = BoundedChannelFullMode.Wait,
            SingleReader = true,
            SingleWriter = false,
        });

        services.AddSingleton(channel.Writer);
        services.AddSingleton(channel.Reader);

        return services;
    }
}
