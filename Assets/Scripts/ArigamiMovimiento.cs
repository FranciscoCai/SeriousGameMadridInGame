using UnityEngine;

public class ArigamiMovimiento : MonoBehaviour
{
 

    [SerializeField] private PlayerInput input;


    void Awake()
    {


    }

    // Update is called once per frame
    void OnEnable()
    {

    }
    private void OnDisable()
    {

    }
    private void Start()
    {

    }

    void Update()
    {

            // Crea un rayo desde la c¨¢mara hacia la posici¨®n del mouse en la pantalla
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Lanza el raycast y verifica si golpea alg¨²n objeto en la escena
            if (Physics.Raycast(ray, out hit))
            {
                // Si el raycast golpea algo, imprime el nombre del objeto golpeado
                Debug.Log("Golpe¨®: " + hit.collider.name);

                // Puedes acceder a la posici¨®n del punto de impacto
                Vector3 hitPoint = hit.point;
                Debug.Log("Punto de impacto: " + hitPoint);
                Destroy(hit.transform.gameObject);
            }

    }
}
