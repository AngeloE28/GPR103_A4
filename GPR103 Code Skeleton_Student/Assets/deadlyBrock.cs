using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadlyBrock : MonoBehaviour
{
    public GameManager gameManager;

    public Vector2 startingPosition = new Vector2(-5,-2);
    public Vector2 endPosition = new Vector2(4,-2);
    public int direction = 1;
    public float speed = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * direction * speed);
        if ((transform.position.x * direction) > (endPosition.x * direction))
        {
            transform.position = startingPosition;
        }

    }
}
