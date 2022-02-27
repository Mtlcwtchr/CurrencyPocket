using System;
using System.Collections.Generic;


namespace Pocket.Abstraction
{
    public interface ICurrencyPocket<T>
    {
        Action<string, T> OnCurrencyUpdated { get; }
        
        bool GetCurrency(string currency, out T value);
        bool AddCurrency(string currency, T value, bool shouldSaveImmediately = true);

        int GetSupportedCurrencyTypes(out List<string> currencyTypes);

        void Initialize(ICurrencyPocketConfiguration<T> configuration, ICurrencyPocketStateHandler<T> stateHandler);
        void Deinitialize();
    }
}