using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStageMonster : MonoBehaviour
{
    private MonsterData monsterData;

    [Header("생성되는 몬스터의 부모가 되는 오브젝트")]
    [SerializeField] MonsterManager monsterManager;
    [Header("생성되는 몬스터의 위치")]
    [SerializeField] List<Transform> monsterPoints;

    private void Awake()
    {
        monsterData = GetComponent<MonsterData>();
    }

    /// <summary>
    /// 배치되어 있는 리스트 중 랜덤으로 하나를 골라 지정된 위치에 몬스터 스폰
    /// </summary>
    public void MonsterSpawn()
    {
        int num = RandomNum();
        Debug.Log(num);
        string[] point = monsterData.MonsterList[num].Split(',');

        for (int i = 0; i < point.Length; i++)
        {
            int.TryParse(point[i], out int id);
            for (int j = 0; j < monsterData.MonsterKey.Count; j++)
            {
                if (monsterData.MonsterKey[j] == id)
                {
                    Monster newMonster = Instantiate(monsterData.Monster[id], monsterPoints[i].position, monsterPoints[i].rotation).GetComponent<Monster>();
                    newMonster.transform.parent = monsterManager.transform;
                    monsterManager.AddMonster(newMonster);
                    Debug.Log($"오브젝트 생성 {id}");
                }
            }
        }
    }

    private int RandomNum()
    {
        int num = Random.Range(0, monsterData.MonsterList.Count);

        if (!monsterData.MonsterListActive[num])
        {
            monsterData.MonsterListActive[num] = true;
            return num;
        }
        else
            return RandomNum();
    }
}
