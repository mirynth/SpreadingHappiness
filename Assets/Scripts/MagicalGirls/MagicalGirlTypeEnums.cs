public class MagicalGirlTypeEnums
{
	// ----- Happy Types -----
	public enum HappyMagicalGirlTypes
	{
		BasicHappyMagicalGirl,
	}

	public static AbstractHappyState ConvertHappyType(HappyMagicalGirlTypes type, AbstractMagicalGirlController controller)
	{
		switch (type)
		{
			case HappyMagicalGirlTypes.BasicHappyMagicalGirl:
				return new BasicHappyMagicalGirl(controller);
			default:
				return null;
		}
	}

	// ----- Angry Types -----
	public enum AngryMagicalGirlTypes
	{
		BasicAngryMagicalGirl,
		DevAngryMagicalGirl,
	}

	public static AbstractAngryState ConvertAngryType(AngryMagicalGirlTypes type, AbstractMagicalGirlController controller)
	{
		switch (type)
		{
			case AngryMagicalGirlTypes.BasicAngryMagicalGirl:
				return new BasicAngryMagicalGirl(controller);
			case AngryMagicalGirlTypes.DevAngryMagicalGirl:
				return new DevAngryMagicalGirl(controller);
			default:
				return null;
		}
	}
}
