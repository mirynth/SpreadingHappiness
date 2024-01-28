using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalGirlPlaceholderController : AbstractMagicalGirlController
{
	[SerializeField] protected MagicalGirlTypeEnums.HappyMagicalGirlTypes happyType;
	[SerializeField] protected MagicalGirlTypeEnums.AngryMagicalGirlTypes angryType;

	public MagicalGirlPlaceholderController () : base()
	{
		happyState = MagicalGirlTypeEnums.ConvertHappyType(this.happyType, this);
		angryState = MagicalGirlTypeEnums.ConvertAngryType(this.angryType, this);

		if (isAngryAtStart)
			magicalGirlState = angryState;
		else magicalGirlState = happyState;
	}
}
