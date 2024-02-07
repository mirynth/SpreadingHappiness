using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An angry magical girl that shoots bobas in a circle.
public class BasicAngryMagicalGirl : AbstractAngryState
{
	// Time to wait between 2 shots in seconds.
	public override float CooldownTimeBeforeShooting {get {return 2.0f;}}
		
	public override void Shoot(){
		if (Random.Range(0, 10) == 1)
		{
			this.ShootMissile(BulletType.Greed, 1.0f, 15.0f, 0.2f);
		}
		else
		{
			this.ShootInCircle(5.0f, BulletType.Wrath);
		}
	
	}
	//public override void Move(){}
}
