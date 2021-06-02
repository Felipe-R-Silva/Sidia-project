using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPack : BaseConsumable
{
    public MovementPack()
    {
        SetType(BaseConsumable.consumableType.MovementBoost);
        if (Tier == 0)
        {
            movementBonus = 1;
        }
        if (Tier == 1)
        {
            movementBonus = 2;
        }
        if (Tier == 2)
        {
            movementBonus = 3;
        }
    }
}
