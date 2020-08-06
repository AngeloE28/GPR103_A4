using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSimpleMove : MonoBehaviour
{
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && transform.position.y < gameManager.levelConstraintTop)
        {
            transform.position = transform.position + new Vector3(0, 1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S) && transform.position.y > gameManager.levelConstraintBottom)
        {
            transform.position = transform.position + new Vector3(0, -1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.A) && transform.position.x > gameManager.levelConstraintLeft)
        {
            transform.position = transform.position + new Vector3(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D) && transform.position.x < gameManager.levelConstraintRight)
        {
            transform.position = transform.position + new Vector3(1, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<Vehicle>()!= null)
        {
            print("Hit");
        }
        else
        {
            print(collision.transform.tag);
        }
    }
}
