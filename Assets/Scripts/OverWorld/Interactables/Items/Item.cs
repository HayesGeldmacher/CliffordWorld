using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public bool limited = true;
    public float uses = 1;

    public virtual void Use()
    {
        Debug.Log("using " + name);

        if (limited)
        {
            uses -= 1;
            if(uses <= 0)
            {
                Inventory.instance.Remove(this);
            }
        }
    }
}
