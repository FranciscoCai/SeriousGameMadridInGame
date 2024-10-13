using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
     enum PlayerMode { Move, Shoot };

    [SerializeField] private PlayerMode playerMode;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float speed;
    float turnSmoothVelocity;
    private Rigidbody rb;

    private LineRenderer laserLine;
    [SerializeField] private Transform laserOrigin;
    private float gunRange = 50f;

    [SerializeField] private PlayerInput input;


    void Awake()
    {
        //P_Input = new InputSystem_Actions();
        rb = GetComponent<Rigidbody>();

        laserLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void OnEnable()
    {
        input.onMove += GetInputMove;
        input.onStopMove += StopMove;
        input.onAttack += StartAttack;
        input.onStopAttack += StopAttack;
    }
    private void OnDisable()
    {
        input.onMove -= GetInputMove;
        input.onStopMove -= StopMove;
        input.onAttack -= StartAttack;
        input.onStopAttack -= StopAttack;
    }
    private void Start()
    {
        input.EnableGameplayInputs();
        playerMode = PlayerMode.Move;
    }
    void GetInputMove(Vector2 moveInput)
    {
        if (playerMode == PlayerMode.Shoot)
        {
            return;
        }
        Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y);
        if (direction.magnitude > 0.1)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothVelocity);
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.linearVelocity = moveDir * speed;
        }
    }
    void StopMove()
    {
        rb.linearVelocity = Vector3.zero;
    }
    private IEnumerator ShootEfect()
    {
        while (playerMode == PlayerMode.Shoot)
        {
            laserLine.SetPosition(0, laserOrigin.position);
            Vector3 rayOrigin = gameObject.transform.position;
            RaycastHit hit;
            if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, 50f))
            {
                laserLine.SetPosition(1, hit.point);
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (gameObject.transform.forward * gunRange));
            }
            Vector3 p = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(p);
            Vector3 direction;
            if (Physics.Raycast(ray, out RaycastHit hitData,100,-1<<7))
            {
                direction = new Vector3(gameObject.transform.position.x - hitData.point.x, 0, gameObject.transform.position.y - hitData.point.z);
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothVelocity);
                transform.rotation = Quaternion.Euler(0, targetAngle, 0);
            }
            else
            {
            }
            yield return null;
        }
    }
    public void StopAttack()
    {
        playerMode = PlayerMode.Move;
        StopCoroutine(ShootEfect());
    }
    public void StartAttack()
    {
        playerMode = PlayerMode.Shoot;
        StartCoroutine(ShootEfect());
    }
}
