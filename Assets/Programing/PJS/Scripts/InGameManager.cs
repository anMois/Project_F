using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    [Header("플레이어 시작 위치")]
    [SerializeField] List<Transform> playerPoints;  //플레이어 시작 위치
    [Header("전투 스테이지")]
    [SerializeField] List<GameObject> stages;       //생성되는 스테이지

    [Header("씬에 존재하는 플레이어, 시작 존")]
    [SerializeField] GameObject player;             //플레이어
    [SerializeField] GameObject startZone;          //스테이지 플레이어 안전구역

    private int stageNum;
    public int StageNum { get { return stageNum; } }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        RandomPoint();
    }

    private void Start()
    {
        for (int i = 0; i < stages.Count; i++)
        {
            if (i != stageNum)
                stages[i].SetActive(false);
        }
    }

    private void RandomPoint()
    {
        stageNum = Random.Range(0, stages.Count);
        
        StageMovePosition(player.transform, startZone.transform, stageNum);
    }

    private void StageMovePosition(Transform player, Transform lifeZone, int num)
    {
        player.position = playerPoints[num].position;
        lifeZone.position = playerPoints[num].position;
    }
}
