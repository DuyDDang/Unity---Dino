using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private Transform highPos;
    [SerializeField] private Transform lowPos;
    private float timer = 0;
    [SerializeField] private float spawRate = 2f;


    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawRate)
        {
            SpawObstacles();
            timer = 0;
        }

    }
    private void SpawObstacles()
    {
        int index = Random.Range(0, obstacles.Length);
        if (index == 0 || index == 1)
        {
            GameObject obstacle = Instantiate(obstacles[index], lowPos.position, Quaternion.identity);
        }
        else if (index == 2)
        {
            GameObject obstacle = Instantiate(obstacles[index], highPos.position, Quaternion.identity);
        }
    }
}
