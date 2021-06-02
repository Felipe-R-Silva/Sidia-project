using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePack : BaseConsumable
{
    public DamagePack()
    {
        SetType(BaseConsumable.consumableType.AtackBoost);
        if (Tier == 0)
        {
            damageBonus = 3;
        }
        if (Tier == 1)
        {
            damageBonus = 5;
        }
        if (Tier == 2)
        {
            damageBonus = 10;
        }
    }
}
