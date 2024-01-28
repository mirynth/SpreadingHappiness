using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A happy magical girl that shoots bobas in a circle.
public class BasicHappyMagicalGirl : AbstractHappyState
{
	// Time to wait between 2 shots in seconds.
	public override float CooldownTimeBeforeShooting {get {return 2.0f;}}
	
	public BasicHappyMagicalGirl(MagicalGirlController mg) : base(mg)
	{}
		
	public override void Shoot(){this.ShootInCircle(5.0f, BulletType.Boba);}
	//public override void Move(){}
}
