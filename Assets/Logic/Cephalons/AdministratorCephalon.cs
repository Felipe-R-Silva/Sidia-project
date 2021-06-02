using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class AdministratorCephalon : MonoBehaviour
{
    //--
    [SerializeField] public GameObject DebugPREFAB;
    //--
    [SerializeField] private bool HaveAlreadyInitializedPlayers = false;
    [SerializeField] private bool RequsetedToplacePlayer1 = false;
    [SerializeField] private bool RequsetedToplacePlayer2 = false;
    //--
    [SerializeField] private List<Player> playerPrefabs;
    [SerializeField] private List<GameObject> Ingameplayers = new List<GameObject>();
    //---
    [SerializeField] private GameObject dialogWindow;
    [SerializeField] private TMP_Text textDialog;
    [SerializeField] private AdministratorDialogs CephalonAdministratorDialogs;
    //--
    [SerializeField] private AudioSource VoiceOutput;
    [SerializeField] private AudioClip WakeUpsound;
    [SerializeField] private AudioClip Anoucementsound;
    //--
    [SerializeField] private Cephalon cephalon;
    [SerializeField] private CommandGiver commandGiver;
    //---
    //---GameTurnsLogic
    //---
    public bool GameHasStarted = false;
    public int NumberOfPlayersThatHaveToPlayedThisRound = 2;
    [SerializeField] int IndexCurrentPlayerTurn;

    public static event Action<GameObject> BrodcastPlayerThatShouldPlay;
    public static event Action<GameObject,GameObject> BrodcastGameHasStarted;
    public static event Action BrodcastTurnHasEnded;

    private void Awake()
    {
        cephalon = this.transform.root.GetComponent<Cephalon>();
        commandGiver = cephalon.GetComandGiver();
        SplaySound(WakeUpsound, VoiceOutput);//WakeUp noise
        Say(CephalonAdministratorDialogs.D_GameStartDialog);
        CommandGiver.ClickedOnSomething += AdministratorReactionToClick;
        Player.BrodcastIHaveNoMoreMovements += ReactToPlayerNotHavingMovement;

    }
    public void ReactToPlayerNotHavingMovement(GameObject player) 
    {
        IndexCurrentPlayerTurn=(IndexCurrentPlayerTurn + 1) % 2;
        Debug.Log("Index of currentPlayerIs: "+ IndexCurrentPlayerTurn);
        NumberOfPlayersThatHaveToPlayedThisRound--;
        if (NumberOfPlayersThatHaveToPlayedThisRound == 0) 
        {
            BrodcastTurnHasEnded?.Invoke();
            NumberOfPlayersThatHaveToPlayedThisRound = 2;
        }
        BrodcastPlayerThatShouldPlay?.Invoke(Ingameplayers[IndexCurrentPlayerTurn]);
    }
    public void AdministratorReactionToClick(GameObject target)
    {
        if (target.tag == "CephalonAdminUI") { dialogWindow.SetActive(false); }
        if (!HaveAlreadyInitializedPlayers)
        {
            GameHasStarted = CephalonTheAdministratorInitializer(target);
            if (HaveAlreadyInitializedPlayers) { return;}
        }
        if (!GameHasStarted) { Debug.Log("Game Has Not Started Yet"); return; }//game has not started yet

    }
    public bool CephalonTheAdministratorInitializer(GameObject target)
    {

        if (Ingameplayers.Count <= 0)
        {
            if (!RequsetedToplacePlayer1)
            {
                string dialog = $"{cephalon.PlayerGameChoice[0].GetPlayerName()} Chose were you want to Start";
                Say(dialog);
                RequsetedToplacePlayer1 = true;
            }
            if (target.TryGetComponent<Tyle>(out Tyle targetComponent) && !targetComponent.IsthisTyleOcupied())
            {
                if (targetComponent.GetTyleType() == Tyle.TyleZones.Empty)
                {
                    if(cephalon.PlayerGameChoice[0].GetPlayertype() == Player.CharacterType.Basic) 
                    {
                        GameObject player1 = Instantiate(playerPrefabs[0].gameObject, targetComponent.transform.position, Quaternion.identity);
                        Ingameplayers.Add(player1);
                        targetComponent.SetPlayerOnThisTyle(player1);
                    }
                    else 
                    {
                        GameObject player1 = Instantiate(playerPrefabs[1].gameObject, targetComponent.transform.position, Quaternion.identity);
                        Ingameplayers.Add(player1);
                        targetComponent.SetPlayerOnThisTyle(player1);
                    }

                    if (RequsetedToplacePlayer1 && !RequsetedToplacePlayer2)
                    {
                        string dialog = $"Nice,YourTurn {cephalon.PlayerGameChoice[1].GetPlayerName()} pick a place";
                        Say(dialog);
                        RequsetedToplacePlayer2 = true;
                    }
                }
            }
        }
        if (Ingameplayers.Count <= 1)
        {
            if (target.TryGetComponent<Tyle>(out Tyle targetComponent) && !targetComponent.IsthisTyleOcupied())
            {
                if (targetComponent.GetTyleType() == Tyle.TyleZones.Empty)
                {
                    if (cephalon.PlayerGameChoice[1].GetPlayertype() == Player.CharacterType.Basic)
                    {
                        GameObject player2 = Instantiate(playerPrefabs[0].gameObject, targetComponent.transform.position, Quaternion.identity);
                        Ingameplayers.Add(player2);
                        targetComponent.SetPlayerOnThisTyle(player2);
                    }
                    else
                    {
                        GameObject player2 = Instantiate(playerPrefabs[1].gameObject, targetComponent.transform.position, Quaternion.identity);
                        Ingameplayers.Add(player2);
                        targetComponent.SetPlayerOnThisTyle(player2);
                    }
                }
            }
        }
        if(Ingameplayers.Count == 2) 
        {
            int chosewhostarts = UnityEngine.Random.Range(0, 2);
            IndexCurrentPlayerTurn = chosewhostarts;
            BrodcastPlayerThatShouldPlay?.Invoke(Ingameplayers[IndexCurrentPlayerTurn]);//brodcastplayerTurn
            Say($"Thats It You Guys are ready for the game to Start. I administrator Have decided that {cephalon.PlayerGameChoice[chosewhostarts].GetPlayerName()} Plays First. Good Luck!");
            HaveAlreadyInitializedPlayers = true;
            BrodcastGameHasStarted?.Invoke(Ingameplayers[0], Ingameplayers[1]);
            return true;
        }
        return false;
    }
    public void Say(string message) 
    {
        dialogWindow.SetActive(true);
        textDialog.text = message;
    }
    public void SplaySound(AudioClip audio, AudioSource microphone)
    {
        microphone.clip = audio;
        microphone.Play();
    }
    public List<Player> GetplayerPrefabs() 
    {
        return playerPrefabs;
    }

}
