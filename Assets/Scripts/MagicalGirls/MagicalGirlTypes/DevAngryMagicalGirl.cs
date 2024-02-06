using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This magical girl type is used in development to test various shooting mechanics.
// This magical girl type should not be used in the final game.
public class DevAngryMagicalGirl : AbstractAngryState
{
	public override float CooldownTimeBeforeShooting {get {return 3.0f;}}
	
    // to-do: have these functions take cooldown time as a parameter
    //public override void Shoot(){ ShootMissileExample(); }
    //public override void Shoot(){ ShootWaveExample(); }
	public override void Shoot(){this.ShootInCircle(10.0f, BulletType.Wrath);}
	//public override void Shoot(){this.ShootMissile(BulletType.Wrath);}

	//public override void Move(){}
}
