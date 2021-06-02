using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
[System.Serializable]
public class Player : MonoBehaviour
{
   public enum CharacterType { Unknown,Runner,Basic};



    [SerializeField] protected private CharacterType myType = CharacterType.Unknown;
    //---------
    [SerializeField] private Vector2 location = new Vector2(-1,-1);
    //---------
    [SerializeField] protected private int Maxhealth;
    [SerializeField] protected private int health;
    //---------
    [SerializeField] protected private int MAXdamage;
    [SerializeField] protected private int damage;
    [SerializeField] protected private int damageBonus;
    //---------
    [SerializeField] protected private int MAXmovement;
    [SerializeField] protected private int movement;
    [SerializeField] protected private int movementBonus;
    //----
    [SerializeField] protected private int MAXdice;
    [SerializeField] protected private int dice;
    [SerializeField] protected private int diceBonus;
    [SerializeField] protected private int diceShards;

    public static event Action<GameObject> BrodcastIHaveNoMoreMovements;
    public static event Action<GameObject> BrodcastIPlayerHaveResetMyInfo;
    public static event Action<GameObject> BrodcastIPlayerHaveUpdatedMyInfo;
    public Player() 
    {
        Maxhealth = 100;
        health = 100;
        MAXdamage = 30;
        damage = 30;
        damageBonus = 0;
        MAXmovement = 3;
        movement = 3;
        movementBonus = 0;
        SetType(Player.CharacterType.Unknown);
        MAXdice = 3;
        dice = 3;
        diceBonus = 0;
        diceShards = 0;
    }
    private void OnEnable()
    {
        UnitMove.PlayerEnteredTyle += ReactToEnteringTyle;
        AdministratorCephalon.BrodcastTurnHasEnded += ReactToTurnEnd;
        BaseConsumable.BrodcastPlayerCollectedPickUp += ReactToPlayerCollectingPickUp;
    }
    public void ReactToPlayerCollectingPickUp(GameObject player,int P_healthBonus,int P_damageBonus,int P_movementBonus, int P_diceBonust, int P_diceShard) 
    {
        if (this.gameObject != player) { return; }
        //AdjustHealth
        if(P_healthBonus>0 && GetCurrentHealth() < GetHealth()) 
        {
            if((GetCurrentHealth()+ P_healthBonus)>= GetHealth()) 
            {
                SetHealth(GetHealth());
            }
            SetHealth(GetHealth()+ P_healthBonus);
        }
        //SetDamageBonus
        if (P_damageBonus > 0)
        {
            SetDamBonus(GetDamageBonus() + P_damageBonus);
        }
        //SetMoveBonus
        if (P_movementBonus > 0)
        {
            SetMovBonus(GetMovementBonus() + P_movementBonus);
        }
        //SetDiceShardBonus
        if (P_diceShard > 0)
        {
            SetDiceShardsBonus(GetDiceShards() + P_diceBonust);
        }
        //SetDiceBonus
        if (P_diceBonust > 0)
        {
            SetDiceBonus(GetDiceBonus() + P_diceBonust);
        }
        //brodcast
        BrodcastIPlayerHaveUpdatedMyInfo?.Invoke(this.gameObject);
    }
    virtual public void ReactToTurnEnd() 
    {
        ResetForNewTurn();
        BrodcastIPlayerHaveResetMyInfo?.Invoke(this.gameObject);
    }
    private void ReactToEnteringTyle(Vector2 tyleValues, GameObject player)
    {
        if (this.gameObject != player) { return; }
        SetLocation(tyleValues);
        //update movement
        SetMovement(GetCurrentMovement() - 1);
        BrodcastIPlayerHaveUpdatedMyInfo?.Invoke(this.gameObject);
        if (GetCurrentMovement() <= 0)
        {
            BrodcastIHaveNoMoreMovements?.Invoke(this.gameObject);
            return;
        }
    }

    public virtual void ResetForNewTurn() 
    {
        SetDamage(MAXdamage);
        SetDamBonus(0);
        SetMovement(MAXmovement);
        SetMovBonus(0);
        SetDice(MAXdice);
        SetDiceBonus(0);
    }
    #region Get
    public int GetDamage()
    {
        return MAXdamage;
    }
    public int GetCurrentDamage()
    {
        return damage;
    }
    public int GetDamageBonus()
    {
        return damageBonus;
    }
    public int GetHealth()
    {
        return Maxhealth;
    }
    public int GetCurrentHealth()
    {
        return health;
    }
    public int GetMovement() 
    {
        return MAXmovement;
    }
    public int GetMovementBonus()
    {
        return movementBonus;
    }
    public int GetCurrentMovement()
    {
        return movement;
    }
    public int GetDice()
    {
        return MAXdice;
    }
    public int GetCurrentDice()
    {
        return MAXdice;
    }
    public int GetDiceBonus()
    {
        return diceBonus;
    }
    public int GetDiceShards()
    {
        return diceShards;
    }
    public Vector2 GetLocation() 
    {
        return location;
    }
    #endregion
    #region Set
    public void SetLocation(Vector2 newLocation) 
    {
        location = newLocation;
    }
    public void SetType(Player.CharacterType newType) 
    {
        myType = newType;
    }
    public void SetHealth(int newHealth) 
    {
        health = newHealth;
    }
    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }
    public void SetDamBonus(int newDamageBonus)
    {
        damageBonus = newDamageBonus;
    }
    public void SetDice(int newDice)
    {
        dice = newDice;
    }
    public void SetDiceBonus(int newDiceBonus)
    {
        diceBonus = newDiceBonus;
    }
    public void SetDiceShardsBonus(int newDiceShards)
    {
        diceShards = newDiceShards;
    }
    public void SetMovement(int newMovement)
    {
        movement = newMovement;
    }
    public void SetMovBonus(int newMovementBonus)
    {
        movementBonus = newMovementBonus;
    }
    #endregion

}
