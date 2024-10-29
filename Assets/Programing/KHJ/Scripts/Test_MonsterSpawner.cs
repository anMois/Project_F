using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_MonsterSpawner : MonoBehaviour
{
    [SerializeField] MonsterManager monsterManager;

    [SerializeField] GameObject[] monsterPrefabs;
    [SerializeField] Transform spawnPoint;

    private void Update()
    {
        Monster newMonster;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            newMonster = Instantiate(monsterPrefabs[0], spawnPoint.position, Quaternion.identity).GetComponent<Monster>();
            monsterManager.AddMonster(newMonster);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            newMonster = Instantiate(monsterPrefabs[1], spawnPoint.position, Quaternion.identity).GetComponent<Monster>();
            monsterManager.AddMonster(newMonster);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            newMonster = Instantiate(monsterPrefabs[2], spawnPoint.position, Quaternion.identity).GetComponent<Monster>();
            monsterManager.AddMonster(newMonster);
        }
    }
}
