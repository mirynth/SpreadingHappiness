using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/*
 * Grows in size over time
 * */
public class GreedBulletEffect : AbstractProjectileEffect
{
    static Sprite cached_sprite = null;

    float starting_size_multiplier = 1.0f;
    float final_size_multiplier = 5.0f;
    float current_size = 0.0f;
    float base_size = 0.53f;

    float size_growth_timer = 5.0f;
    float size_growth_counter = 0.0f;
    AbstractProjectileController parent;

    public GreedBulletEffect(float starting_size, float final_size, float growth_time)
    {
        starting_size_multiplier = starting_size;
        final_size_multiplier = final_size;
        current_size = starting_size;

        size_growth_timer = growth_time;
        size_growth_counter = growth_time;
    }

    public override void Update()
    {
        size_growth_counter = Mathf.Max(0, size_growth_counter - Time.deltaTime);
        current_size = starting_size_multiplier + ((final_size_multiplier - starting_size_multiplier) * (1.0f - (size_growth_counter / size_growth_timer)));

        parent.transform.localScale = Vector3.one * current_size;
    }

    Sprite GetVisualSprite()
    {
        if (cached_sprite == null)
            cached_sprite = Resources.Load<Sprite>("Art/VFX/enemy_bullet");
        return cached_sprite;
    }

    public override void ApplyVisual(AbstractProjectileController parent_controller)
    {
        parent = parent_controller;
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