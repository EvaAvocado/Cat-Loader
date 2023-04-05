using UnityEngine;

public class AwakeController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private InputSystemController inputSystem;
    [SerializeField] private Box[] boxes;
    
    private void Awake()
    {
        if (inputSystem != null)
        {
            inputSystem.Init();
        }

        if (player != null)
        {
            player.Init();
        }

        if (boxes != null)
        {
            foreach (var box in boxes)
            {
                box.Init();
            }
        }
        
    }
}
