using System.Collections;
using UnityEngine;

public class DoT : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] int lifeTime;
    [SerializeField] public int dmg;
    [SerializeField] public int dmgs;
    Coroutine damageCoroutine;

    IDamageable damageable;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void OnTriggerEnter(Collider other)
    {
        // layermask 는 투사체에 부닺힐 레이어 
        //bool valid = (layerMask & (1 << other.gameObject.layer)) != 0;
        //if (!valid)
        //    return;
        bool valid = other.CompareTag("Monster");
        if (!valid)
            return;

        damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            StartDamage();
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // XXX: 레이어마스크 연산식 오기입
        //bool valid = (layerMask & (1 << other.gameObject.layer)) != 0;
        //if (!valid)
        //    return;
        bool valid = other.CompareTag("Monster");
        if (!valid)
            return;

        damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            EndDamage();
        }
    }

    private void StartDamage()      //피해 입히는 코루틴
    {
        damageCoroutine = StartCoroutine(InflictdamageOverTime(dmg));

    }

    private void EndDamage()         // 피해 끝나는 코루틴
    {

        StopCoroutine(damageCoroutine);
    }

    private IEnumerator InflictdamageOverTime(int damage)
    {
        while (lifeTime > 0)
        {
            Damage(layerMask, dmg);        // 피해 입히는 부분
            damageable.TakeHit(dmg);
            yield return new WaitForSeconds(1f);        // 대기시간

            lifeTime -= 1;
        }

    }

    public void Damage(int friendlyLayer, int attackDamage)       // 실제로 피해 입히는 부분
    {
        rb.position = Vector3.zero;
        layerMask &= ~friendlyLayer;

        dmg = attackDamage;
    }

    private void FIre()
    {

    }
}
