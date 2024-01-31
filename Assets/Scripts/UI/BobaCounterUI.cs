using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
	public class BobaCounterUI : MonoBehaviour
	{
		[SerializeField] private TMP_Text bobaCountText;
		[SerializeField, Range(0.1f, 20f), Tooltip("How quickly the text animates when the boba count is updated")] private float textAnimationSpeedScale = 10f;
		[SerializeField, Range(1f, 20f), Tooltip("How high the text jumps when the boba count is updated")] private float textAnimationHeight = 10f;
		private float textDefaultPositionY;
		private int localBobaTotal;

		private void Awake()
		{
			//Just in case the text component is not set
			if (bobaCountText == null)
			{
				Debug.LogWarning("BobaCounterUI.Awake: A text component needs to be assigned to the BobaCounter in the inspector");
				return;
			}

			//Store the default/starting position of the text to use as a reference for the jump animation
			textDefaultPositionY = bobaCountText.rectTransform.anchoredPosition.y;
		}

		private void OnEnable()
		{
			//Link the UpdateBobaCount function to the PlayerBobaCountChanged event
			UIEvents.PlayerBobaCountChanged += UpdateBobaCount;
		}

		private void OnDisable()
		{
			//Remove the UpdateBobaCount function from the PlayerBobaCountChanged event
			UIEvents.PlayerBobaCountChanged -= UpdateBobaCount;
		}

		private void UpdateBobaCount(int newBobaTotal)
		{
			//Just in case the text component is not set
			if (bobaCountText == null)
			{
				Debug.LogWarning("BobaCounterUI.UpdateBobaCount: A text component needs to be assigned to the BobaCounter in the inspector");
				return;
			}

			bobaCountText.text = localBobaTotal.ToString();
			StopAllCoroutines();
			StartCoroutine(TextAnimation(newBobaTotal));
		}

		#region Text Animation

		private IEnumerator TextAnimation(int newBobaTotal)
		{
			float lerpValue = 0f;
			float textPositionX = bobaCountText.rectTransform.anchoredPosition.x;
			float textJumpPositionY = textDefaultPositionY + textAnimationHeight; //Text position at the top of the jump
			Vector2 textPositionContainer = new Vector2(textPositionX, textDefaultPositionY); //Using this Vector2 to update the text position in the loops
			bobaCountText.rectTransform.anchoredPosition = textPositionContainer;
			localBobaTotal = newBobaTotal;
			//Raise the text up to the desired jump position
			while (lerpValue < 1f)
			{
				lerpValue = Mathf.Clamp01(lerpValue + Time.unscaledDeltaTime * textAnimationSpeedScale);
				textPositionContainer.Set(textPositionX, Mathf.Lerp(textDefaultPositionY, textJumpPositionY, lerpValue));
				bobaCountText.rectTransform.anchoredPosition = textPositionContainer;
				yield return null;
			}

			//Update the text component to show the new boba amount
			bobaCountText.text = newBobaTotal.ToString();

			lerpValue = 0f;
			//Bring the text back down to its original position
			while (lerpValue < 1f)
			{
				lerpValue = Mathf.Clamp01(lerpValue + Time.unscaledDeltaTime * textAnimationSpeedScale);
				textPositionContainer.Set(textPositionX, Mathf.Lerp(textJumpPositionY, textDefaultPositionY, lerpValue));
				bobaCountText.rectTransform.anchoredPosition = textPositionContainer;
				yield return null;
			}
		}

		#endregion
	}
}