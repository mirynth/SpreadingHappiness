using UnityEngine;

public abstract class AbstractProjectileEffect
{
    public virtual void Update() { }

    public abstract void ApplyVisual(AbstractProjectileController parent_controller);
    public abstract void RemoveVisual(AbstractProjectileController parent_controller);

    public abstract void OnCollision(AbstractProjectileController parent_controller, MainCharacterController main_character);
}