using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [Tooltip("Invoke OnAreaOut event when player moves to outside of start zone")]
    [SerializeField] StartZone startZone;
    [Tooltip("Serve stage & wave information")]
    [SerializeField] StageManager stageManager;

    [SerializeField] List<Monster> monsters = new List<Monster>(10);
    public int MonsterCount { get { return monsters.Count; } }

    public void AddMonster(Monster newMonster)
    {
        // Set initial state
        if (stageManager.CurWave > 0)
        {
            newMonster.ChangeState(Monster.State.Trace);
        }

        // Subscribes events
        foreach (Monster mon in monsters)
        {
            mon.OnAlarm += newMonster.Trace;
            newMonster.OnAlarm += mon.Trace;
        }
        newMonster.OnDead += RemoveMonster;
        startZone.OnAreaOut += newMonster.Trace;

        monsters.Add(newMonster);
    }

    private void RemoveMonster(Monster monToRemove)
    {
        monsters.Remove(monToRemove);

        foreach (Monster mon in monsters)
        {
            mon.OnAlarm -= monToRemove.Trace;
            monToRemove.OnAlarm -= mon.Trace;
        }
        monToRemove.OnDead -= RemoveMonster;
        startZone.OnAreaOut -= monToRemove.Trace;
    }
}
