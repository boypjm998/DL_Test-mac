using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFromPlayer : MonoBehaviour
{
    private float knockbackPower;
    private float knockbackForce;
    private float dmgModifier;
    private int spGain;
    private int firedir;



    public List<int> hitFlags;


    public GameObject hitConnectEffect;
    public Collider2D attackCollider;
    public Transform playerpos;
    static int DEFAULT_GRAVITY = 4;
    public float hitShakeIntensity;
    




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual int GetFireDir()
    {
        return firedir;
    }

    public virtual void InitAttackBasicAttributes(float knockbackPower,float knockbackForce, float dmgModifier, int spGain, int firedir)
    {
        this.knockbackPower = knockbackPower;
        this.dmgModifier = dmgModifier;
        this.spGain = spGain;
        this.firedir = firedir;
        this.knockbackForce = knockbackForce;
    }

    public virtual void PlayHitConnectEffect(float shakeIntensity)
    
    {
        GameObject eff = Instantiate(hitConnectEffect, transform.position, Quaternion.identity);
        CinemachineOperator.Instance.CameraShake(shakeIntensity, .1f);
    }

}
