using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This magical girl type is used in development to test various shooting mechanics.
// This magical girl type should not be used in the final game.
public class DevAngryMagicalGirl : AbstractAngryState
{
	public override float CooldownTimeBeforeShooting {get {return 3.0f;}}

    static Sprite renderable = null;

    Sprite GetSprite()
    {
        if(renderable ==  null)
        {
            renderable = Resources.Load<Sprite>("Art/ANGY");
        }
        return renderable;
    }

    public override void ApplyVisual(MagicalGirlController parent_controller)
    {
        parent_controller.magical_girl_renderable.enabled = true;
        parent_controller.magical_girl_renderable.sprite = GetSprite();
        parent_controller.magical_girl_renderable.transform.localScale = Vector3.one * 0.36f;
    }

    public override void RemoveVisual(MagicalGirlController parent_controller)
    {
        parent_controller.magical_girl_renderable.enabled = false;
    }

    // to-do: have these functions take cooldown time as a parameter
    //public override void Shoot(){ ShootMissileExample(); }
    //public override void Shoot(){ ShootWaveExample(); }
    public override void Shoot(){this.ShootInCircle(10.0f, BulletType.Wrath);}
	//public override void Shoot(){this.ShootMissile(BulletType.Wrath);}

	//public override void Move(){}
}
