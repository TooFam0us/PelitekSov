using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObjectInteraction : MonoBehaviour
{

    public GameObject obj;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (obj == getClickedObject(out RaycastHit hit))
            {
                Debug.Log("Toimii :)");
            }
        }
    }

    GameObject getClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            //if (!isCursorOverUI())
            //{
             target = hit.collider.gameObject;
            //}
        }
        return target;
    }

    //private bool isCursorOverUI()
    //{
    //    PointerEventData ped = new PointerEventData(EventSystem.current);
    //    ped.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    //    List<RaycastResult> results = new List<RaycastResult>();
    //    EventSystem.current.RaycastAll(ped, results);
    //    return results.Count > 0;
    //}
}
