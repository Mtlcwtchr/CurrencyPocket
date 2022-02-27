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
            PlayerPrefs.Save();

            return true;
        }

        public bool LoadCurrencyState(string currency, out float value, float defaultValue = default)
        {
            value = PlayerPrefs.GetFloat(PrefsPrefix + currency, defaultValue);

            return PlayerPrefs.HasKey(PrefsPrefix + currency);
        }
    }
}