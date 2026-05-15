using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public float leftBoundary = -10;
    void Update()
    {
        MoveObstacles();
    }
    private void MoveObstacles()
    {
        transform.position += Vector3.left * GameManager.instance.GetGameSpeed() * Time.deltaTime;
        if (transform.position.x < leftBoundary)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.GameOver();
        }
    }
}
