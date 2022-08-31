using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DragDrop : MonoBehaviour
{
    [SerializeField] LayerMask inputLayerMask;

    Vector3 startPos;
    Vector3 dist;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.eventMask = inputLayerMask;
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    void OnMouseDown()
    {
        startPos = Camera.main.WorldToScreenPoint(transform.position);
        dist = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, startPos.z));
    }

    void OnMouseDrag()
    {
        Vector3 lastPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, startPos.z);
        transform.position = Camera.main.ScreenToWorldPoint(lastPos) + dist;
    }

}
