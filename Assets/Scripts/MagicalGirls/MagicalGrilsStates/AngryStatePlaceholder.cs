using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryStatePlaceholder : AbstractAngryState
{


	public override float CooldownTimeBeforeShooting {get {return 3.0f;}}

	public AngryStatePlaceholder(AbstractMagicalGirlController mg) : base(mg)
	{}

	
    // to-do: have these functions take cooldown time as a parameter
    //public override void Shoot(){ ShootMissileExample(); }
    //public override void Shoot(){ ShootWaveExample(); }
	public override void Shoot(){this.ShootInCircle(300, BulletType.Wrath);}
	//public override void Shoot(){this.ShootMissile(300, BulletType.Wrath);}

	//public override void Move(){}
}
