using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MonsterData : MonoBehaviour
{
    //몬스터 테이블
    const string monsterData = "https://docs.google.com/spreadsheets/d/1cqURKknVtc4HjHlWKmOfNi0SYTNzrUZoZbPl3gMIWqw/export?gid=0&format=csv";

    //몬스터 배치 테이블
    const string monsterSpawnData = "https://docs.google.com/spreadsheets/d/1cqURKknVtc4HjHlWKmOfNi0SYTNzrUZoZbPl3gMIWqw/export?gid=1240725374&format=csv";

    private Dictionary<int, GameObject> monster = new Dictionary<int, GameObject>();    // 몬스터를 저장할 딕셔너리
    private List<int> monsterKey = new List<int>();         // 저장된 몬스터들의 ID
    private List<string> monsterList = new List<string>();  // 몬스터의 배치 리스트
    private bool[] monsterListActive;

    public Dictionary<int, GameObject> Monster { get { return monster; } }
    public List<int> MonsterKey { get { return monsterKey; } }
    public List<string> MonsterList { get { return monsterList; } }
    public bool[] MonsterListActive { get { return monsterListActive; } }

    [Header("생성되는 몬스터")]
    [SerializeField] List<GameObject> monsterPrefabs;

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
        monsterListActive = new bool[monsterList.Count];
        yield break;
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
    }
}
