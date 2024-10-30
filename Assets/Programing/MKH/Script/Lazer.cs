using UnityEngine;

public class Lazer : MonoBehaviour
{
    /*
    [SerializeField] GameObject lazer;
    [SerializeField] float length;
    [SerializeField] float range;

    PlayerAttack attack;


    private void Start()
    {
        attack = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
    }
    private void Update()
    {

        Next();

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Destroy(lazer, 5f);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Destroy(lazer);
        }

    }

    private void Next()
    {
        lazer.transform.localScale = new Vector3(1 + range, 1 + range, 1 + length);
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (length >= 0 && length < 3)
            {
                length++;
            }
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (range >= 0 && range < 3)
            {
                range++;
            }
        }
    }

    private void Follow()
    {
        transform.position = attack.attackPos.position;
    }
    */

    [Tooltip("Interactable layers")]
    [SerializeField] LayerMask layerMask;
    [Tooltip("Limit time since projectile launched")]
    [SerializeField] float lifeTime;
    [SerializeField] float length;
    [SerializeField] float range;
    [SerializeField] int dmg;

    PlayerAttack attack;

    private void Awake()
    {
        attack = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        bool valid = (layerMask & (1 << other.gameObject.layer)) != 0;
        if (!valid)
            return;

        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeHit(dmg);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Launch projectile to target
    /// </summary>
    /// <param name="friendlyLayer">layer of shooter's friendly</param>
    /// <param name="target">target to set projectile direction</param>
    public void Launch(int friendlyLayer, Transform target, int attackaDamage)
    {
        // Ignore friendly layer
        layerMask &= ~friendlyLayer;

        dmg = attackaDamage;

        // Fire
        Vector3 direction = (target.position - transform.position).normalized;
        //transform.localScale = new Vector3(1 + range, 1 + range, 1 + length);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Destroy(gameObject, lifeTime);
        }
        else if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            Destroy(gameObject);
        }
    }

}
