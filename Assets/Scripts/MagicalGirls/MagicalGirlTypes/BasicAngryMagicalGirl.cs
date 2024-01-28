using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An angry magical girl that shoots bobas in a circle.
public class BasicAngryMagicalGirl : AbstractAngryState
{
	// Time to wait between 2 shots in seconds.
	public override float CooldownTimeBeforeShooting {get {return 2.0f;}}
	
	public BasicAngryMagicalGirl(MagicalGirlController mg) : base(mg)
	{}
		
	public override void Shoot(){this.ShootInCircle(5.0f, BulletType.Wrath);}
	//public override void Move(){}
}
