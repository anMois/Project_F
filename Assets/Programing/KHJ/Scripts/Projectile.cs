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

    private void OnCollisionEnter(Collision collision)
    {
        bool valid = (layerMask & collision.gameObject.layer) == collision.gameObject.layer;
        if (!valid)
            return;

        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        Debug.Log($"Collision with {collision.gameObject.name}");
        if (damageable != null)
        {
            damageable.TakeHit(dmg);
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
    public void Launch(int friendlyLayer, Transform target)
    {
        // Ignore friendly layer
        layerMask &= ~friendlyLayer;

        // Fire
        gameObject.SetActive(true);
        Vector3 direction = (transform.position - target.position).normalized;
        rb.velocity = direction * speed;

        Destroy(gameObject, lifeTime);
    }
}
