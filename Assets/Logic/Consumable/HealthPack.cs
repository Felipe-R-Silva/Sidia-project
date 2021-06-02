using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : BaseConsumable
{
    public HealthPack()
    {
        SetType(BaseConsumable.consumableType.HealthPack);
        if (Tier == 0) 
        {
            healthBonus = 5;
        }
        if (Tier == 1)
        {
            healthBonus = 10;
        }
        if (Tier == 2)
        {
            healthBonus = 20;
        }
    }
}
