using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [Tooltip("Invoke OnAreaOut event when player moves to outside of start zone")]
    [SerializeField] StartZone startZone;

    [SerializeField] List<Monster> monsters = new List<Monster>(10);
    public int MonsterCount { get { return monsters.Count; } }

    public void AddMonster(Monster newMonster)
    {
        // Subscribes events
        foreach (Monster mon in monsters)
        {
            mon.OnAlarm += newMonster.Trace;
            newMonster.OnAlarm += mon.Trace;
        }
        newMonster.OnAlarm += newMonster.Trace;
        startZone.OnAreaOut += newMonster.Trace;

        monsters.Add(newMonster);
    }
}
