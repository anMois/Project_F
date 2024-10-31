using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public enum StageState { Battle, NonBattle }
    [SerializeField] StageState curState;
    [SerializeField] int stageNum;
    [SerializeField] int curWave;
    [SerializeField] List<int> maxWave;
    [SerializeField] MonsterManager monsterManager;
    [SerializeField] MonsterSpawnTable monsterTable;

    public int StageNum { get { return stageNum; } set { stageNum = value; } }
    public int CurWave { get { return curWave; } set { curWave = value; } }

    private void Update()
    {
        StartCoroutine(MonsterSpawnRoutine());
    }

    IEnumerator MonsterSpawnRoutine()
    {
        if(monsterManager.MonsterCount == 0 && curState == StageState.Battle)
        {
            if (curWave != maxWave[stageNum])
                monsterTable.MonsterSapwn();
            else
            {
                //클리어 여부 확인
                stageNum++;
                curWave = 0;
                yield break;
            }
        }

        curState = StageState.NonBattle;
        yield break;
    }
}
