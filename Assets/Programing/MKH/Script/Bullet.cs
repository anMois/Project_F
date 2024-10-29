using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float speed;

    public void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
