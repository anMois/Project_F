using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CSVDownload : MonoBehaviour
{
    const string urlPath = "https://docs.google.com/spreadsheets/d/1DdyytW9508YQYY1_63fVf_bZNvDM7thHC7nBM7M4X6M/export?format=csv";

    public GameObject[] buttons;

    private void Awake()
    {
        StartCoroutine(DownloadRoutine());
    }

    IEnumerator DownloadRoutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(urlPath);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string receiveText = request.downloadHandler.text;
            List<ItemData> itemDataList = ParseCSV(receiveText);

            SetItemData(itemDataList);
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
        HashSet<int> usedIndices = new HashSet<int>();

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i >= itemDataList.Count)
                break;

            ItemData itemData = itemDataList[i];

            ItemButton itemButton = buttons[i].GetComponent<ItemButton>();
            if (itemButton != null)
            {
                itemButton.itemData = itemData;
            }
        }

        foreach (GameObject buttonObject in buttons)
        {
            if (usedIndices.Count >= itemDataList.Count)
                break;

            int randomIndex;

            do
            {
                randomIndex = Random.Range(0, itemDataList.Count);
            } while (usedIndices.Contains(randomIndex));

            usedIndices.Add(randomIndex);

            ItemData itemData = itemDataList[randomIndex];

            StoreItemButton storeItemButton = buttonObject.GetComponent<StoreItemButton>();
            if (storeItemButton != null)
            {
                storeItemButton.itemData = itemData;
            }

            GameObject itemPanel = GameObject.Find($"Store Canvas/Random Store Item/ItemPanel{usedIndices.Count}");
            if (itemPanel == null)
            {
                continue; //사용하는 이유는 앞에 itemButton에서는 itemPanel을 사용하지 않음
            }

            Button itemNameButton = itemPanel.transform.Find("Item Name Button")?.GetComponent<Button>();
            if (itemNameButton != null)
            {
                TextMeshProUGUI itemNameText = itemNameButton.GetComponentInChildren<TextMeshProUGUI>();
                if (itemNameText != null)
                {
                    itemNameText.text = itemData.itemName;
                }
            }

            Image itemImageComponent = itemPanel.transform.Find("Item Image")?.GetComponent<Image>();
            if (itemImageComponent != null)
            {
                itemImageComponent.sprite = itemData.itemImage;
            }

            TextMeshProUGUI itemGold = itemPanel.transform.Find("Gold")?.GetComponent<TextMeshProUGUI>();
            if (itemGold != null)
            {
                itemGold.text = itemData.price.ToString();
            }
        }
    }


}
