using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    private GameManager gameManager;
    public float playerSpeed;
    private float horizontalInput;
    private float verticalInput;
    public int lives;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerSpeed = 6f;
        lives = 3;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.ChangeLivesText(lives);
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
        Movement();
    }

    public void LoseALife()
    {
        lives--;
        gameManager.ChangeLivesText(lives);
        if(lives == 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -gameManager.verticalScreenSize + 2, -1), transform.position.z);

        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * playerSpeed);

        if (transform.position.x > gameManager.horizontalScreenSize || transform.position.x <= -gameManager.horizontalScreenSize)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }
        /*
        if (transform.position.y > gameManager.verticalScreenSize || transform.position.y <= -gameManager.verticalScreenSize)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }
        */
    }

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0,1,0), Quaternion.identity);
        }
    }
}
