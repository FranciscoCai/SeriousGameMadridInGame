using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PruebaClck : MonoBehaviour
{
    public GameObject cube;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (cube == GetClickedObject(out RaycastHit hit))
            {
                print("clicked/touched!");
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            print("Mouse is off!");
        }
    }

    GameObject GetClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            if (!isPointerOverUIObject()) { target = hit.collider.gameObject; }
        }
        return target;
    }
    private bool isPointerOverUIObject()
    {
        PointerEventData ped = new PointerEventData(EventSystem.current);
        ped.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(ped, results);
        return results.Count > 0;
    }
}
