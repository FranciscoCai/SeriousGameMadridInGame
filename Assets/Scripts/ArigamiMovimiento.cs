using System.Collections;
using UnityEngine;

public class ArigamiMovimiento : MonoBehaviour
{


    [SerializeField] private InputActions input;
    [SerializeField] private GameObject SelectGameObject;

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
                moveble.gameObject.transform.Rotate(Time.deltaTime*new Vector3(CantidadDeGiro.y * moveble.VectorRotate.x, CantidadDeGiro.y * moveble.VectorRotate.y, CantidadDeGiro.y * moveble.VectorRotate.z));
                vectorInicial = vectorNormal;
            }
        }
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
