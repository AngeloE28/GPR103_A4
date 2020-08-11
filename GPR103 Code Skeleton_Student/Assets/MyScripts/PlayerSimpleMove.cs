using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSimpleMove : MonoBehaviour
{
    public GameManager gameManager;
    public LayerMask platformMask;
    public LayerMask riverMask;

    public Vector2 Pos;
    public Vector2 ePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position + new Vector3(0.3f, 0f, 0f), Vector2.left, 0.1f, platformMask);
        if (water())
        {
            ePos = new Vector2(6, -8.5f);
        }
      
        if (hitInfo)
        {
            transform.SetParent(GetComponent<Platform>().transform);
        }
        else
        {
            DetachFromParent();
        }
    }

    bool water()
    {
        bool isWater = false;
        Debug.DrawRay(transform.position + new Vector3(0.03f, -0.25f, 0f), Vector2.up*0.1f, Color.red);

        RaycastHit2D hitInfo4 = Physics2D.Raycast(transform.position + new Vector3(0.03f, -0.25f, 0f), Vector2.up, 0.1f, riverMask);

        if (hitInfo4)
        {
            isWater = true;
        }
        else
        {
            isWater = false;
        }
        return isWater;
    }

    void Move()
    {
        if (Input.GetKeyDown(KeyCode.W) && transform.position.y < gameManager.levelConstraintTop)
        {
            transform.position = transform.position + new Vector3(0, 1, 0);
            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S) && transform.position.y > gameManager.levelConstraintBottom)
        {
            transform.position = transform.position + new Vector3(0, -1, 0);
            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 180);
        }
        else if (Input.GetKeyDown(KeyCode.A) && transform.position.x > gameManager.levelConstraintLeft)
        {
            transform.position = transform.position + new Vector3(-1, 0, 0);
            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 90);
        }
        else if (Input.GetKeyDown(KeyCode.D) && transform.position.x < gameManager.levelConstraintRight)
        {
            transform.position = transform.position + new Vector3(1, 0, 0);
            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, -90);
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
        else if (collision.transform.GetComponent<Platform>() != null)
        {
            print("ran into log");
           // transform.SetParent(collision.transform);
        }
        //else
        //{
        //    Destroy(this.gameObject);
        //}
    }
    void DetachFromParent()
    {
        transform.SetParent(null);
    }
}
