using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager_c001 : MonoBehaviour
{
    [Header("AttackFXPlayers")]
    [SerializeField]
    private GameObject Shotpoints;
    [SerializeField]
    private GameObject RangedAttackFXLayer;


    [Header("Projectiles")]
    public GameObject projectile1;//roll
    public GameObject projectile2;//combo1
    public GameObject projectile3;//dash


    public Transform shotpoint;
    public GameObject attackContainer;

    // Start is called before the first frame update
    void Start()
    {
        //rollAttack();
    }

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RollAttack()
    {
        GameObject attackPointObject = FindShotPointInChildren(Shotpoints, "RollAttack");
        shotpoint = attackPointObject.transform;
        GameObject container = Instantiate(attackContainer, shotpoint.position, transform.rotation, RangedAttackFXLayer.transform);
        Instantiate(projectile1, container.transform.position, transform.rotation, container.transform);
        Instantiate(projectile1, container.transform.position,
            Quaternion.Euler(transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y,
            transform.rotation.eulerAngles.z + 2.5f), container.transform);
        Instantiate(projectile1, container.transform.position,
            Quaternion.Euler(transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y,
            transform.rotation.eulerAngles.z - 2.5f), container.transform);



    }


    private void StandardAttackCombo1()
    {
        GameObject attackPointObject = FindShotPointInChildren(Shotpoints, "StandardAttack");
        shotpoint = attackPointObject.transform;
        GameObject container = Instantiate(attackContainer, shotpoint.position, transform.rotation, RangedAttackFXLayer.transform);

        Instantiate(projectile2, container.transform.position, transform.rotation, container.transform);


    }


    private void DashAttack()
    {

        GameObject attackPointObject = FindShotPointInChildren(Shotpoints, "DashAttack");
        shotpoint = attackPointObject.transform;
        GameObject container = Instantiate(attackContainer, shotpoint.position, transform.rotation, RangedAttackFXLayer.transform);
        Instantiate(projectile3, container.transform.position, transform.rotation, container.transform);
        Instantiate(projectile3, container.transform.position,
            Quaternion.Euler(transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y,
            transform.rotation.eulerAngles.z + 6.6f), container.transform);
        Instantiate(projectile3, container.transform.position,
            Quaternion.Euler(transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y,
            transform.rotation.eulerAngles.z + 13.3f), container.transform);
        Instantiate(projectile3, container.transform.position,
            Quaternion.Euler(transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y,
            transform.rotation.eulerAngles.z + 20f), container.transform);


    }





    private GameObject FindShotPointInChildren(GameObject _parent, string childName)
    {
        for (int i = 0; i < _parent.transform.childCount; i++)
        {
            if (_parent.transform.GetChild(i).name == childName)
            {
                return _parent.transform.GetChild(i).gameObject;
            }
        }
        return null;
    }


}
