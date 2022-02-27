using Pocket.Abstraction;
using UnityEngine;


namespace Pocket
{
    public class PlayerPrefsCurrencyPocketStateHandler : ICurrencyPocketStateHandler<float>
    {
        private const string PrefsPrefix = "scurr_";
        
        public bool SaveCurrencyState(string currency, float value)
        {
            PlayerPrefs.SetFloat(PrefsPrefix + currency, value);

            return true;
        }

        public bool LoadCurrencyState(string currency, out float value, float defaultValue = default)
        {
            value = PlayerPrefs.GetFloat(currency, defaultValue);

            return PlayerPrefs.HasKey(currency);
        }
    }
}