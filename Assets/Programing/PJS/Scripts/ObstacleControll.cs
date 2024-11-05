using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleControll : MonoBehaviour
{
    [Header("배치할 장애물 리스트")]
    [SerializeField] List<GameObject> obstacles;

    private void Start()
    {
        CreateObs();
    }

    private void CreateObs()
    {
        int num = Random.Range(0, obstacles.Count);
        Instantiate(obstacles[num], obstacles[num].transform.position, Quaternion.identity);
    }
}
