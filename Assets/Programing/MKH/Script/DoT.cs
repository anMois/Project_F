using System.Collections;
using UnityEngine;

public class DoT : MonoBehaviour
{
    [SerializeField] int lifeTime;
    [SerializeField] int dmg;
    [SerializeField] string name;
    Coroutine damageCoroutine;

    IDamageable damageable;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void OnTriggerEnter(Collider other)
    {
        //bool valid = (layerMask & (1 << other.gameObject.layer)) != 0;
        //if (!valid)
        //    return;
        bool valid = other.CompareTag(name);
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
        bool valid = other.CompareTag(name);
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
        damageCoroutine = StartCoroutine(InflictdamageOverTime());

    }

    private void EndDamage()         // 피해 끝나는 코루틴
    {
        StopCoroutine(damageCoroutine);
    }

    private IEnumerator InflictdamageOverTime()
    {
        while (lifeTime > 0)
        {
            damageable.TakeHit(dmg);        // 피해 입히는 부분
            yield return new WaitForSeconds(1f);        // 대기시간

            lifeTime -= 1;
        }
    }


    /// <summary>
    /// Time to DoT to tag name
    /// </summary>
    /// <param name="name">Tag name of the attack target</param>
    /// <param name="attackDamage">attackDamage</param>
    /// <param name="time">duration of attack</param>
    public void Damage(string name, int attackDamage, int time)       // 실제로 피해 입히는 부분
    {
        this.name = name;
        dmg = attackDamage;
        lifeTime = time;
    }
}
