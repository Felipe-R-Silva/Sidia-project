using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugSelfDestruckt : MonoBehaviour
{
    private void OnEnable()
    {
        Destroy(this.gameObject, 3);
    }
}
