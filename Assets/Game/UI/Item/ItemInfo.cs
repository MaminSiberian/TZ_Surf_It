using UnityEngine;

[CreateAssetMenu(fileName = "ItemInfo", menuName = "Inventory/New ItemInfo")]
public class ItemInfo : ScriptableObject
{
    [SerializeField] private string _itemName;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Vector2Int _size;

    public string itemName => _itemName;
    public Sprite sprite => _sprite;
    public Vector2Int size => _size;
}
