using System;
using UnityEngine;
using UnityEngine.Events;

public class ExitTriggerComponent : MonoBehaviour
{
    [SerializeField] private new string tag;
    
    public Action<GameObject> OnTriggerExited;
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(tag))
        {
            OnTriggerExited?.Invoke(other.gameObject);
        }
    }
}
