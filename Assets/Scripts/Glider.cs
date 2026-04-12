using UnityEngine;

public class Glider : MonoBehaviour
{
    public bool goingUp;
    public float speed;
    private GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (goingUp)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        else if (!goingUp)
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }

        if(transform.position.y < -gameManager.verticalScreenSize)
        {
            Destroy(this.gameObject);
        }

        /*
        transform.Translate(new Vector3(0,-1,0) * Time.deltaTime * 8.0f);


        if (transform.position.y < -6.5f)
        {
            Destroy(gameObject);
        }
        */
    }
}
