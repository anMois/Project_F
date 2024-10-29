using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] List<Monster> monsters = new List<Monster>(10);

    public void AddMonster(Monster newMonster)
    {
        // New monster and other existing monsters subscribe each other
        foreach (Monster mon in monsters)
        {
            mon.OnAlarm += newMonster.Trace;
            newMonster.OnAlarm += mon.Trace;
        }
        newMonster.OnAlarm += newMonster.Trace;

        monsters.Add(newMonster);
    }
}
