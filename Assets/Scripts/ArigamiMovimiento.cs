using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class ArigamiMovimiento : MonoBehaviour
{


    [SerializeField] private InputActions input;
    [SerializeField] private GameObject SelectGameObject;
    [SerializeField] private GameObject cam;
    public bool MovingCam = false;

    void Awake()
    {
        input.EnableGameplayInputs();
    }

    // Update is called once per frame
    void OnEnable()
    {
        input.onLook += POnLooK;
        input.onStopAttack += EndAttack;
    }
    private void OnDisable()
    {
        input.onLook -= POnLooK;
        input.onStopAttack -= EndAttack;
    }
    public Vector2 vectorInicial;
    public float MForce;
    public float ForceEfect;
    void POnLooK(Vector2 vector2)
    {
        vectorInicial = vector2;
        if (SelectGameObject != null)
        {
            MovebleObject moveble = SelectGameObject.GetComponent<MovebleObject>();
            if (moveble != null)
            {
                Vector2 vectorNormal = vector2 * moveble.upp;
                Vector3 CantidadDeGiro = (vectorNormal - vectorInicial)*10;

                if(CantidadDeGiro.y < -MForce)
                {
                    CantidadDeGiro.y = -ForceEfect;
                }
                if (CantidadDeGiro.y > -5)
                {
                    CantidadDeGiro.y = 5;
                }
                moveble.gameObject.transform.Rotate(Time.deltaTime*new Vector3(CantidadDeGiro.y * moveble.VectorRotate.x, CantidadDeGiro.y * moveble.VectorRotate.y, CantidadDeGiro.y * moveble.VectorRotate.z));
                vectorInicial = vectorNormal;
            }
        }
    }
    private IEnumerator CloseCamera(Vector3 finalPosition, Quaternion finalRotation)
    {
        MovingCam = true;
        while (finalPosition != gameObject.transform.position && finalRotation != gameObject.transform.rotation)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, finalPosition, 0.08f/Vector3.Distance(cam.transform.position, finalPosition));
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, finalRotation, 0.08f / Vector3.Distance(cam.transform.position, finalPosition));
            yield return null;
        }
        MovingCam = false;
    }
    public float[] MrandomForce;
    private IEnumerator RandomForce()
    {
        while (SelectGameObject != null)
        {
            MovebleObject moveble = SelectGameObject.GetComponent<MovebleObject>();
            if (moveble != null)
            {
                float randomForce;
                randomForce = Random.Range(MrandomForce[0], MrandomForce[1]);
                moveble.gameObject.transform.Rotate(Time.deltaTime * new Vector3(randomForce * moveble.VectorRotate.x, randomForce * moveble.VectorRotate.y, randomForce * moveble.VectorRotate.z));
            }
                yield return null;
        }
    }
    void EndAttack()
    {
        SelectGameObject = null;
        StopCoroutine(RandomForce());
    }
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                SelectGameObject = hit.transform.gameObject;
               
                MovebleObject moveble = SelectGameObject.GetComponent<MovebleObject>();
                if (moveble != null)
                {
                    StartCoroutine(RandomForce());
                }
                }
        }

    }
}
