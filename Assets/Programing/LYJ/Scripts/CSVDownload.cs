using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class CSVDownload : MonoBehaviour
{
    const string urlPath = "https://docs.google.com/spreadsheets/d/1DdyytW9508YQYY1_63fVf_bZNvDM7thHC7nBM7M4X6M/export?format=csv";

    public MainController mainController; //MainController 참조 추가
    public GameObject[] buttons; //버튼 배열 추가

    private void Awake()
    {
        mainController = GetComponent<MainController>();
        StartCoroutine(DownloadRoutine());
    }

    IEnumerator DownloadRoutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(urlPath);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string receiveText = request.downloadHandler.text;
            List<ItemData> itemDataList = ParseCSV(receiveText); //CSV 데이터 파싱

            //MainController로 아이템 데이터 전달
            SetItemData(itemDataList);
        }
        else
        {
            Debug.LogError("Failed to download CSV: " + request.error);
        }
    }

    private List<ItemData> ParseCSV(string csvText)
    {
        List<ItemData> itemDataList = new List<ItemData>();
        StringReader reader = new StringReader(csvText);
        bool headerSkipped = false;

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();

            if (!headerSkipped)
            {
                headerSkipped = true;
                continue;
            }

            string[] columns = line.Split(',');
            if (columns.Length >= 4)
            {
                ItemData itemData = new ItemData();
                itemData.itemName = columns[0];
                itemData.description = columns[1];
                itemData.price = float.Parse(columns[3]);
                itemData.itemImage = Resources.Load<Sprite>(columns[2]);

                itemDataList.Add(itemData);
            }
        }
        return itemDataList;
    }

    private void SetItemData(List<ItemData> itemDataList)
    {
        foreach (ItemData itemData in itemDataList)
        {
            foreach (GameObject buttonObject in buttons)
            {
                if (buttonObject.name == itemData.itemName)
                {
                    ItemButton itemButton = buttonObject.GetComponent<ItemButton>();
                    if (itemButton != null)
                    {
                        itemButton.itemData = itemData;
                    }
                }
            }
        }
    }
}
