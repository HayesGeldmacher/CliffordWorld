using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
    private bool _inventoryOpen = false;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    [SerializeField] private GameObject _inventoryUI;

    public int _space = 8;

    public List<Item> _items = new List<Item>();

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            _inventoryOpen = !_inventoryOpen;

            if (_inventoryOpen)
            {
                _inventoryUI.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
            else
            {
                _inventoryUI.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }


    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (_items.Count >= _space)
            {
                Debug.Log("Not enough room");
                return false;
            }
            else
            {
                _items.Add(item);

                if (onItemChangedCallback != null)
                {
                    onItemChangedCallback.Invoke();
                }
            }
        }

        return true;
    }

    public void Remove(Item item)
    {
        _items.Remove(item);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}