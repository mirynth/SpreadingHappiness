using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BobaBulletEffect : AbstractProjectileEffect
{
    Sprite cached_sprite = null;

    public override float GetColliderRadius()
    {
        return 1 / 3.36f;
    }

    public override float GetVisualScale()
    {
        return 0.165f;
    }

    public override Sprite GetVisualSprite()
    {
        if (cached_sprite == null)
            cached_sprite = Resources.Load<Sprite>("Art/Boba_Bullet_1");
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
        main_character.IncrementBobaBitCount();
    }
}