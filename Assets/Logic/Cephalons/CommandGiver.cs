using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class CommandGiver : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask = new LayerMask();
    [SerializeField] private LayerMask IgnorelayerMask = new LayerMask();
    [SerializeField] private Camera currentCamera;

    public static event Action<GameObject> ClickedOnSomething;

    private void OnEnable()
    {
        currentCamera = Camera.main;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void Update()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame){return;}
        if (IsMouseOverUIsWithIgnores()) {
             return; 
        }
        Ray ray = currentCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask)){ return;}
        Debug.DrawLine(currentCamera.transform.position, hit.point, Color.red);

        if (hit.collider.TryGetComponent<Tyle>(out Tyle target))//hit a Tyle
        {
            ClickedOnSomething?.Invoke(target.gameObject);
            //Debug.Log($"You clicked in the tyle({target.Getlocation().x},{target.Getlocation().y}) = {target.GetType()}");
        }
        
    }

    private bool IsMouseOverUIsWithIgnores()
    {
        PointerEventData pointEventData = new PointerEventData(EventSystem.current);
        pointEventData.position = Input.mousePosition;
        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointEventData, raycastResultList);
        for (int i = 0; i < raycastResultList.Count; i++)
        {
            if (IgnorelayerMask == (IgnorelayerMask | (1 << raycastResultList[i].gameObject.layer)))
            {
                ClickedOnSomething?.Invoke(raycastResultList[i].gameObject);
                return true;
            }
        }
        return false;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       // currentCamera = Camera.current;
    }
    private void OnSceneUnloaded(Scene scene)
    {
      //  currentCamera = Camera.current;
    }

}
