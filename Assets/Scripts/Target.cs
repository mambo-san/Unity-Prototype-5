using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;

    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xSpawnRange = 6;
    private float ySpawnPos = -3;

    public ParticleSystem explosionParticle;
    public int pointValue;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        targetRb = GetComponent<Rigidbody>();
        targetRb.position = randomSpawnPos();
        targetRb.AddForce(randomForce(), ForceMode.Impulse);
        targetRb.AddTorque(randomTorque(), randomTorque(), randomTorque());
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -20)
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseOver()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mouse"))
        {
            if (gameManager.isGameActive && !gameManager.isPaused)
            {
                
                gameManager.updateScore(pointValue);
                Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (!gameObject.CompareTag("Bad"))
        {
            Debug.Log("Collided with " + other.gameObject.tag);
            gameManager.DecreaseLives();
            
        }
        Destroy(gameObject);
    }



    private Vector3 randomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    private float randomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    private Vector3 randomSpawnPos()
    {
        return new Vector3(Random.Range(-xSpawnRange, xSpawnRange), ySpawnPos);
    }
}
