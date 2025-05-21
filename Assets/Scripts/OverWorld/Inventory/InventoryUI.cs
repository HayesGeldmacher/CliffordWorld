using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private Inventory _inventory;
    [SerializeField] private Transform _itemsParent;
    InventorySlot[] _slots;


    // Start is called before the first frame update
    void Start()
    {
        _inventory = Inventory.instance;
        _inventory.onItemChangedCallback += UpdateUI;

        _slots = _itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    public void UpdateUI()
    {
        Debug.Log("updating UI!!");

        for(int i = 0; i < _slots.Length; i++)
        {
            if(i < _inventory._items.Count)
            {
                _slots[i].AddItem(_inventory._items[i]);
            }
            else
            {
                _slots[i].ClearSlot();
            }
        }
    }
}
