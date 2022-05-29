using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    /* 
   [SerializeField] private bool isCoin;
   [SerializeField] private bool isHealth;
   [SerializeField] private bool isAmmo;
   */
    //NewPlayer newPlayer; - no need because of singleton

    enum ItemType { Coin, Health, Ammo, InventoryItem } // Creates an Item Type enum (Drop Down)
    [SerializeField] private ItemType itemType;
    [SerializeField] public string inventoryStringName;
    [SerializeField] public Sprite inventorySprite;

    // Start is called before the first frame update
    void Start()
    {
        // If I'm a coin - print to the console "I'm a coin"
        /*
        if (itemType == ItemType.Coin)
        {
            Debug.Log("I'm a Coin!");
        }
        else if (itemType == ItemType.Health)
        {
            Debug.Log("I'm Health!");
        }
        else if (itemType == ItemType.Ammo)
        {
            Debug.Log("I'm Ammo!");
        }
        else
        {
            Debug.Log("I'm an Inventory Item!");
        }
        */

        // to avoid redandancy we should create this to prevent doing the same line of code over and over again
        //newPlayer = GameObject.Find("Player").GetComponent<NewPlayer>(); -no need because of singleton
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player is touch me, print "Collect!" in the console
        if (collision.gameObject == NewPlayer.Instance.gameObject)
        {
            if (itemType == ItemType.Coin)
            {
                NewPlayer.Instance.coinsCollected += 10;
                Destroy(gameObject);
            }
            else if (itemType == ItemType.Health)
            {
                if (NewPlayer.Instance.health < 100)
                {
                    NewPlayer.Instance.health += 10;
                    Destroy(gameObject);
                }

            }
            else if (itemType == ItemType.Ammo)
            {

            }
            else if (itemType == ItemType.InventoryItem)
            {
                NewPlayer.Instance.AddInventoryItem(inventoryStringName, inventorySprite);
                Destroy(gameObject);
            }
            else
            {

            }

            //Update UI
            NewPlayer.Instance.UpdateUI();

        }
    }

}
