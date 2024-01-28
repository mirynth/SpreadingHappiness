using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaBitController : AbstractProjectileController
{	
	public override void OnCollisionEffect(MainCharacterController e)
	{
			e.IncrementBobaBitCount();		
	}
}
