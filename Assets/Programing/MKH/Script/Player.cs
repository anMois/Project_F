using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IDamageable
{
    public UnityAction OnAlarm;

    [SerializeField] PlayerMover playerMover;


    [Header("Attributes")]
    [SerializeField] int maxHp;
    [SerializeField] public int curHp;

    private int friendlyLayer;

    private void Awake()
    {
        playerMover = GetComponent<PlayerMover>();
        friendlyLayer = LayerMask.GetMask("Player");

        curHp = maxHp;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            curHp--;

            if (curHp <= 0)
                return;
        }

    }

    public void TakeHit(int dmg)
    {
        OnAlarm?.Invoke();

        curHp -= dmg;
        if (curHp <= 0)
        {
            playerMover.AnimaitorPlay();
        }
    }
}
