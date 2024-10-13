using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Player Input")]
public class PlayerInput : ScriptableObject, InputSystem_Actions.IPlayerOnGameActions
{
    public event UnityAction<Vector2> onMove = delegate { };
    public event UnityAction onStopMove = delegate { };
    public event UnityAction onAttack = delegate { };
    public event UnityAction onStopAttack = delegate { };
    InputSystem_Actions inputActions;

    public void OnAttack(InputAction.CallbackContext context)
    {

    }

    public void OnLook(InputAction.CallbackContext context)
    {

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        onMove.Invoke(context.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        inputActions = new InputSystem_Actions();
        inputActions.PlayerOnGame.SetCallbacks(this);
    }
    private void OnDisable()
    {
        DisableAllInputs();
    }
    public void EnableGameplayInputs()
    {
        inputActions.PlayerOnGame.Enable();
    }
    public void DisableAllInputs()
    {
        inputActions.PlayerOnGame.Disable();
    }
}
