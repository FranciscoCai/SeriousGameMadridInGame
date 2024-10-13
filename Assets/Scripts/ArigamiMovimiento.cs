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

    }

    private void Start()
    {

    }
    public Vector2 vectorInicial;
    void POnLooK(Vector2 vector2)
    {
        vectorInicial = vector2;
        if (SelectGameObject != null)
        {
            Vector2 vectorNormal = vector2 * SelectGameObject.transform.forward;
            MovebleObject moveble = SelectGameObject.GetComponent<MovebleObject>();
            if(moveble != null)
            {
                Vector2 CantidadDeGiro = (vectorInicial - vectorNormal);
                moveble.gameObject.transform.Rotate(moveble.VectorRotate * Time.deltaTime*CantidadDeGiro);
                vectorInicial = vectorNormal;
            }
        }
    }
    void EndAttack()
    {
        SelectGameObject = null;
    }
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {

                Vector3 hitPoint = hit.point;
                SelectGameObject = hit.transform.gameObject;
            }
        }

    }
}
