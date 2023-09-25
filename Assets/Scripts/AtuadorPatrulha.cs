using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtuadorPatrulha : MonoBehaviour
{
    public enum patrolType { square, circle, linear };

    [Header("Points")]
    [SerializeField] private Vector3 origin;
    [SerializeField] private float distance;

    private Vector3 infLimit;
    private Vector3 supLimit;
    public float circleLimit;

    private float time = 0;
    private float timeLimit;

    [Header("Configuration")]
    [SerializeField] private Transform me;

    public float patrolNumber;
    [SerializeField] float maxTime;
    [SerializeField] private float angleLimit;

    void Start()
    {
        patrolNumber = Random.Range(0, 3);

        origin = transform.position;
        supLimit = new Vector3(transform.position.x - distance, 0, transform.position.z + distance);
        infLimit = new Vector3(transform.position.x + distance, 0, transform.position.z - distance);

        timeLimit = Random.Range(1, maxTime);
    }

    void Update()
    {
        circleLimit = Vector3.Distance(origin, me.position);

        // Definir em modo projeto a área de patrulha quadrada

        if (patrolNumber == 1)
        {
            Debug.DrawLine(supLimit, new Vector3(supLimit.x, 0, infLimit.z), Color.red);
            Debug.DrawLine(supLimit, new Vector3(infLimit.x, 0, supLimit.z), Color.red);
            Debug.DrawLine(infLimit, new Vector3(supLimit.x, 0, infLimit.z), Color.red);
            Debug.DrawLine(infLimit, new Vector3(infLimit.x, 0, supLimit.z), Color.red);
        }

        if (patrolNumber == 2)
        {
            Debug.DrawLine(origin, me.position, Color.blue);
        }

    }
    public void patrol(float speed, patrolType type)
    {
        time += Time.deltaTime;

        if (patrolNumber == 1)
        {
            type = patrolType.square;
        }

        if (patrolNumber == 2)
        {
            type = patrolType.circle;
        }

        if (time < maxTime)
        {
            //Continuar sua rota definida
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        else
        {
            //Escolher nova rota
            if (type == patrolType.square)
            {
                squareArea();
            }

            else if (type == patrolType.circle)
            {
                circleArea();
            }

            else
            {
                //linearArea();
            }

            time = 0;
            timeLimit = Random.Range(1, maxTime);

        }
    }

    public void squareArea()
    {
        //Verificar se estou dentro da área de patrulha
        //Definir a minha rota

        if (transform.position.x < supLimit.x ||
            transform.position.z > supLimit.z ||
            transform.position.z < infLimit.z ||
            transform.position.x > infLimit.x)
        {
            //Estou fora dos limites da patrulha (quaternion)
            Vector3 direction = (origin - transform.position).normalized;
            Quaternion giro = Quaternion.LookRotation(direction);
            giro.x = 0;
            giro.z = 0;
            transform.rotation = giro;
        }

        else
        {
            //Estou dentro dos limites da patrulha (graus)
            float angle = Random.value * angleLimit;
            transform.Rotate(new Vector3(0, angle, 0));
        }
    }

    public void circleArea()
    {
        if (circleLimit > distance)
        {
            //Estou fora dos limites da patrulha (quaternion)
            Vector3 direction = (origin - transform.position).normalized;
            Quaternion giro = Quaternion.LookRotation(direction);
            giro.x = 0;
            giro.z = 0;
            transform.rotation = giro;
        }
        else
        {
            //Estou dentro dos limites da patrulha (graus)
            float angle = Random.value * angleLimit;
            transform.Rotate(new Vector3(0, angle, 0));

        }
    }
}