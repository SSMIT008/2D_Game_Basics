using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{

    [SerializeField] private string requiredInventoryItemString;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == NewPlayer.Instance.gameObject)
        {
            // inventory.ContainsKey("key1");
            // if the player inventory has a "key1", then destroy me!
            // Check to see if the player's inventory contains the required inventory item!
            if (NewPlayer.Instance.inventory.ContainsKey(requiredInventoryItemString))
            {
                NewPlayer.Instance.RemoveInventoryItem(requiredInventoryItemString);
                Destroy(gameObject);
            }

        }
    }
}
