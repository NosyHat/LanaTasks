using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inteligencia : MonoBehaviour
{
    [Header("Atuadores")]
    [SerializeField] private AtuadorPerseguir chase;
    [SerializeField] private AtuadorPatrulha patrolling;
    [SerializeField] private AtuadorPatrulha.patrolType patrolType;

    [Header("Sensores")]
    [SerializeField] private SensorVisao vision;

    [Header("Configuração")]
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject me;
    public bool statueMode;

    Animator anim;
    [SerializeField] private float speed;
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        if (vision.ver(target))
        {
            if (me.name == "Doutora")
            {
                anim.SetBool("canWalk", true);
                chase.perseguir(speed, target);
                speed = 4f;
            }
            else
            {
                anim.SetBool("canWalk", true);
                chase.perseguir(speed, target);
                speed = 2f;
            }
        }
        else
        {
            anim.SetBool("canWalk", true);
            patrolling.patrol(speed, patrolType);
            speed = 1f;
        }
    }
}
