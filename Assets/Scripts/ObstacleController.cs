using System.Collections;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{


    public float speed = 3f;
    private Vector2 spawnPos = new Vector2(11.5f, -4f);
    private Vector2 sleepPos = new Vector2(-13f, -10f);
    public bool isActive = false;
    ObstacleManager om => ObstacleManager.Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
    }

    private void Start()
    {
        gameObject.transform.position = spawnPos;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isActive == true) {
            transform.Translate(Vector2.left * speed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeleteZone"))
        {
            isActive = false;
            gameObject.SetActive(false);
            gameObject.transform.position = spawnPos;
        }
    }

}
