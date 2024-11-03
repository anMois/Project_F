using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    [SerializeField] List<Transform> playerPoints;  //플레이어 생성 위치
    [SerializeField] List<GameObject> stages;       //생성되는 스테이지

    [SerializeField] GameObject curStage;           //현제 스테이지
    [SerializeField] GameObject player;             //플레이어
    [SerializeField] GameObject startZone;          //스테이지 플레이어 안전구역

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        RandomPoint();
    }

    private void RandomPoint()
    {
        int num = Random.Range(0, stages.Count);
        stages[num].SetActive(true);
        curStage = stages[num];
        //Instantiate(playerPrefab, playerPoints[num].position, Quaternion.identity);
        //Instantiate(startZone, playerPoints[num].position, Quaternion.identity);
        player.transform.position = playerPoints[num].position;
        startZone.transform.position = playerPoints[num].position;
    }
}
