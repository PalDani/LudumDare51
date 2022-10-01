using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Litch : Enemy
{
    protected override Collider2D CastAttackArea()
    {
        //TODO: Cast Magic towards player
        return base.CastAttackArea();
    }
}
