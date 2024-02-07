using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WrathBulletEffect : AbstractProjectileEffect
{
    static Sprite cached_sprite = null;

    Sprite GetVisualSprite()
    {
        if (cached_sprite == null)
            cached_sprite = Resources.Load<Sprite>("Art/VFX/enemy_bullet");
        return cached_sprite;
    }

    public override void ApplyVisual(AbstractProjectileController parent_controller)
    {
        parent_controller.GetComponent<SpriteRenderer>().enabled = true;
        parent_controller.GetComponent<CircleCollider2D>().enabled = true;

        parent_controller.GetComponent<SpriteRenderer>().sprite = GetVisualSprite();
        //Exact image radius is 0.53, but shaving a bit off works better visually.
        parent_controller.GetComponent<CircleCollider2D>().radius = 0.45f;
        parent_controller.transform.localScale = Vector3.one;
    }

    public override void RemoveVisual(AbstractProjectileController parent_controller)
    {
        parent_controller.GetComponent<SpriteRenderer>().enabled = false;
        parent_controller.GetComponent<CircleCollider2D>().enabled = false;
    }

    public override void OnCollision(AbstractProjectileController parent_controller, MainCharacterController main_character)
    {
        main_character.TakeDamage();
    }
}