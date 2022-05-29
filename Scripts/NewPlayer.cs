using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewPlayer : PhysicsObject
{
    [Header("Attributes")]

    [SerializeField] private float attackDuration; // This one shows how long the attack box is active when attacking
    public int attackPower = 25;
    [SerializeField] private float jumpPower = 1;
    [SerializeField] private float maxSpeed = 1;

    [Header("Inventory")]

    public int ammo;
    public int coinsCollected;
    public int health = 100;
    private int maxHealth = 100;

    [Header("References")]

    [SerializeField] private GameObject attackBox;
    public Dictionary<string, Sprite> inventory = new Dictionary<string, Sprite>(); // Dictionary story all inventory item strings and values
    public Sprite inventoryItemBlank; // The default inventory item slot sprite
    private Vector2 healthBarOrigSize;
    public Sprite keySprite; // The key inventory item
    public Sprite keyGemSprite; // The gem key inventory item

    // Singleton Instantiation
    private static NewPlayer instance;

    public static NewPlayer Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<NewPlayer>();
            return instance;
        }
    }

    //Awake
    private void Awake()
    {
        if (GameObject.Find("New Player")) Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //don't destroy player while loading scenes
        DontDestroyOnLoad(gameObject);
        gameObject.name = "New Player";

        //coinsText = GameObject.Find("Coins").GetComponent<Text>(); - decided to drag in the UI, since we are using this once
        healthBarOrigSize = GameManager.Instance.healthBar.rectTransform.sizeDelta;
        UpdateUI();

        SetSpawnLocation();

        // I want to add an inventory item of: Key2 with the sprite of gemKey
        // AddInventoryItem("key2", keyGemSprite);
    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, 0);

        // If the player presses "Jump" and we're grounded, set the velocity to a jump power value
        if (Input.GetButton("Jump") && grounded)
        {
            velocity.y = jumpPower;
        }

        // Flip the player's localscale.x to if the move speed is greater than 0.01 or less than -0.01
        if (targetVelocity.x < -.01)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (targetVelocity.x > 0.01)
        {
            transform.localScale = new Vector2(1, 1);
        }

        // If we press "Fire1", then set the attackBox to active. Otherwise set active to false
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(ActivateAttack());
        }
        /* {
               // StartCoroutine(ActivateAttack())
               attackBox.SetActive(true);
           }
           else
           {
               attackBox.SetActive(false);
           } */

        //Check if Player Health is < or = to 0
        if (health <= 0)
        {
            Die();
        }

    }

    // ActivateAttack function
    public IEnumerator ActivateAttack()
    {
        attackBox.SetActive(true);
        // wait 0.5f sec
        yield return new WaitForSeconds(attackDuration);
        attackBox.SetActive(false);
    }


    //Update UI Element
    public void UpdateUI()
    {
        // If the HealthBarOrigSize has not been set yet, match it to the healthbar rectTransform Size
        if (healthBarOrigSize == Vector2.zero) healthBarOrigSize = GameManager.Instance.healthBar.rectTransform.sizeDelta;

        //CoinsUI = coinsCollect
        // 10 --> "10"
        GameManager.Instance.coinsText.text = coinsCollected.ToString();

        // Set healthBar width to a percentage of its orig value
        // healthBarOrogSize.x * (health/maxHealth)

        GameManager.Instance.healthBar.rectTransform.sizeDelta = new Vector2(healthBarOrigSize.x * ((float)health / (float)maxHealth), GameManager.Instance.healthBar.rectTransform.sizeDelta.y);

        // Set Health Bar Width to 100
        //healthBar.rectTransform.sizeDelta = new Vector2(100, healthBar.rectTransform.sizeDelta.y);

    }

    public void Die()
    {
        Destroy(GameManager.Instance.gameObject);
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetSpawnLocation()
    {
        transform.position = GameObject.Find("Spawn Location").transform.position;
    }

    public void AddInventoryItem(string inventoryName, Sprite image)
    {
        inventory.Add(inventoryName, image);
        // The blank sprite should now swap with the key sprite
        GameManager.Instance.inventoryItemImage.sprite = inventory[inventoryName];
    }

    public void RemoveInventoryItem(string inventoryName)
    {
        inventory.Remove(inventoryName);
        //The blank sprite should now swap with key sprite
        GameManager.Instance.inventoryItemImage.sprite = inventoryItemBlank;
    }
}
