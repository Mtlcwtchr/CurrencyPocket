namespace Pocket.Abstraction
{
    public interface ICurrencyPocketStateHandler<T>
    {
        bool SaveCurrencyState(string currency, T value);
        bool LoadCurrencyState(string currency, out T value, T defaultValue = default);
    }
}