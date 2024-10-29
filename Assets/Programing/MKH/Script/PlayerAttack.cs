using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] public Transform attackPos;
    [SerializeField] float attackSpeed;
    [SerializeField] float lineSize;
    [SerializeField] int time;
    [SerializeField]BulletManager bulletManager;


    public int curAniHash { get; private set; }


    //private void Start()
    //{
    //    bulletManager = GetComponent<BulletManager>();
    //}

    private void Update()
    {
        
    }
}
