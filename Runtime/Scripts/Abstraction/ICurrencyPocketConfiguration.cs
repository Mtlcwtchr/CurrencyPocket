using System;
using System.Collections.Generic;


namespace Pocket.Abstraction
{
    public interface ICurrencyPocketConfiguration<in T>
    {
        Action<string, T> OnCurrencyUpdated { get; }
        List<string> SupportedCurrencyTypes { get; }
    }
}