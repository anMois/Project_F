using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Player : MonoBehaviour, IDamageable
{
    [SerializeField] int maxHp;
    [SerializeField] int curHp;

    private void Awake()
    {
        curHp = maxHp;
    }

    public void TakeHit(int dmg)
    {
        curHp -= dmg;
        if (curHp < 0)
        {
            Destroy(gameObject);
        }
    }
}
