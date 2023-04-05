using System;
using UnityEngine;
using UnityEngine.Events;

public class EnterTriggerComponent : MonoBehaviour
{
    [SerializeField] private new string tag;
    
    public Action<GameObject> OnTriggerEntered;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tag))
        {
            OnTriggerEntered?.Invoke(other.gameObject);
        }
    }
}
