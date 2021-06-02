using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Runner : Player
{
    public Runner()
    {
        Maxhealth = 90;
        health = 90;
        MAXdamage = 20;
        damage = 20;
        damageBonus = 0;
        MAXmovement = 5;
        movement = 5;
        movementBonus = 0;
        SetType(Player.CharacterType.Runner);
        MAXdice = 2;
        dice = 2;
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
