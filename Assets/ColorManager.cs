using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    

    private Renderer tileRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        tileRenderer = GetComponentInChildren<MeshRenderer>();
    }

    

    public void SetColor(Color color)
    {
        GetComponentInChildren<MeshRenderer>()?.material.SetColor("_Color", color);
       
    }

    
}
