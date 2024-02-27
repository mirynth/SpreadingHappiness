using System.Collections;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

namespace UI
{
	public class HealthCounterUI : MonoBehaviour
	{
		[SerializeField] private TMP_Text healthCountText;
		private KeyValuePair<float, float> localHealth;

		private void Awake()
		{
			//Just in case the text component is not set
			if (healthCountText == null)
			{
				Debug.LogWarning("HealthCounterUI.Awake: A text component needs to be assigned to the BobaCounter in the inspector");
				return;
			}
		}

		private void OnEnable()
		{
			//Link the UpdateBobaCount function to the PlayerBobaCountChanged event
			UIEvents.PlayerHealthChanged += UpdateHealth;
		}

		private void OnDisable()
		{
			//Remove the UpdateBobaCount function from the PlayerBobaCountChanged event
			UIEvents.PlayerHealthChanged -= UpdateHealth;
		}

		private void UpdateHealth(KeyValuePair<float, float> health_total)
		{
			//Just in case the text component is not set
			if (healthCountText == null)
			{
				Debug.LogWarning("BobaCounterUI.UpdateBobaCount: A text component needs to be assigned to the BobaCounter in the inspector");
				return;
			}

            healthCountText.text = "HP: " + FormatFloat(health_total.Key) + " | " + FormatFloat(health_total.Value);
		}

		string FormatFloat(float value)
		{
			return value.ToString("n2");
		}
	}
}