using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalGirlPlaceholderController : AbstractMagicalGirlController
{
	public MagicalGirlPlaceholderController () : base()
	{
		angryState = new AngryStatePlaceholder(this);
		happyState = new HappyStatePlaceholder(this);
		if (isAngryAtStart)
			magicalGirlState = angryState;
		else magicalGirlState = happyState;
	}
}
