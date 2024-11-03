using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject explosionFactiory;
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

        if (other.CompareTag("Monster"))
        {
            damageable = other.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeHit(dmg);
            }

        }

        if (other.CompareTag("Ground"))
        {
            Debug.Log(other + "Ground");
            GameObject exp = Instantiate(explosionFactiory);
            exp.transform.position = transform.position;
            Destroy(exp, 1);

            Collider[] cols = Physics.OverlapSphere(exp.transform.position, exp.transform.localScale.z / 2);
            for (int i = 0; i < cols.Length; i++)
            {
                Debug.Log(cols[i].name);
                if (cols[i].gameObject.tag == "Monster")
                {
                    damageable = cols[i].GetComponent<IDamageable>();

                    damageable.TakeHit(dmg);
                    //Debug.Log(damageable);
                    //Destroy(cols[i].gameObject);
                    //Debug.Log("---");
                }
            }

            Destroy(gameObject);
        }
    }

}
