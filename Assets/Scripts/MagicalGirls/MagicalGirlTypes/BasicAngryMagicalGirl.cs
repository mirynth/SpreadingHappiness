using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An angry magical girl that shoots bobas in a circle.
public class BasicAngryMagicalGirl : AbstractAngryState
{
	// Time to wait between 2 shots in seconds.
	public override float CooldownTimeBeforeShooting {get {return 2.0f;} }
    static Sprite renderable = null;

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

    Sprite GetSprite()
    {
        if (renderable == null)
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
}
