using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour, IInteractable
{
    private InGameController storeController;

    private void Start()
    {
        storeController = FindObjectOfType<InGameController>();
    }

    public void Interact()
    {
        if (GameManager.Instance.currentHealth < GameManager.Instance.maxHealth * 0.7f)
        {
            GameManager.Instance.Heal(0.7f);
            Debug.Log("체력이 회복되었습니다.");
        }
    }
}