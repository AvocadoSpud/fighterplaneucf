using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject explosionPrefab;
    private GameManager gameManager;

    private void Start()
    {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerController>().LoseALife();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        else if (other.tag == "Weapons")
        {
            Destroy(other.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameManager.AddScore(5);
            Destroy(gameObject);
        }
    }


}
