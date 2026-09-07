using System.Threading.Channels;
using Trading.PricingEngine.Models;

namespace Trading.Api.Registrations;

public static class PriceChannelRegistration
{
    private const int Capacity = 1000;

    public static IServiceCollection AddPriceChannel(this IServiceCollection services)
    {
        var channel = Channel.CreateBounded<PriceTick>(new BoundedChannelOptions(Capacity)
        {
            FullMode = BoundedChannelFullMode.Wait,
            SingleReader = true,
            SingleWriter = false,
        });

        services.AddSingleton(channel.Writer);   // Trading.PricingEngine (producer)
        services.AddSingleton(channel.Reader);   // Trading.TradingEngine (consumer)

        return services;
    }
}