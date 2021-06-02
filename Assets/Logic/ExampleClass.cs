// Get a rectangular area of a texture and place it into
// a new texture the size of the rectangle.
using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour
{
    // Source texture and the rectangular area we want to extract.
    [SerializeField]public Texture2D sourceTex;
    [SerializeField]public Rect sourceRect;

    void Start()
    {
        int x = Mathf.FloorToInt(sourceRect.x);
        int y = Mathf.FloorToInt(sourceRect.y);
        int width = Mathf.FloorToInt(sourceRect.width);
        int height = Mathf.FloorToInt(sourceRect.height);

        Color[] pix = sourceTex.GetPixels(x, y, width, height);
        Texture2D destTex = new Texture2D(width, height);
        destTex.SetPixels(pix);
        destTex.Apply();

        // Set the current object's texture to show the
        // extracted rectangle.
        GetComponent<Renderer>().material.mainTexture = destTex;
    }
}
