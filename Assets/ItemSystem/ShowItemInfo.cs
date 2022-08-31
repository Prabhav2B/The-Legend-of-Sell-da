using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowItemInfo : MonoBehaviour
{
    Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Items");

        if (Physics.Raycast(ray, out hit, 120.0f, mask))
        {
            if (hit.transform.gameObject == gameObject && transform.position == originalPosition) // Input.GetMouseButtonDown(0) != true && Input.GetMouseButton(0) != true)
            {
                    // Show info
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
                
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

    }
}
