using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour
{
    public BoxCollider2D box;
    public Rigidbody2D rigid;
    private Vector2 boxsize;
    private Vector3 center;

    public PlayerOnewayPlatformEffector pltEffector;






    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponentInParent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();

        boxsize = box.size;
        pltEffector = GetComponentInChildren<PlayerOnewayPlatformEffector>();


    }

    // Update is called once per frame
    void Update()
    {
        center = transform.position;
        center.y += box.offset.y - 0.05f;
        Collider2D outputCol = Physics2D.OverlapBox(center, boxsize, 0.0f, LayerMask.GetMask("Ground"));
        Collider2D pointColliderA =
            Physics2D.OverlapPoint(new Vector2(center.x - 0.5f * boxsize.x, center.y - 0.5f * boxsize.y),
            LayerMask.GetMask("Platforms"));

        Collider2D pointColliderB =
            Physics2D.OverlapPoint(new Vector2(center.x + 0.5f * boxsize.x, center.y - 0.5f * boxsize.y),
            LayerMask.GetMask("Platforms"));


        if (outputCol != null || (pointColliderA!=null || pointColliderB!=null) && rigid.velocity.y==0)
        {
            SendMessageUpwards("IsGround");
        }
        else{

            SendMessageUpwards("IsNotGround");

        }


    }
}
