using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PruebaCamara : MonoBehaviour
{
    public Transform targetTransform;
    public Transform cameraPositionOne;
    public GameObject targetGO;

    public bool condicion = false;
    public bool conditionTwo = true;


    public float distance = 5f; //Tiene que ser igual al target distance
    public float zoomSpeed = 5f;
    public float xSpeed = 1000f;
    public float ySpeed = 800f;

    public float yMinLimit = -5f;
    public float yMaxLimit = 80f;

    private float targetDistance = 1.1f; // Distancia del objetivo para hacer zoom  
    private float x = 0.0f;
    private float y = 0.0f;

    private void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;


        Quaternion rotation = Quaternion.Euler(y, x, 0);


        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + targetTransform.position;

        transform.position = position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (targetGO == GetClickedObject(out RaycastHit hit))
            {
                StartCoroutine(CloseCamera(cameraPositionOne.position,cameraPositionOne.rotation));
                //transform.position = cameraPositionOne.position;

                condicion = true;

                // targetDistance = 0.7f; // distancia zoom cuanto menos, más cerca

            }

        }

        if (Input.GetMouseButton(1))
        {
            //targetDistance = 1.1f; // distancia sin zoom  
            x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;


            y = ClampAngle(y, yMinLimit, yMaxLimit);



            condicion = false;
            
        }


        if (condicion == false)
        {
            Debug.Log("Guarra");

            distance = Mathf.Lerp(distance, targetDistance, Time.deltaTime * zoomSpeed);


            Quaternion rotation = Quaternion.Euler(y, x, 0);


            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + targetTransform.position;


            transform.rotation = rotation;
            transform.position = position;
        }
        
       /* if(condicion == false && conditionTwo == false)
        {
            Debug.Log("No, guarra t?);

            // Rotación de la cámara u objeto
            Quaternion rotationTwo = Quaternion.Euler(y, x, 0);

            // Calcula la posición objetivo de la cámara
            Vector3 targetPositionTwo = rotationTwo * new Vector3(0.0f, 0.0f, -distance) + targetTransform.position;

            // Interpolación suave de la posición hacia el objetivo
            Vector3 positionTwo = Vector3.Lerp(transform.position, targetPositionTwo, Time.deltaTime * zoomSpeed);

            // Aplica la rotación y la nueva posición
            transform.rotation = rotationTwo;
            transform.position = positionTwo;

            conditionTwo = true;
        }*/
    }
    private IEnumerator CloseCamera(Vector3 finalPosition,Quaternion finalRotation)
    {
 
        // Ciclo para interpolar la posición y rotación
        while (finalPosition != gameObject.transform.position && finalRotation != gameObject.transform.rotation)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, finalPosition, 0.12f);
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, finalRotation, 0.12f);
            yield return null;
        }
       
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
