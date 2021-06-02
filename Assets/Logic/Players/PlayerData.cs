using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerData
{
    [SerializeField] Player.CharacterType character;
    [SerializeField] string namePlayer;
    [SerializeField] Color color;
    public PlayerData(Player.CharacterType Ntype,string Nname,Color Ncolor) 
    {
        character = Ntype;
        namePlayer = Nname;
        color = Ncolor;
    }
    PlayerData() { }
    public string GetPlayerName() 
    {
        return namePlayer;
    }
    public Player.CharacterType GetPlayertype()
    {
        return character;
    }
    public Color GetColor()
    {
        return color;
    }
}
