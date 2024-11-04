using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    [Header("플레이어 시작 위치")]
    [SerializeField] List<Transform> playerPoints;  //플레이어 시작 위치
    [Header("상점")]
    [SerializeField] Transform store;
    [Header("모닥불")]
    [SerializeField] Transform bonfire;
    [Header("전투 스테이지")]
    [SerializeField] List<GameObject> stages;       //생성되는 스테이지

    [Header("씬에 존재하는 플레이어, 시작 존")]
    [SerializeField] GameObject player;             //플레이어
    [SerializeField] GameObject startZone;          //스테이지 플레이어 안전구역

    [SerializeField] int stageNum;

    public Transform CurPlayerPoint { get { return playerPoints[stageNum]; } }
    public GameObject CurStage { get { return stages[StageNum]; } }
    public int StageNum { get { return stageNum; } }
    public GameObject Player { get { return player; } }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        RandomStagePoint();
    }

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

    public void StoreOrBonfirePosition(Transform player, bool choice)
    {
        if (choice)
            player.position = store.position;
        else
            player.position = bonfire.position;
    }
}
