using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PhysicsObject
{

    [Header("Attributes")]

    [SerializeField] private int attackPower = 10;
    private int direction = 1;
    public int health = 100;
    private int maxHealth = 100;
    [SerializeField] private float maxSpeed;
    [SerializeField] private LayerMask rayCastLayerMask; // Which layer do we want the raycast to interact with?
    [SerializeField] private Vector2 rayCastOffset; // Offset from the center of the raycast origin
    [SerializeField] private float rayCastLength = 2;

    [Header("References")]

    private RaycastHit2D rightLedgeRaycastHit;
    private RaycastHit2D leftLedgeRaycastHit;
    private RaycastHit2D rightWallRaycastHit;
    private RaycastHit2D leftWallRaycastHit;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = new Vector2(maxSpeed * direction, 0);

        /* If the rightLedgeRaycastHit fires a laser and hits ground
         in front of the player on the right, we are not near a ledge! */

        //Check for right Ledge
        rightLedgeRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x + rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.down, rayCastLength);
        Debug.DrawRay(new Vector2(transform.position.x + rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.down * rayCastLength, Color.red);
        if (rightLedgeRaycastHit.collider == null) direction = -1;
        /*{
            //Debug.Log("Crap! I'm close to the right ledge");
            direction = -1;
        }
        else
        {
            Debug.Log("I'm touching: " + rightLedgeRaycastHit.collider.gameObject);
        }
        */

        // Check for left Ledge
        leftLedgeRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x - rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.down, rayCastLength);
        Debug.DrawRay(new Vector2(transform.position.x - rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.down * rayCastLength, Color.red);
        if (leftLedgeRaycastHit.collider == null) direction = 1;

        //Check for right wall!
        rightWallRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, rayCastLength, rayCastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.right * rayCastLength, Color.red);
        if (rightWallRaycastHit.collider != null) direction = -1;

        //Check for left wall!
        leftWallRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, rayCastLength, rayCastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.left * rayCastLength, Color.red);
        if (leftWallRaycastHit.collider != null) direction = 1;

        // If health is smaller than 0, destroy me
        if (health <= 0)
        {
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
    }

    // if I collide with player - Hurt the Player (health is going to decreease). And update the UI
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject == NewPlayer.Instance.gameObject)
        {
            // Hurt the player, then update the UI
            NewPlayer.Instance.health -= attackPower;
            NewPlayer.Instance.UpdateUI();
        }
    }

}
