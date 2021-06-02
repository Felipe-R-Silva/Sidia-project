using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic : Player
{
    public Basic() 
    {
        Maxhealth = 120;
        health = 120;
        MAXdamage = 35;
        damage = 35;
        damageBonus = 0;
        MAXmovement = 3;
        movement = 3;
        movementBonus = 0;
        SetType(Player.CharacterType.Basic);
        MAXdice = 3;
        dice = 3;
        diceBonus = 0;
        diceShards = 0;
    }
     override public void ResetForNewTurn()
    {
        SetDamage(MAXdamage);
        SetDamBonus(0);
        SetMovement(MAXmovement);
        SetMovBonus(0);
        SetDice(MAXdice);
        SetDiceBonus(0);
    }

}
