using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnewayPlatformEffector : MonoBehaviour
{
    public GameObject currentOnewayPlatform;
    private Rigidbody2D rigid;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private BoxCollider2D playerCollider;
    public PlayerInput pi;


    private void Awake()
    {
        playerCollider = GetComponentInChildren<BoxCollider2D>();
        pi = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody2D>();
        anim = rigid.GetComponent<Animator>();


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pi.buttonDown.IsPressing)
        {
            if (currentOnewayPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            currentOnewayPlatform = collision.gameObject;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            currentOnewayPlatform = null;

        }
    }

    private IEnumerator DisableCollision()
    {
        Collider2D platformCollider = currentOnewayPlatform.GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(0.3f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }

}
