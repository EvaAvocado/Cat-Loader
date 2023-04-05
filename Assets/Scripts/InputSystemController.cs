using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemController : MonoBehaviour
{
    public static Action<float> OnMoved;
    public static Action OnJumped;
    public static Action OnInteracted;

    private InputSystem _input;

    #region Monobehaviour Callbacks

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    #endregion

    public void Init()
    {
        _input = new InputSystem();
        _input.Main.Jump.started += context => OnJump();
        _input.Main.Interact.started += context => OnInteract();
    }

    private void FixedUpdate()
    {
        OnHorizontal(_input.Main.HorizontalMove.ReadValue<float>());
    }

    private void OnHorizontal(float direction)
    {
        OnMoved?.Invoke(direction);
    }

    private void OnJump()
    {
        OnJumped?.Invoke();
    }

    private void OnInteract()
    {
        OnInteracted?.Invoke();
    }
}