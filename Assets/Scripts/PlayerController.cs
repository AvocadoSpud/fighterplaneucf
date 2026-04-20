using System.Collections;
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

    public GameObject thruster;
    public GameObject shield;

    public int weaponType;
    public bool shieldActive;

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
        if (shieldActive)
        {
            shieldActive = false;
        }
        else
        {
            lives--;
        }
        gameManager.ChangeLivesText(lives);
        if(lives == 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            gameManager.GameOver();

            Destroy(gameObject);
        }
    }
    IEnumerator ShieldPowerDown()
    {
        yield return new WaitForSeconds(5);
        shield.SetActive(false);
        shieldActive = false;
        gameManager.PlaySound(2);
        gameManager.ManagePowerupText(5);
    }
    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(5);
        playerSpeed = 5f;
        thruster.SetActive(false);
        gameManager.PlaySound(2);
        gameManager.ManagePowerupText(5);
    }
    IEnumerator WeaponPowerDown()
    {
        yield return new WaitForSeconds(5);
        weaponType = 1;
        gameManager.PlaySound(2);
        gameManager.ManagePowerupText(5);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            gameManager.AddScore(1);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Powerup"))
        {
            Destroy(collision.gameObject);
            int whichPowerup = Random.Range(1, 5);
            gameManager.PlaySound(1);
            switch (whichPowerup)
            {
                case 1:
                    playerSpeed = 10;
                    // start speed coroutine down
                    // thruster active
                    thruster.SetActive(true);
                    StartCoroutine(SpeedPowerDown());
                    break;
                case 2:
                    // set weapon type to 2
                    weaponType = 2;
                    // weapon power down coroutine
                    StartCoroutine(WeaponPowerDown());
                    break;
                case 3:
                    // set weapon type to 3
                    weaponType = 3;
                    // weapon power down
                    StartCoroutine(WeaponPowerDown());
                    break;

                case 4:
                    //set shield active
                    //set the shield power down coroutine
                    shield.SetActive(true);
                    shieldActive = true;
                    StartCoroutine(ShieldPowerDown());
                    break;
            }
            gameManager.ManagePowerupText(whichPowerup);
        }
    }
}
