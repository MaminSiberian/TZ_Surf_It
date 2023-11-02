using UnityEngine;

[CreateAssetMenu(fileName = "ItemInfo", menuName = "Inventory/New ItemInfo")]
public class ItemInfo : ScriptableObject
{
    [SerializeField] private string _itemName;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _sizeX;
    [SerializeField] private int _sizeY;

    public string itemName => _itemName;
    public Sprite sprite => _sprite;
    public int sizeX => _sizeX;
    public int sizeY => _sizeY;
}
