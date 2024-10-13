using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "InputActions")]
public class InputActions : ScriptableObject, InputSystem_Actions.IPlayerOnGameActions
{
    public event UnityAction<Vector2> onMove = delegate { };
    public event UnityAction onStopMove = delegate { };
    public event UnityAction onAttack = delegate { };
    public event UnityAction onStopAttack = delegate { };
    public event UnityAction<Vector2> onLook = delegate { };
    InputSystem_Actions inputActions;

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Canceled)
        {
            onStopAttack.Invoke();
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        onLook.Invoke(context.ReadValue<Vector2>());
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onMove.Invoke(context.ReadValue<Vector2>());
        }
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
