using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryStatePlaceholder : AbstractAngryState
{	
	// Time to wait between 2 shots in seconds.
	public override float CooldownTimeBeforeShooting {get {return 2.0f;}}

	public AngryStatePlaceholder(AbstractMagicalGirlController mg) : base(mg)
	{}

	public override void Shoot(){this.ShootInCircle(300, BulletType.Wrath);}
	public override void Move(){}
}
