using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tyle : MonoBehaviour
{
    public enum TyleZones { Unknown,Hospital,ChargeLab,Armmory,Cassino,Wall,Empty};
    [SerializeField] private Vector2 location;
    [SerializeField] private TyleZones Type;
    [SerializeField] private GameObject consumableSpawnPoint;
    [SerializeField] private GameObject consumable;
    [SerializeField] private GameObject TyleCollorDebugger;

    [SerializeField] private bool HasAPlayerOnIt;
    [SerializeField] private GameObject CurrentPlayerOnTyle;
    Tyle(TyleZones InitType) 
    {
        Type = InitType;
    }
    Tyle()
    {
        Type = TyleZones.Unknown;
    }

    private void OnEnable()
    {
        Cephalon.BrodcastEndOfMapCreation += ReactToEndOfBoardCreation;
    }
    private void ReactToEndOfBoardCreation(Cephalon cephalon) 
    {
        if(Type == TyleZones.Armmory) 
        {
            int consumableTier = Random.Range(0, cephalon.allConsumables.AllConsumableDamagePrefab.Length);
            GameObject ConsumableSelected = cephalon.allConsumables.AllConsumableDamagePrefab[consumableTier];
            consumable = Instantiate(ConsumableSelected, consumableSpawnPoint.transform.position, consumableSpawnPoint.transform.rotation);
            consumable.name = $"DamageBoos Tier{consumableTier}";
            consumable.GetComponent<DamagePack>().SetOwner(this);
            consumable.GetComponent<DamagePack>().SetTier(consumableTier);
        }
        if (Type == TyleZones.Cassino)
        {
            int consumableTier = Random.Range(0, cephalon.allConsumables.AllConsumableDicePrefab.Length);
            GameObject ConsumableSelected = cephalon.allConsumables.AllConsumableDicePrefab[consumableTier];
            consumable = Instantiate(ConsumableSelected, consumableSpawnPoint.transform.position, consumableSpawnPoint.transform.rotation);
            consumable.name = $"DamageBoos Tier{consumableTier}";
            consumable.GetComponent<DicePack>().SetOwner(this);
            consumable.GetComponent<DicePack>().SetTier(consumableTier);
        }
        if (Type == TyleZones.ChargeLab)
        {
            int consumableTier = Random.Range(0, cephalon.allConsumables.AllConsumableMovementPrefab.Length);
            GameObject ConsumableSelected = cephalon.allConsumables.AllConsumableMovementPrefab[consumableTier];
            consumable = Instantiate(ConsumableSelected, consumableSpawnPoint.transform.position, consumableSpawnPoint.transform.rotation);
            consumable.name = $"DamageBoos Tier{consumableTier}";
            consumable.GetComponent<MovementPack>().SetOwner(this);
            consumable.GetComponent<MovementPack>().SetTier(consumableTier);
        }
        if (Type == TyleZones.Hospital)
        {
            int consumableTier = Random.Range(0, cephalon.allConsumables.AllConsumableHealthPrefab.Length);
            GameObject ConsumableSelected = cephalon.allConsumables.AllConsumableHealthPrefab[consumableTier];
            consumable = Instantiate(ConsumableSelected, consumableSpawnPoint.transform.position, consumableSpawnPoint.transform.rotation);
            consumable.name = $"DamageBoos Tier{consumableTier}";
            consumable.GetComponent<HealthPack>().SetOwner(this);
            consumable.GetComponent<HealthPack>().SetTier(consumableTier);
        }
    }
    #region SET
    public void SetGroundTexture(Material newMaterial) 
    {
        TyleCollorDebugger.GetComponent<Renderer>().material = newMaterial;
    }
    public void SetLocation(Vector2 newLocation) 
    {
        location = newLocation;
    }
    public void SetType(TyleZones newType)
    {
        Type = newType;
    }
    public void SetTypeByColor(Color color)
    {
        //BLUE -speed
        if (color.r < 0.2 && color.g < 0.2 && color.b > 0.6f)
        {
            //Debug.Log(color + "BLUE");
            Type = TyleZones.ChargeLab;
            TyleCollorDebugger.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.blue);
            return;
        }
        //BLACK -unknown
        else if (color.r < 0.2 && color.g < 0.2 && color.b < 0.2f)
        {
            //Debug.Log(color + "BLACK");
            Type = TyleZones.Unknown;
            TyleCollorDebugger.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.black);
            return;
        }
        //CLEARPINK -heal
        if (color.r > 0.8 && color.g < 0.4 && color.b > 0.8f)
        {
            //Debug.Log(color + "PINK");
            Type = TyleZones.Hospital;
            Color pink = new Color(0.9f, 0.3f, 0.9f, 1f);
            TyleCollorDebugger.GetComponent<Renderer>().material.SetColor("_BaseColor", pink);
            return;
        }
        //RED -Atack
        if (color.r > 0.8 && color.g < 0.2 && color.b < 0.2f)
        {
            //Debug.Log(color + "RED");
            Type = TyleZones.Armmory;
            TyleCollorDebugger.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
            return;
        }
        //WHITE -Empty
        if (color.r > 0.8 && color.g > 0.8 && color.b > 0.8f)
        {
            //Debug.Log(color + "WHITE");
            Type = TyleZones.Empty;
            TyleCollorDebugger.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.white);
            return;
        }
        //YELLOW -Dice
        if (color.r > 0.8 && color.g > 0.8 && color.b < 0.2f)
        {
            //Debug.Log(color + "YELLOW");
            Type = TyleZones.Cassino;
            TyleCollorDebugger.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.yellow);
            return;
        }
        //GRAY -wall
        if (color.r > 0.4 && color.g > 0.4 && color.b < 0.6f)
        {
            Type = TyleZones.Wall;
            TyleCollorDebugger.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.gray);
            //Debug.Log(color + "");
        }

    }
    public void SetPlayerOnThisTyle(GameObject player)
    {
        CurrentPlayerOnTyle = player;
        player.GetComponent<Player>().SetLocation(Getlocation());
        HasAPlayerOnIt = true;
    }
    public void RemovePlayerOThisTyle()
    {
        CurrentPlayerOnTyle = null;
        HasAPlayerOnIt = false;
    }
    #endregion
    #region GET
    public GameObject GetGroundBlock() 
    {
        return TyleCollorDebugger;
    }
    public Vector2 Getlocation() 
    {
        return location;
    }
    public TyleZones GetTyleType() 
    {
        return Type;
    }
    public bool IsthisTyleOcupied()
    {
        return HasAPlayerOnIt;
    }
    #endregion
}