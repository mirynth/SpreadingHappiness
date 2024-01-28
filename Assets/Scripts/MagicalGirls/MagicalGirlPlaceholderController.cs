using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalGirlPlaceholderController : AbstractMagicalGirlController
{
	public MagicalGirlPlaceholderController () : base()
	{
		angryState = new DevAngryMagicalGirl(this);
		happyState = new BasicHappyMagicalGirl(this);
		if (isAngryAtStart)
			magicalGirlState = angryState;
		else magicalGirlState = happyState;
	}
}
