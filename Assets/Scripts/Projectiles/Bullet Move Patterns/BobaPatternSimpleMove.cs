using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaPatternSimpleMove : AbstractBobaPattern
{
    Vector2 move_direction;
    float speed;

    public BobaPatternSimpleMove(Vector2 move_direction, float speed)
    {
        this.move_direction = move_direction;
        this.speed = speed;
    }

    public override void onFixedUpdate(Rigidbody2D boba_body)
    {
        // Simply move the boba in a straight line
        Vector2 newPosition = boba_body.position + (move_direction * speed * Time.fixedDeltaTime);
        boba_body.MovePosition(newPosition);
    }
}
