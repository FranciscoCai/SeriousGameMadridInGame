using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PruebaCamara : MonoBehaviour
{
    public Transform targetTransform;
    public GameObject targetGO; 
    public float distance = 5f;
    public float targetDistance = 5f; // Distancia del objetivo para hacer zoom  
    public float zoomSpeed = 2f; 
    public float xSpeed = 1000f;
    public float ySpeed = 800f;

    public float yMinLimit = -5f;
    public float yMaxLimit = 80f;

    // Almacenan los valores de rotaci�n. Luego se les pasar� a la c�mara para que haga cosas guays :) (Girar).
    private float x = 0.0f;
    private float y = 0.0f;

    private void Start()
    {
        // Recoge el �ngulo de rotaci�n incial de la c�mara y se lo pasa a los floats "x" e "y".  
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (targetGO == GetClickedObject(out RaycastHit hit))
            {
                targetDistance = 2f;
            }
            
        }

        if (Input.GetMouseButton(1))
        {
            targetDistance = 5f;   
            x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

            // Limita el �ngulo Y dentro de los l�mites definidos para que no atraviese la mesa.  
            y = ClampAngle(y, yMinLimit, yMaxLimit);
        }

        // Pa q el zoom sea smooth.
        distance = Mathf.Lerp(distance, targetDistance, Time.deltaTime * zoomSpeed);

        // Calcula la rotaci�n en funci�n a los �ngulos x e y.
        Quaternion rotation = Quaternion.Euler(y, x, 0);

        // Calcula la posici�n y rotaci�n de la c�mara.
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + targetTransform.position;

        // Aplica la rotaci�n y posici�n a la c�mara.
        transform.rotation = rotation;
        transform.position = position;
    }

    // Hace el coso pa que no atraviese la mesa (q sino queda feo y tal).
    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F) angle += 360F;
        if (angle > 360F) angle -= 360F;
        return Mathf.Clamp(angle, min, max);
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
