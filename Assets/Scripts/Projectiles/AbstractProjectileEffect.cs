using UnityEngine;

public abstract class AbstractProjectileEffect
{
    public abstract float GetVisualScale();
    public abstract Sprite GetVisualSprite();
    public abstract float GetColliderRadius();

    public abstract void ApplyVisual(AbstractProjectileController parent_controller);
    public abstract void OnCollision(AbstractProjectileController parent_controller, MainCharacterController main_character);
}