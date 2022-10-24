using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int enemyID;







    private GameObject flash;
    private SpriteRenderer spriteRenderer;
    
    private Coroutine hurtEffectCoroutine;
    
    [SerializeField]
    private float hurtEffectDuration;

    // Start is called before the first frame update
    void Start()
    {
        flash = transform.Find("Flash").gameObject;
        spriteRenderer = flash.GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void ReceiveHit()
    {
        Flash();
    }

    public virtual void Flash()
    {
        if (hurtEffectCoroutine != null)
        {
            StopCoroutine(hurtEffectCoroutine);
        }
        hurtEffectCoroutine = StartCoroutine(HurtEffectCoroutine());
    }


    private IEnumerator HurtEffectCoroutine()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 100);
        yield return new WaitForSeconds(hurtEffectDuration);
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
        hurtEffectCoroutine = null;
    }

}
