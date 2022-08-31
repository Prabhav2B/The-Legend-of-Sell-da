using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightDisplay : MonoBehaviour
{
    public GameObject DisplayObject;
    public Material HighlightMaterial;

    private Material OriginalMaterial;
    // Start is called before the first frame update
    void Start()
    {
        OriginalMaterial = DisplayObject.GetComponent<Renderer>().material;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        DisplayObject.GetComponent<Renderer>().material = HighlightMaterial;
    }

    void OnTriggerExit(Collider collider)
    {
        DisplayObject.GetComponent<Renderer>().material = OriginalMaterial;
    }

    //  OriginalMaterial = DisplayObject.GetComponent<Renderer>().material;
}

