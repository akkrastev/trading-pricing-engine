using System;
using System.Collections.Generic;
using System.Text;
using Trading.PricingEngine.Models;

namespace Trading.PricingEngine.Services.Interfaces
{
    public interface IPriceTickValidator
    {
        bool IsValid(PriceTick data);
    }
}
