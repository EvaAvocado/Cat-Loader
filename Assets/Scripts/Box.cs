using System;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    #region Variables
    
    [SerializeField] private float magneticForce;
    [SerializeField] private float magneticError;
    [SerializeField] private GroundCheckComponent groundCheckComponent;
    
    private Rigidbody2D _rb;
    private Player _player;
    [SerializeField] private bool _isGrounded;
    private bool _isMagnetic;

    #endregion
    
    #region Monobehaviour Callbacks

    private void OnEnable()
    {
        groundCheckComponent.OnGroundChecked += SetIsGroundedState;
        _player.OnEditedBoxesList += CheckIsMagneticState;
    }

    private void OnDisable()
    {
        groundCheckComponent.OnGroundChecked -= SetIsGroundedState;
        _player.OnEditedBoxesList -= CheckIsMagneticState;
    }

    #endregion
    
    public void Init()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<Player>();
    }

    private void FixedUpdate()
    {
        MagneticToPlayer();
    }

    private void MagneticToPlayer()
    {
        if (_isMagnetic)
        {
            Vector2 dir = (_player.transform.position - transform.position).normalized;
            if (transform.position.x < _player.transform.position.x + magneticError &&
                  transform.position.x > _player.transform.position.x + -magneticError &&
                  transform.position.y < _player.transform.position.y + magneticError &&
                  transform.position.y > _player.transform.position.y + -magneticError)
            {
                _rb.velocity = Vector2.zero;
            }
            else
            {
                _rb.AddForce(dir * magneticForce);
            }
        }
        else
        {
            _rb.velocity = Vector2.zero;
            
        }
    }

    public void TeleportToPlayer()
    {
        transform.position = _player.transform.position;
    }

    private void CheckIsMagneticState(List<Box> boxesList)
    {
        _isMagnetic = boxesList.Contains(this);
    }

    private void SetIsGroundedState(bool status)
    {
        _isGrounded = status;
        if (_isGrounded)
        {
            _isMagnetic = false;
            _player.DeleteBoxFromList(gameObject);
        }
    }
}
