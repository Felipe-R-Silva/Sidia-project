using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class Cephalon : MonoBehaviour
{
    Dictionary<string, GameObject> OrganizingGroupsSet = new Dictionary<string, GameObject>();
    //---
    [SerializeField] public PlayerData[] PlayerGameChoice = new PlayerData[2];
    [SerializeField] private CommandGiver commandGiver;
    [SerializeField] public AllConsumablesInGame allConsumables;
    //--
    [SerializeField] private string gameSceneName = "GameScene"; //basescene = "_DDOL_preload"
    //------ World
    [SerializeField] private Tyle tylePrefab;
    [SerializeField] private Vector2 boardSize;
    [SerializeField] private Vector2 boardSpacing;
    [SerializeField] public Tyle[,] boardMatrix =null;
    //--Maps
    public Texture2D LevelsourceTex;
    public Rect LevelsourceRect;
    //--Parallel Mind
    [SerializeField] private AdministratorCephalon Administrator;
    //---
    public static event Action<Cephalon> BrodcastEndOfMapCreation;

    private void OnEnable()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        SceneManager.sceneLoaded += OnSceneLoaded;

        UnitMove.PlayerEnteredTyle += ReactToPlayerEnteringTyle;
        UnitMove.PlayerLeftTyle += ReactTOPlayerLeavingTyle;
        DontDestroyOnLoad(this.gameObject);
    }
    public void ReactToPlayerEnteringTyle(Vector2 tyleValues, GameObject player) 
    {
        boardMatrix[(int)tyleValues.x, (int)tyleValues.y].SetPlayerOnThisTyle(player);
    }
    public void ReactTOPlayerLeavingTyle(Vector2 LocationPlayerLeft) 
    {
        boardMatrix[(int)LocationPlayerLeft.x, (int)LocationPlayerLeft.y].RemovePlayerOThisTyle();
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name+" mode:"+ mode);

        if (gameSceneName == null) { Debug.LogError("You Have not defined in Cephalon what scene is main scene"); }
        if(scene.name != gameSceneName) { return; }//not game scene
        CreateGameBoard();
        ActivateCefalonAdministrator();
    }
    private void OnSceneUnloaded(Scene scene)
    {
        Debug.Log("OnSceneUnloaded:" + scene.name);
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
        //Do Something here

    }
    private void CreateGameBoard() 
    {
        boardMatrix = new Tyle[(int)boardSize.x, (int)boardSize.y];
        //---
        GameObject Board= new GameObject();
        Board.name = "Board";
        Color[] pix = ConvertImageToArrayOfColors(LevelsourceTex, LevelsourceRect);
        int pixCounter = 0;
        for (int y = 0; y < boardMatrix.GetLength(1); y++)
        {
            for (int x = 0; x < boardMatrix.GetLength(0); x++)
            {
                Color colortocheck = pix[pixCounter];

                boardMatrix[x,y] = Instantiate(tylePrefab,transform.position + new Vector3(x,0,y),Quaternion.identity);
                boardMatrix[x, y].SetLocation(new Vector2(x, y));
                boardMatrix[x, y].SetTypeByColor(colortocheck);//sets the type
                //Makes the GameObject "newParent" the parent of the GameObject "player".
                string thisTyleType = boardMatrix[x, y].GetTyleType().ToString();
                if(boardMatrix[x, y].GetTyleType() == Tyle.TyleZones.Wall) 
                {
                    boardMatrix[x, y].GetGroundBlock().transform.localScale += new Vector3(0, 2, 0);
                }
                if (OrganizingGroupsSet.ContainsKey(thisTyleType)) 
                {
                    boardMatrix[x, y].transform.parent = OrganizingGroupsSet[thisTyleType].gameObject.transform;
                }
                else 
                {
                    GameObject newgamObject = new GameObject();
                    newgamObject.name = thisTyleType;
                    newgamObject.transform.parent = Board.transform;
                    OrganizingGroupsSet.Add(thisTyleType, newgamObject);
                    boardMatrix[x, y].transform.parent = newgamObject.transform;
                }
                boardMatrix[x, y].name = $"{ boardMatrix[x, y].GetTyleType()}({x},{y})";
                SetDetailsOfEachType(boardMatrix[x, y]);
                pixCounter++;
            }
        }
        BrodcastEndOfMapCreation?.Invoke(this);
    }
    public void SetDetailsOfEachType(Tyle tyleToAdjust)
    {  //Unknown=0,Hospital=1,ChargeLab=2,Armmory=3,Cassino=4,Wall=5,Empty=6
        tyleToAdjust.SetGroundTexture(allConsumables.AllGroundTyles[Convert.ToInt32(tyleToAdjust.GetTyleType())]);
    }
    public Color[] ConvertImageToArrayOfColors(Texture2D sourceTex, Rect sourceRect)
    {
        int xr = Mathf.FloorToInt(sourceRect.x);
        int yr = Mathf.FloorToInt(sourceRect.y);
        int width = Mathf.FloorToInt(sourceRect.width);
        int height = Mathf.FloorToInt(sourceRect.height);

        return sourceTex.GetPixels(xr, yr, width, height);
    }

    private void ActivateCefalonAdministrator() 
    {
        Administrator.gameObject.SetActive(true);
    }
    public CommandGiver GetComandGiver() 
    {
        return commandGiver;
    }
    public AdministratorCephalon GetAdministrator() 
    {
        return Administrator;
    }
}
