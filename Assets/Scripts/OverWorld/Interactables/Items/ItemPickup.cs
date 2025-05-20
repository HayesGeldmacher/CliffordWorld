using UnityEngine;

public class ItemPickup : Interactable
{
    public Item _item;


  public override void Interact()
    {
        base.Interact();
        PickUp();

    }

    private void PickUp()
    {

        Debug.Log("picking up " + _item.name);
        bool wasPickedUp = Inventory.instance.Add(_item);

        if (wasPickedUp)
        {
            Destroy(gameObject);
            
        }
    }
}
