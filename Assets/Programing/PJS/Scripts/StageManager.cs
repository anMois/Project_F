using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] int stageNum;
    [SerializeField] int waveNum;
    [SerializeField] List<string> monsterPlace;
    [SerializeField] MonsterManager monsterManager;

    public int WaveNum {  get { return waveNum; } set { waveNum = value; } }
    public List<string> MonsterPlace { get { return monsterPlace; } set { monsterPlace = value; } }

    private void Update()
    {
        if (monsterManager.MonsterCount == 0)
        {
            
        }
    }
}
