using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item/Key")]
public class KeyItem : Item
{


    public override void Use()
    {
        Debug.Log("using " + name);
    }
}