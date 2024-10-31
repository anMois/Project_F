using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] int stageNum;
    [SerializeField] int curWave;
    [SerializeField] List<int> maxWave;
    [SerializeField] List<string> monsterPlace;
    [SerializeField] MonsterManager monsterManager;
    [SerializeField] MonsterSpawnTable monsterTable;

    public int CurWave { get { return curWave; } set { curWave = value; } }
    public List<string> MonsterPlace { get { return monsterPlace; } set { monsterPlace = value; } }

    private void Update()
    {
        StartCoroutine(MonsterSpawnRoutine());
    }

    IEnumerator MonsterSpawnRoutine()
    {
        yield return new WaitForSeconds(3f);
        if(monsterManager.MonsterCount == 0)
        {
            if (curWave != maxWave[stageNum])
                monsterTable.MonsterSapwn();
            else
            {
                stageNum++;
                curWave = 0;
                yield break;
            }
        }
    }
}
