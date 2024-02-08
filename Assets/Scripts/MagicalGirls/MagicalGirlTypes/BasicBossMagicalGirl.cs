using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An angry magical girl that shoots bobas in a circle.
public class BasicBossMagicalGirl : AbstractAngryState
{
	// Time to wait between 2 shots in seconds.
	public override float CooldownTimeBeforeShooting {get {return cooldown;} }
    static Sprite renderable = null;

    bool shot_toggle = false;
    float cooldown = 2.0f;

    int count = 0;
    int difficulty_mod = 0;
    int second_rest = 2;

    public override void Shoot()
    {
        if(shot_toggle)
        {
            cooldown = 2.0f;
            this.ShootInCircle(8.0f, BulletType.Greed, 4 + count * 3, count * 15);
            count++;
            if (count > 3)
            {
                shot_toggle = false;
                count = 0;
                second_rest = 0;
            }
        } 
        else
        {
            cooldown = 0.1f;
            this.ShootMissile(BulletType.Wrath, 1.0f, 15.0f + count/10, 0.3f, true);
            count++;
            if (count > 15 + difficulty_mod * 2)
            {
                if(second_rest >= 3)
                    shot_toggle = true;
                second_rest++;
                count = 0;
                difficulty_mod++;
                cooldown = 0.5f;
            }
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
