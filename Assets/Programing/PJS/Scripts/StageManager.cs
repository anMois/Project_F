using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public enum StageState { Normal, Elite, Boss, Store, Clear, Choice, Potal }
    [SerializeField] InGameManager inGame;

    [Header("현재 상태, 이전 상태")]
    [SerializeField] StageState curState = StageState.Normal;
    [SerializeField] StageState preState;

    [Header("현재 스테이지, 현재 웨이브 (0 부터 시작)")]
    [SerializeField] int stageNum;
    [SerializeField] int curWave;

    [Header("스테이지별 웨이브 수")]
    [SerializeField] List<int> maxWave;
    [Header("몬스터 매니저")]
    [SerializeField] MonsterManager monsterManager;

    [Header("몬스터 생성 오브젝트")]
    [SerializeField] CreateStageMonster[] createStageMonsters;
    [SerializeField] CreateStageMonster curStageMonster;

    [Header("스테이지 클리어")]
    [SerializeField] ClearBox clearBox;
    [SerializeField] Teleport potal;

    public int StageNum { get { return stageNum; } set { stageNum = value; } }
    public int CurWave { get { return curWave; } set { curWave = value; } }
    public int LastStage { get { return maxWave.Count - 1; } }
    public StageState CurState { set { curState = value; } }
    public StageState PreState { get { return preState; } }

    private void Start()
    {
        Init();
        SelectStage();

        StartCoroutine(MonsterSpawnRoutine());
    }

    private void Init()
    {
        curWave = 0;
    }

    IEnumerator MonsterSpawnRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(1.5f);

        while (true)
        {
            yield return delay;
            //스테이지 전투 상황
            if (monsterManager.MonsterCount == 0 && (curState == StageState.Normal || curState == StageState.Elite))
            {
                // 해당 스테이지의 모든 웨이브를 진행 중일시
                //웨이브 확인 후 웨이브 증가 및 몬스터 생성
                if (curWave != maxWave[stageNum])
                {
                    curStageMonster.MonsterSpawn(curState, curWave, maxWave[stageNum]);
                    curWave++;
                    SoundManager.Instance.RoundStartSound();
                }
                // 해당 스테이지의 모든 웨이브를 다 클리어 했을시
                // 스테이지 클리어 웨이브 초기화 클리어 상자 생성
                else
                {
                    //클리어 여부 확인
                    preState = curState;
                    curState = StageState.Clear;
                    curWave = 0;
                }
            }
            else if (curState == StageState.Store)
            {
                potal.transform.position = inGame.Player.transform.position;
                preState = curState;
                curState = StageState.Choice;
            }
        }
    }

    private void Update()
    {
        if (curState == StageState.Clear)
        {
            SoundManager.Instance.ClearBoxSound();
            clearBox.transform.position = inGame.CurPlayerPoint.position;
            curState = StageState.Choice;
        }
        else if (curState == StageState.Choice && clearBox.IsOpen)
        {
            curState = StageState.Potal;
            clearBox.transform.position = Vector3.zero;
            SoundManager.Instance.TeleportSound();
            potal.transform.position = inGame.CurPlayerPoint.position;
        }
        else
        {
            clearBox.IsOpen = false;
        }
    }

    private void LateUpdate()
    {
        GameManager.Instance.StageWaveText(stageNum + 1, curWave, maxWave[stageNum], curState, preState);
    }

    /// <summary>
    /// 다음 스테이지 이동
    /// </summary>
    /// <param name="changeStage">이동할 스테이지</param>
    public void NextStage(StageState changeStage)
    {
        if(changeStage != StageState.Boss)
            stageNum++;

        curState = changeStage;
        curStageMonster = null;
        potal.transform.position = Vector3.zero;
        SelectStage();
    }

    private void SelectStage()
    {
        //스테이지들 중 현재 진행 중인 스테이지에 해당한 CreateStageMonster 찾기
        for (int i = 0; i < createStageMonsters.Length; i++)
        {
            if (createStageMonsters[i].transform.parent.gameObject == inGame.CurStage &&
                (curState == StageState.Normal || curState == StageState.Elite))
            {
                curStageMonster = createStageMonsters[i];
            }
        }
    }
}
