using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WrathBulletEffect : AbstractProjectileEffect
{
    Sprite cached_sprite = null;

    public override float GetColliderRadius()
    {
        return 0.53f;
    }

    public override float GetVisualScale()
    {
        return 1.0f;
    }

    public override Sprite GetVisualSprite()
    {
        if (cached_sprite == null)
            cached_sprite = Resources.Load<Sprite>("Art/VFX/enemy_bullet");
        return cached_sprite;
    }

    public override void ApplyVisual(AbstractProjectileController parent)
    {
        parent.GetComponent<SpriteRenderer>().sprite = GetVisualSprite();
        parent.GetComponent<CircleCollider2D>().radius = GetColliderRadius();
        parent.transform.localScale = new Vector3(GetVisualScale(), GetVisualScale(), GetVisualScale());
    }

    public override void OnCollision(AbstractProjectileController parent_controller, MainCharacterController main_character)
    {
        main_character.TakeDamage();
    }
}