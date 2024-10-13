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
            MovebleObject moveble = SelectGameObject.GetComponent<MovebleObject>();
            if (moveble != null)
            {
                Vector2 vectorNormal = vector2 * moveble.upp;
                Vector3 CantidadDeGiro = vectorNormal - vectorInicial;
                moveble.gameObject.transform.Rotate(Time.deltaTime*new Vector3(CantidadDeGiro.y * moveble.VectorRotate.x, CantidadDeGiro.y * moveble.VectorRotate.y, CantidadDeGiro.y * moveble.VectorRotate.z));
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
