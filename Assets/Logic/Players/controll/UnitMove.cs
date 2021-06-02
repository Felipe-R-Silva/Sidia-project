using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
public class UnitMove : MonoBehaviour
{
    [SerializeField] bool IamActive = false;
    [SerializeField] GameObject showeretoclick;
    //--
    public static event Action<Vector2> PlayerLeftTyle;
    public static event Action<Vector2,GameObject> PlayerEnteredTyle;
    private void OnEnable()
    {
        AdministratorCephalon.BrodcastPlayerThatShouldPlay += CheckIfIsIsMyTurn;
        Player.BrodcastIHaveNoMoreMovements += ReactToPlayerWithNoMovements;
    }
    private void ReactToPlayerWithNoMovements(GameObject player) 
    {
        Debug.Log(player.name + " Is out of moves");
        if (this.gameObject == player && IamActive)
        {
            showeretoclick.SetActive(false); //stop displaying data
            CommandGiver.ClickedOnSomething -= ReactToCommand;//stop listening for commands
            IamActive = false;
        }
    }
    private void CheckIfIsIsMyTurn(GameObject playerOfThisturn) 
    {
        if(this.gameObject == playerOfThisturn && !IamActive) 
        {
            showeretoclick.SetActive(true);//activate player helper
            CommandGiver.ClickedOnSomething += ReactToCommand;//StartListening to commands
            IamActive = true;
        }
    }
    private void ReactToCommand(GameObject target) 
    {
        //Movelogic
        Vector2 currentplayerLocation = GetComponent<Player>().GetLocation();
        Vector2 locationIamClicking = target.GetComponent<Tyle>().Getlocation();
        Vector2 MovementGap = new Vector2(locationIamClicking.x - currentplayerLocation.x, locationIamClicking.y - currentplayerLocation.y);//target-me
        
        if (locationIamClicking == currentplayerLocation) { Debug.Log("Clicking My Self"); return; }//clicking on myself
        if (target.GetComponent<Tyle>().IsthisTyleOcupied()) { Debug.Log("Ocupied place"); return; }//the place you are trying to move has someone
        if(target.GetComponent<Tyle>().GetTyleType() == Tyle.TyleZones.Wall) { Debug.Log("wall tyle"); return; }
        if (currentplayerLocation.y == locationIamClicking.y)
        {
            if ((currentplayerLocation.x == locationIamClicking.x + 1) || (currentplayerLocation.x == locationIamClicking.x - 1))
            {
                MovePlayer(this.gameObject, target, MovementGap);
            }
        }
        if (currentplayerLocation.x == locationIamClicking.x)
        {
            if ((currentplayerLocation.y == locationIamClicking.y + 1) || (currentplayerLocation.y == locationIamClicking.y - 1))
            {
                MovePlayer(this.gameObject, target, MovementGap);
            }
        }
    }
    public void MovePlayer(GameObject player, GameObject tyle, Vector2 DiferenceOfPossitions)
    {
        Vector2 playerlocation = player.GetComponent<Player>().GetLocation();//quet matrix location

        player.transform.Translate(DiferenceOfPossitions.x, 0, DiferenceOfPossitions.y);//move

        PlayerEnteredTyle?.Invoke(tyle.GetComponent<Tyle>().Getlocation(), player);//inform entering new tyle
        PlayerLeftTyle?.Invoke(playerlocation); //inform leaving old zone
    }

}
