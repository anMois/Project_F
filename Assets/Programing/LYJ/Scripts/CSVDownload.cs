using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CSVDownload : MonoBehaviour
{
    public static CSVDownload Instance { get; private set; }

    const string urlPath = "https://docs.google.com/spreadsheets/d/1DdyytW9508YQYY1_63fVf_bZNvDM7thHC7nBM7M4X6M/export?format=csv";

    public GameObject[] mainButtons;
    public GameObject[] buttons;

    public List<ItemData> itemDataList { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        itemDataList = new List<ItemData>(); //아이템데이터 저장 리스트
        StartCoroutine(DownloadRoutine());
    }

    //CSV 파일 다운로드
    public IEnumerator DownloadRoutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(urlPath);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string receiveText = request.downloadHandler.text;
            List<ItemData> itemDataList = ParseCSV(receiveText); //CSV 데이터를 파싱하여 아이템 데이터 리스트로 변환

            SetItemData(itemDataList);
        }
    }

    //CSV 파일의 텍스트 데이터를 ItemData 리스트로 변환
    private List<ItemData> ParseCSV(string csvText)
    {
        List<ItemData> itemDataList = new List<ItemData>();
        StringReader reader = new StringReader(csvText);
        bool headerSkipped = false;

        //CSV파일의 모든 내용을 읽고
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();

            //첫번째 줄 헤더로 건너뜀
            if (!headerSkipped)
            {
                headerSkipped = true;
                continue;
            }

            //각 줄을 ,로 나누어서 배열로 만듦
            string[] columns = line.Split(',');
            if (columns.Length >= 13)
            {
                ItemData itemData = new ItemData();
                itemData.itemName = columns[0];
                itemData.description = columns[1];
                itemData.price = float.Parse(columns[3]);
                itemData.itemImage = Resources.Load<Sprite>(columns[2]);
                itemData.elemental = int.Parse(columns[12]);

                itemDataList.Add(itemData); //필요한 정보가 모두 있으면 아이템 데이터 리스트에 아이템 추가
            }
        }
        return itemDataList;
    }

    //아이템 데이터를 버튼에 설정
    public void SetItemData(List<ItemData> itemDataList)
    {
        this.itemDataList = itemDataList;

        HashSet<int> usedIndices = new HashSet<int>(); //이미 사용된 인덱스를 기록할 HashSet 생성

        //순서대로 버튼에 아이템 데이터 설정함 (랜덤 X)
        for (int i = 0; i < mainButtons.Length; i++)
        {
            if (i >= itemDataList.Count) //리스트에 아이템이 없으면 멈춤
                break;

            ItemData itemData = itemDataList[i];

            //ItemButton 컴포넌트를 찾고 데이터를 설정함
            ItemButton itemButton = mainButtons[i].GetComponent<ItemButton>();
            if (itemButton != null)
            {
                itemButton.itemData = itemData;
            }
        }

        //랜덤으로 버튼에 데이터를 설정함
        foreach (GameObject buttonObject in buttons)
        {
            if (usedIndices.Count >= itemDataList.Count) //모든 아이템이 사용되었으면 멈춤
                break;

            int randomIndex;

            do //중복되지 않은 인덱스를 찾음 (반복해서)
            {
                randomIndex = Random.Range(0, itemDataList.Count);
            } while (usedIndices.Contains(randomIndex));

            usedIndices.Add(randomIndex);

            ItemData itemData = itemDataList[randomIndex];

            //StoreItemButton 컴포넌트를 찾고 데이터 설정
            StoreItemButton storeItemButton = buttonObject.GetComponent<StoreItemButton>();
            if (storeItemButton != null)
            {
                storeItemButton.itemData = itemData;
            }

            //Store Canvas의 ItemPanel 객체를 찾음
            GameObject itemPanel = GameObject.Find($"Store Canvas/Random Store Item/ItemPanel{usedIndices.Count}");
            if (itemPanel == null) //ItemPanel이 없을 경우 그냥 다음으로 넘어감
            {
                continue; //사용하는 이유는 앞에 itemButton에서는 itemPanel을 사용하지 않음
            }

            //ItemPanel 내의 버튼을 찾음
            Button itemNameButton = itemPanel.transform.Find("Item Name Button")?.GetComponent<Button>();
            if (itemNameButton != null)
            {
                TextMeshProUGUI itemNameText = itemNameButton.GetComponentInChildren<TextMeshProUGUI>();
                if (itemNameText != null)
                {
                    itemNameText.text = itemData.itemName;
                }
            }

            //ItemPanel 내의 이미지를 찾음
            Image itemImageComponent = itemPanel.transform.Find("Item Image")?.GetComponent<Image>();
            if (itemImageComponent != null)
            {
                itemImageComponent.sprite = itemData.itemImage;
            }

            //ItemPanel 내의 가격을 찾아서 읽음
            TextMeshProUGUI itemGold = itemPanel.transform.Find("Gold")?.GetComponent<TextMeshProUGUI>();
            if (itemGold != null)
            {
                itemGold.text = itemData.price.ToString();
            }
        }
    }


}
