using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;

public class Bomb : MonoBehaviour
{
    [SerializeField] public GameObject explosionFactory;
    [SerializeField] Transform target;
    [SerializeField] string naming;
    [SerializeField] float speed;
    [SerializeField] int dmg;

    Rigidbody rb;
    IDamageable damageable;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject exp = Instantiate(explosionFactory);
        exp.transform.position = transform.position;
        Destroy(exp, 1);

        if (other.CompareTag(naming))
        {
            Collider[] cols = Physics.OverlapSphere(exp.transform.position, exp.transform.localScale.z / 2);
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].gameObject.tag == naming)
                {
                    damageable = cols[i].GetComponent<IDamageable>();
                    if (damageable != null)
                    {
                        damageable.TakeHit(dmg);
                    }
                }
            }

            Destroy(gameObject);

        }
        
        if (other.CompareTag("Ground"))
        {
            Debug.Log(other + "Ground");

            Collider[] cols = Physics.OverlapSphere(exp.transform.position, exp.transform.localScale.z / 2);
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].CompareTag(naming))
                {
                    damageable = cols[i].GetComponent<IDamageable>();
                    damageable.TakeHit(dmg);
                }


            }

            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Damage to  Tag name
    /// </summary>
    /// <param name="name">Tag name of the attack target</param>
    /// <param name="attackDamage">attackDamage</param>
    public void Fire(string name, int attackDamage)
    {
        naming = name;
        dmg = attackDamage;
    }
}
