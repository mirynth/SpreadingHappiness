using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
	public static class UIEvents
	{
		public static event Action<int> PlayerBobaCountChanged;
        public static event Action<KeyValuePair<float, float>> PlayerHealthChanged;

        public static void OnPlayerBobaCountChanged(int newBobaTotal)
		{
			//Call any functions that have been linked to the PlayerBobaCountChanged event
			PlayerBobaCountChanged?.Invoke(newBobaTotal);
        }
        public static void OnPlayerHealthChanged(KeyValuePair<float, float> newHealth)
        { 
            PlayerHealthChanged?.Invoke(newHealth);
        }
    }
}