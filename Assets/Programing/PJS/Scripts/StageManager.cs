using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public enum StageState { Battle, NonBattle, Clear, Choice }
    [SerializeField] StageState curState;
    [SerializeField] int stageNum;

    [Header("스테이지별 웨이브 수")]
    [SerializeField] List<int> maxWave;
    [Header("몬스터 매니저")]
    [SerializeField] MonsterManager monsterManager;
    [Header("몬스터 생성 오브젝트 (드래드 인 드롭 X)")]
    [SerializeField] CreateStageMonster curStageMonster;
    [Header("스테이지 클리어 보상 상자")]
    [SerializeField] ClearBox clearBox;
    [SerializeField] Teleport potal;
    [SerializeField] InGameManager inGame;

    private int curWave;
    [SerializeField] CreateStageMonster[] createStageMonsters;

    public int StageNum { get { return stageNum; } set { stageNum = value; } }
    public int CurWave { get { return curWave; } set { curWave = value; } }
    public StageState CurState { set { curState = value; } }

    private void Awake()
    {
        createStageMonsters = FindObjectsOfType<CreateStageMonster>();
    }

    private void Start()
    {
        SelectCreateStage();

        StartCoroutine(MonsterSpawnRoutine());
    }

    IEnumerator MonsterSpawnRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(1.5f);

        while (true)
        {
            yield return delay;
            //스테이지 전투 상황
            if (monsterManager.MonsterCount == 0 && curState == StageState.Battle)
            {
                // 해당 스테이지의 모든 웨이브를 진행 중일시
                //웨이브 확인 후 웨이브 증가 및 몬스터 생성
                if (curWave != maxWave[stageNum])
                {
                    curStageMonster.MonsterSpawn();
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
            //스테이지 비전투 상황
            else
            {
                yield return null;
            }
        }
    }

    private void Update()
    {
        if(curState == StageState.Clear)
        {
            clearBox.transform.position = inGame.CurPlayerPoint.position;
            curState = StageState.Choice;
        }
        else if (curState == StageState.Choice && clearBox.IsOpen)
        {
            clearBox.transform.position = Vector3.zero;
            potal.transform.position = inGame.CurPlayerPoint.position;
        }
    }

   /// <summary>
   /// 스테이지 이동
   /// </summary>
   /// <param name="changeStage">이동한 스테이지</param>
    public void NextStage(StageState changeStage)
    {
        stageNum++;
        curState = changeStage;
        SelectCreateStage();
    }

    private void SelectCreateStage()
    {
        //스테이지들 중 현재 진행 중인 스테이지에 해당한 CreateStageMonster 찾기
        for (int i = 0; i < createStageMonsters.Length; i++)
        {
            if (createStageMonsters[i].transform.parent.gameObject == inGame.CurStage)
            {
                curStageMonster = createStageMonsters[i];
            }
            else
            {
                createStageMonsters[i].transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
