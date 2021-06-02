using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MenuCharacterSelection : MonoBehaviour
{
    [SerializeField] bool isplayer1;
    //--
    [SerializeField] private Cephalon cephalon;
    //---
    [SerializeField] private Player.CharacterType character = Player.CharacterType.Basic;
    [SerializeField] private GameObject Gur;
    [SerializeField] private GameObject Vex;
    //---
    [SerializeField] private TMP_InputField UIplayerName;
    [SerializeField] private Image UIplayerColor;
    //--
    [SerializeField] private bool Confirmed = false;
    [SerializeField] private MenuCharacterSelection OtherPlayerUI;
    [SerializeField] private GameObject StartGameButton;

    public void SwapCharacter() 
    {
        if (Gur.activeSelf) 
        {
            Vex.SetActive(true);
            Gur.SetActive(false);
            character = Player.CharacterType.Runner;//Vex
            return;
        }
        Vex.SetActive(false);
        Gur.SetActive(true);
        character = Player.CharacterType.Basic;//GUR
    }

    public void ConfirmPlayerInformation() 
    {
        int playernumber = 1;//player2
        if (isplayer1) { playernumber = 0; }//player 1
        if (UIplayerName.text == null) 
        {
            UIplayerName.text = $"Player{playernumber}";
        }
        cephalon.PlayerGameChoice[playernumber] = new PlayerData(character, UIplayerName.text, UIplayerColor.color);
        Confirmed = true;
        if (Confirmed && OtherPlayerUI.GetHaveConfirmed()) 
        {
            StartGameButton.SetActive(true);
        }
    }
    public void changeColor() 
    {
        Color newRandomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
        UIplayerColor.color = newRandomColor;
    }

    public bool GetHaveConfirmed() 
    {
        return Confirmed;
    }
}
