using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class renderQueConfig : MonoBehaviour
{
    public int renderQueue = 10;

    private void OnValidate()
    {
        GetComponent<Renderer>().sharedMaterial.renderQueue = renderQueue;
    }
}
