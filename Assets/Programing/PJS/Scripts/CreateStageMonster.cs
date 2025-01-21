using System.Collections.Generic;
using UnityEngine;
using static StageManager;

public class CreateStageMonster : MonoBehaviour
{
    const int ELITECOUNT = 3;

    [SerializeField] MonsterData monsterData;
    [Header("생성되는 몬스터의 부모가 되는 오브젝트")]
    [SerializeField] MonsterManager monsterManager;
    [Header("생성되는 몬스터의 위치")]
    [SerializeField] List<Transform> monsterPoints;

    [SerializeField] List<string> pointsList = new List<string>();

    private void Awake()
    {
        monsterData = monsterManager.transform.GetComponent<MonsterData>();

        foreach(string monsterPos in monsterData.MonsterList)
        {
            pointsList.Add(monsterPos);
        }
    }

    /// <summary>
    /// 배치되어 있는 리스트 중 랜덤으로 하나를 골라 지정된 위치에 몬스터 스폰
    /// </summary>
    public void MonsterSpawn(StageState state, int curWave, int fullWave)
    {
        string[] point = RandomList(state, curWave, fullWave);
       
        AddPointList();

        for (int i = 0; i < point.Length; i++)
        {
            bool isId = int.TryParse(point[i], out int id);
            if (isId)
            {
                for (int j = 0; j < monsterData.MonsterKey.Count; j++)
                {
                    if (monsterData.MonsterKey[j] == id)
                    {
                        GameObject monster = Instantiate(monsterData.Monster[id], monsterPoints[i].position, monsterPoints[i].rotation);

                        if (monster == null)
                            continue;

                        Monster newMonster = monster.GetComponent<Monster>();
                        newMonster.transform.parent = monsterManager.transform;

                        monsterManager.AddMonster(newMonster);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
    }

    private string[] RandomList(StageState _state, int _curWave, int _fullWave)
    {
        int num;
        int normalMosterCount = pointsList.Count - ELITECOUNT;

        if (_state == StageState.Elite && (_curWave + 1) == _fullWave)
        {
            num = Random.Range(normalMosterCount, pointsList.Count);
        }
        else
        {
            num = Random.Range(0, normalMosterCount);
        }

        if (num <= 0)
        {
            num = 0;
        }

        return monsterData.MonsterList[num].Split(',');
    }

    private void AddPointList()
    {
        for (int i = 0; i < monsterData.MonsterList.Count; i++)
        {
            Debug.Log(monsterData.MonsterList[i]);
            pointsList.Add(monsterData.MonsterList[i]);
        }
    }
}
