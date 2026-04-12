using UnityEngine;

public class WaveyMovement : MonoBehaviour
{
    public float speed = 5;
    public float size = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float wave = Mathf.Sin(Time.time * speed) * size;
        transform.Translate(Vector3.right * Time.deltaTime * wave);
    }
}
