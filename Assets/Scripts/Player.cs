using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameManager manager;

    public float moveSpeed = 20f; // Movement Speed
    public GameObject deathParticles;

    private float maxSpeed = 5f;
    public bool usesManager = true;

    private Vector3 input;        // Vector3 Input
    public Rigidbody rbName;      // Rigidbody

    private Vector3 spawn;

    // Use this for initialization
    void Start()
    {
        if (usesManager)
        {
            // Get GameManager class.
            manager = manager.GetComponent<GameManager>();
            print("Current Level: Level " + manager.currentLevel);
        }

        rbName = GetComponent<Rigidbody>();
        spawn = transform.position;



    }

    public string date;
    public string December_25;
    public string December_31;
    public bool alive = true;

    void work()
    {
        print("corporation");
    }

    void NewYear()
    {
        if(date == December_25 || date == December_31)
        {
            NewYear();
        } else
        {
            ContinueSlavingAway();
        }
    }

    void ContinueSlavingAway()
    {
        do { work(); }
        while (alive == true);

        print("Congraturation. You are now dead.");
    }

    void FixedUpdate()
    {
        NewYear();
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (rbName.velocity.magnitude < maxSpeed)
        {
            rbName.AddForce(input * moveSpeed);
        }

        // If player falls off map, die();
        if (transform.position.y <= -2)
        {
            Die();
        }

        Physics.gravity = Physics.Raycast(transform.position, Vector3.down, .5f) ? Vector3.zero : new Vector3(0, -9.5f, 0);

    }

    // On Collision
    void OnCollisionEnter(Collision other)
    {
        // On collision with non-object enemies (ie moving cubes)
        if (other.gameObject.tag == "Enemy")
        {
            Die();
        }
    }

    void OnTriggerEnter(Collider other)
    {

        // On collision with object enemies (such as spikes)
        if (other.transform.tag == "Enemy")
        {
            Die();
        }

        // Reach end of level
        if (other.transform.tag == "Finish")
        {
            Destroy(gameObject);
            
            manager.PlaySound(1);
            manager.CompleteLevel();
        }

        // Collect coin
        if(other.transform.tag == "Coin")
        {
            // Add to score & remove coin.
            print("Collected coin.");

            // Add coin to coin count score.
            manager.coinCount++;

            manager.PlaySound(0);

            // We don't need to call this function because all it does is
            // increment manager.coinCount by 1. (coinCount++)
            // We may re-add this function once sound is added.
            //manager.AddCoin();
            Destroy(other.gameObject);
        }
    }



    void Die()
    {
        print("Player hit enemy");
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        transform.position = spawn;
    }
    void LevelEnd()
    {


    }

    // When we are continuing the collision
    void OnCollisionStay(Collision other)
    {

    }
}
