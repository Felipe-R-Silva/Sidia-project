using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DialogWindow : MonoBehaviour
{
    public void DisableDialogWindow() 
    {
        this.gameObject.SetActive(false);
    }
}
