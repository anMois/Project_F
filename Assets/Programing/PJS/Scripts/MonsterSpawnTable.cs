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
    private List<int> monsterKey = new List<int>();
    private List<string> monsterList = new List<string>();

    [Header("생성되는 몬스터의 부모가 되는 오브젝트")]
    [SerializeField] Transform monsterParent;
    [Header("생성되는 몬스터")]
    [ SerializeField] List<GameObject> monsterPrefabs;
    [Header("생성되는 몬스터의 위치")]
    [SerializeField] List<Transform> monsterPoints;

    private void Awake()
    {
        StartCoroutine(GetDataRoutine());
    }

    IEnumerator GetDataRoutine()
    {
        UnityWebRequest requestMonsterData = UnityWebRequest.Get(monsterData);
        yield return requestMonsterData.SendWebRequest();

        string receiveText = requestMonsterData.downloadHandler.text;
        ParserToMonsterData(receiveText);

        UnityWebRequest requestMonsterSpawn = UnityWebRequest.Get(monsterSpawnData);
        yield return requestMonsterSpawn.SendWebRequest();

        string reciveText = requestMonsterSpawn.downloadHandler.text;
        ParserToMosterSpawnData(reciveText);
        Debug.Log(reciveText);
    }

    private void ParserToMonsterData(string data)
    {
        string[] line = data.Split('\n');
        for (int i = 1; i < line.Length; i++)
        {
            Debug.Log(line[i]);
            string[] datas = line[i].Split(',');
            int.TryParse(datas[0], out int id);
            Debug.Log(datas[1]);
            if (i != line.Length - 1)
            {
                datas[1] = datas[1].Remove(datas[1].IndexOf('\r'));
            }
            GameObject obj = FindGameObject(datas[1]);

            monster.Add(id, obj);
        }

        foreach (KeyValuePair<int, GameObject> item in monster)
        {
            monsterKey.Add(item.Key);
            Debug.Log($"{item.Key} / {item.Value}");
        }
    }

    private GameObject FindGameObject(string name)
    {
        for (int i = 0; i < monsterPrefabs.Count; i++)
        {
            if (monsterPrefabs[i].name == name)
            {
                Debug.Log(monsterPrefabs[i]);
                return monsterPrefabs[i];
            }
        }

        return null;
    }

    private void ParserToMosterSpawnData(string data)
    {
        string[] line = data.Split('\n');
        for (int i = 1; i < line.Length; i++)
        {
            monsterList.Add(line[i]);
        }

        for (int i = 0; i < monsterList.Count; i++)
        {
            Debug.Log(monsterList[i]);
        }

        MonsterSapwn();
    }

    private void MonsterSapwn()
    {
        int num = Random.Range(0, monsterList.Count - 1);
        Debug.Log(num);
        string[] point = monsterList[num].Split(',');

        for (int i = 0; i < point.Length; i++)
        {
            int.TryParse(point[i], out int id);
            for (int j = 0; j < monsterKey.Count; j++)
            {
                if (monsterKey[j] == id)
                {
                    Instantiate(monster[id], monsterPoints[i].position, monsterPoints[i].rotation);
                    Debug.Log($"오브젝트 생성 {id}");
                }
                else
                    Debug.Log("빈공간");
            }
        }
    }
}
