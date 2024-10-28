using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MonsterSpawnTable : MonoBehaviour
{
    //몬스터 테이블
    const string monsterData = "https://docs.google.com/spreadsheets/d/1cqURKknVtc4HjHlWKmOfNi0SYTNzrUZoZbPl3gMIWqw/export?gid=0&format=csv";
    
    //몬스터 배치 테이블
    const string monsterSpawnData = "https://docs.google.com/spreadsheets/d/1cqURKknVtc4HjHlWKmOfNi0SYTNzrUZoZbPl3gMIWqw/export?gid=1240725374&format=csv";

    private Dictionary<int, GameObject> monster = new Dictionary<int, GameObject>();

    private void Awake()
    {
        StartCoroutine(GetDataRoutine());
    }

    IEnumerator GetDataRoutine()
    {
        UnityWebRequest requestMonsterData = UnityWebRequest.Get(monsterData);
        yield return requestMonsterData.SendWebRequest();

        string receiveText = requestMonsterData.downloadHandler.text;
        ParserToDataTable(receiveText);

        UnityWebRequest requestMonsterSpawn = UnityWebRequest.Get(monsterSpawnData);
        yield return requestMonsterSpawn.SendWebRequest();

        string reciveText = requestMonsterSpawn.downloadHandler.text;
        Debug.Log(reciveText);
    }

    private void ParserToDataTable(string data)
    {
        string[] line = data.Split('\n');
        for (int i = 1; i < line.Length; i++)
        {
            string[] datas = line[i].Split(',');
            int.TryParse(datas[0], out int id);
            GameObject obj = new GameObject(datas[1]);

            monster.Add(id, obj);
        }

        Debug.Log("end");
    }
}
