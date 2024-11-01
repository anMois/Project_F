using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public enum StageState { Battle, NonBattle, Clear }
    [SerializeField] StageState curState;
    [SerializeField] int stageNum;

    [Header("스테이지별 웨이브 수")]
    [SerializeField] List<int> maxWave;
    [Header("몬스터 매니저")]
    [SerializeField] MonsterManager monsterManager;
    [Header("몬스터 생성 오브젝트")]
    [SerializeField] CreateStageMonster stageMonster;
    [SerializeField] GameObject clearBox;
    [SerializeField] Transform createPoint;

    private int curWave;

    public int StageNum { get { return stageNum; } set { stageNum = value; } }
    public int CurWave { get { return curWave; } set { curWave = value; } }

    private void Start()
    {
        StartCoroutine(MonsterSpawnRoutine());
    }

    IEnumerator MonsterSpawnRoutine()
    {
        while (curState != StageState.NonBattle)
        {
            yield return new WaitForSeconds(1.5f);
            if (monsterManager.MonsterCount == 0 && curState == StageState.Battle)
            {
                // 해당 스테이지의 모든 웨이브를 진행 중일시
                //웨이브 확인 후 웨이브 증가 및 몬스터 생성
                if (curWave != maxWave[stageNum])
                {
                    stageMonster.MonsterSpawn();
                    curWave++;
                }
                // 해당 스테이지의 모든 웨이브를 다 클리어 했을시
                // 스테이지 클리어 웨이브 초기화 클리어 상자 생성
                else
                {
                    //클리어 여부 확인
                    curState = StageState.Clear;
                    curWave = 0;
                }
            }

            if (curState == StageState.Clear)
            {
                Instantiate(clearBox, createPoint.position, Quaternion.identity);
                yield break;
            }
        }
    }

    private void Update()
    {
        
    }
}
