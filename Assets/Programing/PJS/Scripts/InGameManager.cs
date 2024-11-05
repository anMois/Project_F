using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    [Header("플레이어 시작 위치(마지막은 보스스테이지)")]
    [SerializeField] List<Transform> playerPoints;  //플레이어 시작 위치
    [Header("상점, 모닥불 스테이지")]
    [SerializeField] Transform store;               //상점
    [SerializeField] Transform bonfire;             //모닥불
    [Header("전투 스테이지")]
    [SerializeField] List<GameObject> stages;       //생성되는 스테이지

    [Header("씬에 존재하는 플레이어, 시작 안전구역, 보스 드래곤")]
    [SerializeField] GameObject player;             //플레이어
    [SerializeField] GameObject startZone;          //스테이지 플레이어 안전구역
    [SerializeField] GameObject boss;               //보스 드래곤

    private int stageNum;

    public Transform CurPlayerPoint { get { return playerPoints[stageNum]; } }
    public GameObject CurStage { get { return stages[StageNum]; } }
    public int StageNum { get { return stageNum; } }
    public GameObject Player { get { return player; } }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boss.SetActive(false);
        RandomStagePoint();
    }

    /// <summary>
    /// 랜덤한 전투 스테이지 이동
    /// </summary>
    public void RandomStagePoint()
    {
        stageNum = Random.Range(0, stages.Count);
        
        StageMovePosition(player.transform, startZone.transform, stageNum);
    }

    private void StageMovePosition(Transform player, Transform lifeZone, int num)
    {
        player.position = playerPoints[num].position;
        lifeZone.position = playerPoints[num].position;
        lifeZone.GetComponent<SphereCollider>().enabled = true;
    }

    /// <summary>
    /// 상점 또는 모닥불 스테이지 이동
    /// </summary>
    /// <param name="player">플레이어</param>
    /// <param name="choice">상점or모닥불</param>
    public void StoreOrBonfirePosition(Transform player, bool choice)
    {
        if (choice)
        {
            player.position = store.position;
            SoundManager.Instance.StoreOpenSound();
        }
        else
        {
            player.position = bonfire.position;
        }
    }

    /// <summary>
    /// 보스 스테이지 이동
    /// </summary>
    /// <param name="player">플에이어</param>
    public void BossStagePosition(Transform player)
    {
        player.position = playerPoints[playerPoints.Count - 1].position;
        boss.SetActive(true);
    }
}
