using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicePack : BaseConsumable
{
    public DicePack()
    {
        SetType(BaseConsumable.consumableType.DicePack);
        if (Tier == 0)
        {
            diceShard = 1;
        }
        if (Tier == 1)
        {
            diceShard = 2;
        }
        if (Tier == 2)
        {
            diceBonust = 1;
        }
    }
}
