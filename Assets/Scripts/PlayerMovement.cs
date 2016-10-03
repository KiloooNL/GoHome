using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public GameManager manager;

    public float moveSpeed = 20f; // Movement Speed
    public GameObject deathParticles;

    private float maxSpeed = 5f;

    private Vector3 input;        // Vector3 Input
    public Rigidbody rbName;      // Rigidbody

    private Vector3 spawn;

    // Use this for initialization
    void Start()
    {
        rbName = GetComponent<Rigidbody>();
        spawn = transform.position;

        // Get GameManager class.
        manager = manager.GetComponent<GameManager>();

        print("Current Level: Level " + manager.currentLevel);

    }

    void FixedUpdate()
    {   
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
            manager.CompleteLevel();
        }

        // Collect coin
        if(other.transform.tag == "Coin")
        {
            // Add to score & remove coin.
            print("Collected coin.");
            manager.AddCoin();
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
