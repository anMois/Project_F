using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    /*
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;

    PlayerAttack attack;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        attack = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
    }

    private void Fire()
    {
        Vector3 force = Vector3.forward * speed;
        rb.AddForce(force, ForceMode.Impulse);
    }
    */

    [Tooltip("Interactable layers")]
    [SerializeField] LayerMask layerMask;
    [Tooltip("Limit time since projectile launched")]
    [SerializeField] float lifeTime;
    [SerializeField] float speed;
    [SerializeField] int dmg;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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

        Vector3 direction = (target.position - transform.position).normalized;
        //rb.velocity = direction * speed;
        rb.AddForce(direction * speed, ForceMode.Impulse);

        Destroy(gameObject, lifeTime);
    }
}
