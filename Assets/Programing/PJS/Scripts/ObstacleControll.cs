using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleControll : MonoBehaviour
{
    [SerializeField] List<GameObject> obstacles;

    private void Start()
    {
        CreateObs();
    }

    private void CreateObs()
    {
        GameObject obj = RandomObs();
        Instantiate(obj, obj.transform.position, Quaternion.identity);
    }

    private GameObject RandomObs()
    {
        int num = Random.Range(0, obstacles.Count);
        return obstacles[num];
    }
}
