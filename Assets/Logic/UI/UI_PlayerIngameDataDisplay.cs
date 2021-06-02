using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_PlayerIngameDataDisplay : MonoBehaviour
{
    [SerializeField] bool isplayer1UI = false;
    [SerializeField] GameObject InGameGameObject;
    [SerializeField] private int thisplayernumber = 0;
    [SerializeField] private Cephalon cephalon;
    [SerializeField] private AdministratorCephalon cephalonAdministrator;
    //---
    [SerializeField] private Image[] framesthathavePlayerColor;
    //---
    [SerializeField] private GameObject GAR_playerCharacterImage;
    [SerializeField] private GameObject VEX_playerCharacterImage;
    //---
    [SerializeField] private Image healthbar;
    //---
    [SerializeField] private TMP_Text playername;
    //---
    [SerializeField] private TMP_Text Damage;
    [SerializeField] private GameObject WindowBonusDamage;
    [SerializeField] private TMP_Text DamageBonus;
    //---
    [SerializeField] private TMP_Text Movement;
    [SerializeField] private GameObject WindowBonusMovement;
    [SerializeField] private TMP_Text MovementBonus;
    //---
    [SerializeField] private TMP_Text Dice;
    [SerializeField] private GameObject WindowBonusDice;
    [SerializeField] private TMP_Text DiceFragmentBonus;
    [SerializeField] private TMP_Text DiceBonus;
    void Start()
    {
        if (!isplayer1UI) { thisplayernumber = 1; }
        cephalon = GameObject.Find("Cephalon").GetComponent<Cephalon>();
        cephalonAdministrator = cephalon.GetAdministrator();

        InitializeAllData();

        AdministratorCephalon.BrodcastGameHasStarted += ReactToGameStart;
        Player.BrodcastIPlayerHaveResetMyInfo += ReactToInfoReset;
        Player.BrodcastIPlayerHaveUpdatedMyInfo += ReactToInfoUpdate;
    }
    private void ReactToInfoUpdate(GameObject player) 
    {
        if (InGameGameObject == player)
        {
            if (cephalon.PlayerGameChoice[thisplayernumber].GetPlayertype() == Player.CharacterType.Basic)
            {
                UpdateAllInfoForNewTurn(InGameGameObject.GetComponent<Basic>());
            }
            if (cephalon.PlayerGameChoice[thisplayernumber].GetPlayertype() == Player.CharacterType.Runner)
            {
                UpdateAllInfoForNewTurn(InGameGameObject.GetComponent<Runner>());
            }
        }
    }
    private void ReactToInfoReset(GameObject player) 
    {
        if (InGameGameObject == player)
        {
            if (cephalon.PlayerGameChoice[thisplayernumber].GetPlayertype() == Player.CharacterType.Basic)
            {
                ResetAllInfoForNewTurn(InGameGameObject.GetComponent<Basic>());
            }
            if (cephalon.PlayerGameChoice[thisplayernumber].GetPlayertype() == Player.CharacterType.Runner)
            {
                ResetAllInfoForNewTurn(InGameGameObject.GetComponent<Runner>());
            }
        }
    }
    private void ResetAllInfoForNewTurn(Basic Player)
    {
        Damage.text = Player.GetDamage().ToString();
        DamageBonus.text = "" + 0;
        WindowBonusDamage.SetActive(false);
        Movement.text = Player.GetMovement().ToString();
        MovementBonus.text = "" + 0;
        WindowBonusMovement.SetActive(false);
        Dice.text = "" + 0;
        DiceBonus.text = Player.GetDiceBonus().ToString();
        if (Player.GetDiceShards() <= 0)
        {
            WindowBonusDice.SetActive(false);
        }
    }
    private void ResetAllInfoForNewTurn(Runner Player)
    {
        Damage.text = Player.GetDamage().ToString();
        DamageBonus.text = "" + 0;
        WindowBonusDamage.SetActive(false);
        Movement.text = Player.GetMovement().ToString();
        MovementBonus.text = "" + 0;
        WindowBonusMovement.SetActive(false);
        Dice.text = ""+0;
        DiceBonus.text = Player.GetDiceBonus().ToString();
        if (Player.GetDiceShards() <= 0)
        {
            WindowBonusDice.SetActive(false);
        }
    }
    private void UpdateAllInfoForNewTurn(Basic Player) 
    {
        //Update Damage
        Damage.text = Player.GetCurrentDamage().ToString();
        //Update DamageBonus
        DamageBonus.text = Player.GetDamageBonus().ToString();
        if (Player.GetDamageBonus() <= 0) 
        {
            WindowBonusDamage.SetActive(false);
        }
        else 
        {
            WindowBonusDamage.SetActive(true);
        }
        //Update movement
        Movement.text = Player.GetCurrentMovement().ToString();
        //Update movementBonus
        MovementBonus.text = Player.GetMovementBonus().ToString();
        if (Player.GetMovementBonus() <= 0)
        {
            WindowBonusMovement.SetActive(false);
        }
        else
        {
            WindowBonusMovement.SetActive(true);
        }
        //Update dice
        Dice.text = Player.GetCurrentDice().ToString();
        //Update diceBonus
        DiceBonus.text = Player.GetDiceBonus().ToString();
        //Update diceFragment
        DiceFragmentBonus.text = Player.GetDiceShards().ToString();
        if (Player.GetDiceBonus() > 0)
        {
            WindowBonusDice.SetActive(true);
        }
        if(Player.GetDiceShards() > 0)
        {
            WindowBonusDice.SetActive(true);
        }
    }
    private void UpdateAllInfoForNewTurn(Runner Player)
    {
        //Update Damage
        Damage.text = Player.GetCurrentDamage().ToString();
        //Update DamageBonus
        DamageBonus.text = Player.GetDamageBonus().ToString();
        if (Player.GetDamageBonus() <= 0)
        {
            WindowBonusDamage.SetActive(false);
        }
        else
        {
            WindowBonusDamage.SetActive(true);
        }
        //Update movement
        Movement.text = Player.GetCurrentMovement().ToString();
        //Update movementBonus
        MovementBonus.text = Player.GetMovementBonus().ToString();
        if (Player.GetMovementBonus() <= 0)
        {
            WindowBonusMovement.SetActive(false);
        }
        else
        {
            WindowBonusMovement.SetActive(true);
        }
        //Update dice
        Dice.text = Player.GetCurrentDice().ToString();
        //Update diceBonus
        DiceBonus.text = Player.GetDiceBonus().ToString();
        //Update diceFragment
        DiceFragmentBonus.text = Player.GetDiceShards().ToString();
        if (Player.GetDiceBonus() > 0)
        {
            WindowBonusDice.SetActive(true);
        }
        if (Player.GetDiceShards() > 0)
        {
            WindowBonusDice.SetActive(true);
        }
    }
    private void ReactToGameStart(GameObject player1, GameObject player2) 
    {
        if (isplayer1UI) 
        {
            InGameGameObject = player1;
            return;
        }
        InGameGameObject = player2;
    }
    public void InitializeAllData() 
    {
        initializeAllFramesColorAndName();
        initializePlayerImage();
        initializeBaseDamageMovementDice();
    }
    public void initializeAllFramesColorAndName() 
    {
        Color colorPlayerchose = cephalon.PlayerGameChoice[thisplayernumber].GetColor();
        playername.text = cephalon.PlayerGameChoice[thisplayernumber].GetPlayerName();
        foreach (Image item in framesthathavePlayerColor)
        {
            item.color = colorPlayerchose;
        }
    }
    public void initializePlayerImage() 
    {
        Player.CharacterType characterPlayerSelected = cephalon.PlayerGameChoice[thisplayernumber].GetPlayertype();
        if(characterPlayerSelected == Player.CharacterType.Basic) 
        {
            GAR_playerCharacterImage.SetActive(true);
            VEX_playerCharacterImage.SetActive(false);
        }
        else 
        {
            GAR_playerCharacterImage.SetActive(false);
            VEX_playerCharacterImage.SetActive(true);
        }

    }
    
    public void initializeBaseDamageMovementDice() 
    {
        List<Player> characterOptions = cephalonAdministrator.GetplayerPrefabs();
        Player.CharacterType characterPlayerSelected = cephalon.PlayerGameChoice[thisplayernumber].GetPlayertype();
        if (characterPlayerSelected == Player.CharacterType.Basic)//GAR
        {
            Damage.text = (characterOptions[0].GetComponent<Player>() as Basic).GetDamage().ToString();
            Movement.text = (characterOptions[0].GetComponent<Player>() as Basic).GetMovement().ToString();
            Dice.text = (characterOptions[0].GetComponent<Player>() as Basic).GetDice().ToString();
        }
        else//VEX
        {
            Damage.text = (characterOptions[1].GetComponent<Player>() as Runner).GetDamage().ToString();
            Movement.text = (characterOptions[1].GetComponent<Player>() as Runner).GetMovement().ToString();
            Dice.text = (characterOptions[1].GetComponent<Player>() as Runner).GetDice().ToString();

        }
    }
}
