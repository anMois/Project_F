using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_StageManager : MonoBehaviour
{
    [SerializeField] int stageNum;
    [SerializeField] int curWave;

    public int StageNum { get { return stageNum; } set { stageNum = value; } }
    public int CurWave { get { return curWave; } set { curWave = value; } }
}
