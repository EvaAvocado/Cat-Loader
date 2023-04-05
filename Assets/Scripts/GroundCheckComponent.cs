using System;
using UnityEngine;

public class GroundCheckComponent : MonoBehaviour
{
    #region Variables

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.1f;

    private readonly Collider2D[] _groundCheckResult = new Collider2D[5];
    private bool _isGrounded = true;

    public Action<bool> OnGroundChecked;

    #endregion

    private void FixedUpdate()
    {
        CheckGroundLayer();
    }

    private void CheckGroundLayer()
    {
        var size = Physics2D.OverlapCircleNonAlloc(transform.position, groundCheckRadius, _groundCheckResult,
            groundLayer);
        var currentIsGrounded = size > 0;

        if (_isGrounded != currentIsGrounded)
        {
            _isGrounded = currentIsGrounded;
            OnGroundChecked?.Invoke(_isGrounded);
        }
    }
}