using UnityEngine;

[CreateAssetMenu(fileName = "BoxData", menuName = "Data/New BoxData")]
public class BoxData : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private int strength;
    [SerializeField] private float mass;
    [SerializeField] private bool isBroken;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Sprite boxSprite;
    [SerializeField] private Sprite icon;

    private int _destruction;

    public string Name => name;
    public int Strength => strength;
    public float Mass => mass;
    public bool IsBroken => isBroken;
    public GameObject Prefab => prefab;
    public Sprite BoxSprite => boxSprite;
    public Sprite Icon => icon;

    public void Init()
    {
        prefab.GetComponent<SpriteRenderer>().sprite = boxSprite;
        prefab.GetComponentInChildren<SpriteRenderer>().sprite = icon;
    }

    public void AddDestruction(int newDestruction)
    {
        if (!isBroken)
        {
            _destruction += newDestruction;
            if (_destruction >= strength)
            {
                isBroken = true;
            }
        }
    }
}