using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] GameObject[] bulletPrefab;
    private GameObject curBullet;

    [SerializeField] float speed = 10f;
    [SerializeField] float length;
    [SerializeField] float range;

    [SerializeField] PlayerAttack attack;

    private void Awake()
    {
        curBullet = bulletPrefab[0];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwapBullet(0);
        } 
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwapBullet(1);
        } 
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwapBullet(2);
        }
    }

    public void SwapBullet(int index)
    {
        curBullet = bulletPrefab[index];
    }
   
    private void Fire()
    {
        Instantiate(curBullet, attack.attackPos.position, attack.attackPos.rotation);
    }
}
