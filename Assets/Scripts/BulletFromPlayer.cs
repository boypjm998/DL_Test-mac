using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFromPlayer : AttackFromPlayer
{
    public float speed;
    public float lifeTime;
    public float acceleration;
    public float hitRayDistance;


    public LayerMask targetLayers;
    public RaycastHit2D hitinfo;


    private void Awake()
    {
        Invoke(nameof(DestroySelf), lifeTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hitinfo = Physics2D.Raycast(transform.position, transform.right, hitRayDistance, targetLayers);

        if (hitinfo.collider != null)
        {
            if (hitinfo.collider.CompareTag("Enemy"))
            {
                PlayHitConnectEffect(hitShakeIntensity);
                DestroySelf();
                hitinfo.collider.GetComponentInParent<Enemy>().ReceiveHit();
            }

        }

        transform.Translate(Vector2.right * speed * acceleration);
        //speed += acceleration * Time.fixedDeltaTime;

    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    
}
