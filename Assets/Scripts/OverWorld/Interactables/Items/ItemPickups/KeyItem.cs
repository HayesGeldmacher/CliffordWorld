using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item/Key")]
public class KeyItem : Item
{

    public string _keyValue;

    public override void Use()
    {
        base.Use();
        Debug.Log("using " + name);
    }
}