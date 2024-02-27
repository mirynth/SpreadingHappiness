using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A happy magical girl that shoots bobas in a circle.
public class BasicHappyMagicalGirl : AbstractHappyState
{
	// Time to wait between 2 shots in seconds.
	public override float CooldownTimeBeforeShooting {get {return 2.0f;} }
    static Sprite renderable = null;

    public override void Shoot(){this.ShootInCircle(5.0f, BulletType.Boba); }
    Sprite GetSprite()
    {
        if (renderable == null)
        {
            renderable = Resources.Load<Sprite>("Art/happy mahou colored");
        }
        return renderable;
    }

    public override void ApplyVisual(MagicalGirlController parent_controller)
    {
        parent_controller.magical_girl_renderable.enabled = true;
        parent_controller.magical_girl_renderable.sprite = GetSprite();
        parent_controller.magical_girl_renderable.transform.localScale = Vector3.one * 0.14f;
    }

    public override void RemoveVisual(MagicalGirlController parent_controller)
    {
        parent_controller.magical_girl_renderable.enabled = false;
    }
}
