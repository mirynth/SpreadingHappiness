using System;

namespace UI
{
	public static class UIEvents
	{
		public static event Action<int> PlayerBobaCountChanged;

		public static void OnPlayerBobaCountChanged(int newBobaTotal)
		{
			//Call any functions that have been linked to the PlayerBobaCountChanged event
			PlayerBobaCountChanged?.Invoke(newBobaTotal);
		}
	}
}