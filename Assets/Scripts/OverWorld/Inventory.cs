using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }
    #endregion

    public int _space = 8;

    public List<Item> _items = new List<Item>();

    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
           if(_items.Count >= _space)
            {
                Debug.Log("Not enough room");
                return false;
            }
            else
            {
                _items.Add(item);
            }
        }

        return true;
    }

    public void Remove(Item item)
    {
        _items.Remove(item);
    }
}
