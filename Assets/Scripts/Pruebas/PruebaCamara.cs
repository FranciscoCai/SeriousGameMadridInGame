using UnityEngine;

public class PruebaCamara : MonoBehaviour
{
    public Transform target;
    public float distance = 5f;
    public float targetDistance = 5f; // Distancia del objetivo para hacer zoom  
    public float zoomSpeed = 2f; 
    public float xSpeed = 120f;
    public float ySpeed = 120f;

    public float yMinLimit = -20f;
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
            targetDistance = 2f; 
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
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

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
}
