using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InputManager : MonoBehaviour
{
    // events
    public event EventHandler OnPlayerAttack;
    public event EventHandler OnPlayerInteract;
    public event EventHandler OnPlayerAltInteract;
    private PlayerActionInput playerActionInput;
    private void Awake()
    {
        playerActionInput = new PlayerActionInput();
        playerActionInput.BaseAbilities.Enable();
        playerActionInput.BaseAbilities.Attack.performed += Attack_performed;
        playerActionInput.BaseAbilities.Interact.performed += Interact_performed;
        playerActionInput.BaseAbilities.AltInteract.performed += AltInteract_performed;
    }

    private void AltInteract_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPlayerAltInteract?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPlayerInteract?.Invoke(this, EventArgs.Empty);
    }

    private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPlayerAttack?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementDirectionNormalized()
    {
        Vector2 inputVector = playerActionInput.BaseAbilities.Movement.ReadValue<Vector2>();
        // Handling Movements
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
