using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadosVisao
{
    public bool encontrou { get; set; }
    public Vector3 posicao { get; set; }
}

public class SensorVisao : MonoBehaviour
{
    [SerializeField] private float campoVisao;
    [SerializeField] private float limiteDeVisao;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool ver(GameObject alvo)
    {
        bool encontrou = false;
        Vector3 direcao =
            (alvo.transform.position - transform.position).normalized;
        float angulo = Vector3.Angle(transform.forward, direcao);
        if(angulo < campoVisao)
        {
            //dentro do campo de visão
            RaycastHit coisa;
            Physics.Raycast(transform.position, direcao, 
                out coisa, limiteDeVisao);

            if(coisa.collider.name == alvo.name)
            {
                encontrou = true;
            }
        }
        



        return encontrou;
    }

    public bool ver(GameObject alvo, GameObject objTransparente)
    {
        bool encontrou = false;
        Vector3 direcao =
            (alvo.transform.position - transform.position).normalized;
        float angulo = Vector3.Angle(transform.forward, direcao);
        if (angulo < campoVisao)
        {
            //dentro do campo de visão
            var lista = Physics.RaycastAll(transform.position,
                direcao, limiteDeVisao);
            if (lista[0].collider.name == alvo.name)
            {
                encontrou = true;
            }
            else
            {
                if (lista[0].collider.name == objTransparente.name)
                {
                    if (lista[1].collider.name == alvo.name)
                    {
                        encontrou = true;
                    }
                }
            }
        }




        return encontrou;
    }

    public DadosVisao verPosicao(GameObject alvo)
    {
        DadosVisao dados = new DadosVisao();
        dados.encontrou = false;
        Vector3 direcao =
            (alvo.transform.position - transform.position).normalized;
        float angulo = Vector3.Angle(transform.forward, direcao);
        if (angulo < campoVisao)
        {
            //dentro do campo de visão
            RaycastHit coisa;
            Physics.Raycast(transform.position, direcao,
                out coisa, limiteDeVisao);

            if (coisa.collider.name == alvo.name)
            {
                dados.encontrou = true;
                dados.posicao = coisa.collider.transform.position;
            }
        }




        return dados;
    }
}
