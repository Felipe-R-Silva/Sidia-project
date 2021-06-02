using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseConsumable : MonoBehaviour
{
    public enum consumableType { Unknown, HealthPack, DicePack, AtackBoost, MovementBoost,Empty };
    [SerializeField] protected private int Tier;
    [SerializeField] protected private consumableType Type;
    [SerializeField] protected private Tyle tyleOwner;
    //---
    [SerializeField] protected private int healthBonus;
    [SerializeField] protected private int damageBonus;
    [SerializeField] protected private int movementBonus;
    [SerializeField] protected private int diceBonust;
    [SerializeField] protected private int diceShard;
    public static event Action<GameObject, int, int, int, int, int> BrodcastPlayerCollectedPickUp;
    public BaseConsumable()
    {
        SetType(BaseConsumable.consumableType.Unknown);
        Tier = 0;
        //---
        healthBonus = 0;
        damageBonus = 0;
        movementBonus = 0;
        diceBonust = 0;
        diceShard = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") 
        {
            GiveReward(other.gameObject);
        }
    }
    public virtual void GiveReward(GameObject player) 
    {
        //TargetThatrecived,health,bonusDamage,bonusMovement,bonusDice,DiceShard
        BrodcastPlayerCollectedPickUp?.Invoke(player, healthBonus, damageBonus, movementBonus, diceBonust, diceShard);
        Destroy(this.gameObject,0.5f);
    }
    #region Set
    public void SetType(BaseConsumable.consumableType newType) 
    {
        Type = newType;
    }
    public void SetOwner(Tyle newTyleOwner) 
    {
        tyleOwner = newTyleOwner;
    }
    public void SetTier(int newTier) 
    {
        Tier = newTier;
    }
    #endregion
}
