using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BobaBulletEffect : AbstractProjectileEffect
{
    static Sprite cached_sprite = null;

    Sprite GetVisualSprite()
    {
        if (cached_sprite == null)
            cached_sprite = Resources.Load<Sprite>("Art/Boba_Bullet_1");
        return cached_sprite;
    }

    public override void ApplyVisual(AbstractProjectileController parent_controller)
    {
        parent_controller.GetComponent<SpriteRenderer>().enabled = true;
        parent_controller.GetComponent<CircleCollider2D>().enabled = true;

        parent_controller.GetComponent<SpriteRenderer>().sprite = GetVisualSprite();
        parent_controller.GetComponent<CircleCollider2D>().radius = 1 / 3.36f;
        parent_controller.transform.localScale = new Vector3(0.165f, 0.165f, 0.165f);
    }

    public override void RemoveVisual(AbstractProjectileController parent_controller)
    {
        parent_controller.GetComponent<SpriteRenderer>().enabled = false;
        parent_controller.GetComponent<CircleCollider2D>().enabled = false;
    }

    public override void OnCollision(AbstractProjectileController parent_controller, MainCharacterController main_character)
    {
        main_character.IncrementBobaBitCount();
    }
}