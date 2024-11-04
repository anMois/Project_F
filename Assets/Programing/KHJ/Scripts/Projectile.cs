using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
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

        IDamageable damageable = null;
        Transform curTransform = other.transform;
        // Find IDamageable through parents
        while (curTransform != null)
        {
            damageable = curTransform.GetComponent<IDamageable>();
            if (damageable != null)
                break;
            curTransform = curTransform.parent;
        }

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
    public void Launch(int friendlyLayer, Transform target, int attackaDamage, float launchSpeed = 0)
    {
        // Ignore friendly layer
        layerMask &= ~friendlyLayer;

        dmg = attackaDamage;
        if (launchSpeed > 0)
            speed = launchSpeed;

        // Fire
        gameObject.SetActive(true);
        Vector3 direction = (target.position - transform.position).normalized;
        //rb.velocity = direction * speed;
        rb.AddForce(direction * speed, ForceMode.Impulse);

        Destroy(gameObject, lifeTime);
    }
}
