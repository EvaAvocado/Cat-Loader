using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables

    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private Animator animator;
    [SerializeField] private GroundCheckComponent groundCheckComponent;
    
    private Rigidbody2D _rb;
    private bool _isGrounded;
    private List<Box> _triggeredBoxesList;
    private List<Box> _boxesList;
    private int _boxesLimit;
    private EnterTriggerComponent _enterTriggerComponent;
    private ExitTriggerComponent _exitTriggerComponent;

    private static readonly int IsHorizontalMove = Animator.StringToHash("is-horizontal-move");
    private static readonly int IsGrounded = Animator.StringToHash("is-grounded");

    //public List<Box> BoxesList => _boxesList;
    public Action<List<Box>> OnEditedBoxesList;

    #endregion

    #region Monobehaviour Callbacks

    private void OnEnable()
    {
        InputSystemController.OnMoved += Move;
        InputSystemController.OnJumped += Jump;
        InputSystemController.OnInteracted += LiftBox;
        groundCheckComponent.OnGroundChecked += SetGroundedStatus;
        _enterTriggerComponent.OnTriggerEntered += AddBoxInTriggeredList;
        _exitTriggerComponent.OnTriggerExited += DeleteBoxFromTriggeredList;
    }

    private void OnDisable()
    {
        InputSystemController.OnMoved -= Move;
        InputSystemController.OnJumped -= Jump;
        InputSystemController.OnInteracted -= LiftBox;
        groundCheckComponent.OnGroundChecked -= SetGroundedStatus;
        _enterTriggerComponent.OnTriggerEntered -= AddBoxInTriggeredList;
        _exitTriggerComponent.OnTriggerExited -= DeleteBoxFromTriggeredList;
    }

    #endregion

    public void Init()
    {
        SetGroundedStatus(true);
        _rb = GetComponent<Rigidbody2D>();
        _enterTriggerComponent = GetComponent<EnterTriggerComponent>();
        _exitTriggerComponent = GetComponent<ExitTriggerComponent>();
        _boxesLimit = 1;
        _triggeredBoxesList = new List<Box>();
        _boxesList = new List<Box>();
    }

    private void Move(float horizontalDirection)
    {
        _rb.velocity = new Vector2(horizontalDirection * speed + Time.fixedDeltaTime, _rb.velocity.y);

        if (horizontalDirection != 0)
        {
            transform.localScale = new Vector3(horizontalDirection > 0 ? 1 : -1, 1, 1);
        }

        animator.SetBool(IsHorizontalMove, horizontalDirection != 0);
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * _rb.gravityScale));
            _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void AddBoxInTriggeredList(GameObject gameObj)
    {
        var box = gameObj.GetComponent<Box>();
        if (box != null && _triggeredBoxesList.Count < _boxesLimit)
        {
            _triggeredBoxesList.Add(box);
        }
    }

    private void DeleteBoxFromTriggeredList(GameObject gameObj)
    {
        var box = gameObj.GetComponent<Box>();
        if (box != null)
        {
            if (_triggeredBoxesList.Contains(box))
            {
                _triggeredBoxesList.Remove(box);
            }
            
        }
    }
    
    public void DeleteBoxFromList(GameObject gameObj)
    {
        var box = gameObj.GetComponent<Box>();
        if (box != null)
        {
            if (_boxesList.Contains(box))
            {
                _boxesList.Remove(box);
                OnEditedBoxesList?.Invoke(_boxesList);
            }
            
        }
    }

    private void LiftBox()
    {
        if (_triggeredBoxesList.Count > 0)
        {
            _boxesList = _triggeredBoxesList;
            OnEditedBoxesList?.Invoke(_boxesList);
            
            foreach (var box in _boxesList)
            {
                box.TeleportToPlayer();
            }
        }
    }

    private void SetGroundedStatus(bool status)
    {
        _isGrounded = status;
        animator.SetBool(IsGrounded, _isGrounded);
    }
}