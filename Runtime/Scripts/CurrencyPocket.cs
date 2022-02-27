using System;
using System.Collections.Generic;
using System.Linq;
using Pocket.Abstraction;
using UnityEngine;


namespace Pocket
{
    public class CurrencyPocket : ICurrencyPocket<float>
    {
        public Action<string, float> OnCurrencyUpdated { get; private set; }
    
        private ICurrencyPocketConfiguration<float> _configuration;
        private ICurrencyPocketStateHandler<float> _stateHandler;
    
        private readonly Dictionary<string, float> _pocket = new Dictionary<string, float>();

        private static CurrencyPocket _instance;

        public static ICurrencyPocket<float> Instance => _instance ??= new CurrencyPocket();
        
        public bool GetCurrency(string currency, out float value)
        {
            return _pocket.TryGetValue(currency, out value);
        }
    
        public bool AddCurrency(string currency, float value, bool shouldSaveImmediately = true)
        {
            if (_pocket.ContainsKey(currency))
            {
                if (value * -1.0f >= _pocket[currency])
                {
                    _pocket[currency] = 0;
                }
                else
                {
                    _pocket[currency] += value;
                }
    
                if (shouldSaveImmediately)
                {
                    _stateHandler.SaveCurrencyState(currency, _pocket[currency]);
                }
                
                OnCurrencyUpdated?.Invoke(currency, _pocket[currency]);
    
                return true;
            }
    
            return false;
        }
    
        public int GetSupportedCurrencyTypes(out List<string> currencyTypes)
        {
            currencyTypes = _pocket.Keys.ToList();
    
            return currencyTypes.Count;
        }
    
        public void Initialize(ICurrencyPocketConfiguration<float> configuration, ICurrencyPocketStateHandler<float> stateHandler)
        {
            if (_configuration != null && _stateHandler != null)
            {
                return;
            }
            
            if (configuration == null || stateHandler == null)
            {
                throw new ArgumentNullException();
            }
            
            _configuration = configuration;
            _stateHandler = stateHandler;
    
            OnCurrencyUpdated = _configuration.OnCurrencyUpdated;
            
            foreach (string currency in _configuration.SupportedCurrencyTypes)
            {
                _stateHandler.LoadCurrencyState(currency, out float value);
                _pocket.Add(currency, value);
            }
        }

        public void Deinitialize()
        {
            OnCurrencyUpdated -= _configuration.OnCurrencyUpdated;

            foreach (var pair in _pocket)
            {
                _stateHandler.SaveCurrencyState(pair.Key, pair.Value);
            }
        }
    }
}