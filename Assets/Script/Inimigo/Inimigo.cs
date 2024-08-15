using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public List<Transform> pontos;
    int nextId = 0;
    int idChangeValue = 0;
    public float velocidade;
    public Animator inimigoAnim;
    Rigidbody2D rbInimigo;

    void Awake()
    {
        inimigoAnim = GetComponent<Animator>();
        rbInimigo = GetComponent<Rigidbody2D>();

    }

    void Reset()
    {
        Correr();
    }


    void Correr()
    {
        GetComponent<CapsuleCollider2D>().isTrigger = false;
        GameObject root = new GameObject(name + "_Filho");
        root.transform.position = transform.position;
        transform.SetParent(root.transform);
        
        GameObject pontos = new GameObject(name + "ponto");
        pontos.transform.SetParent(root.transform);
        pontos.transform.position = root.transform.position;

        GameObject p1 = new GameObject("Point1");
        GameObject p2 = new GameObject("Point2");

        p1.transform.SetParent(pontos.transform);
        p2.transform.SetParent(pontos.transform);

        p1.transform.position = root.transform.position;
        p2.transform.position = root.transform.position;

        this.pontos = new List<Transform>();
        this.pontos.Add(p1.transform);
        this.pontos.Add(p2.transform);
    }


    void FixedUpdate()
    {
        MoverParaOProximoPonto();


    }


    void MoverParaOProximoPonto()
    {
        Transform pontoChegada = this.pontos[nextId];

        if (pontoChegada.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        transform.position = Vector3.MoveTowards(transform.position, pontoChegada.position, velocidade);

        if (Vector2.Distance(transform.position, pontoChegada.position) < 1f)
        {
            // A distancia < 1
            // B

            if(nextId == pontos.Count - 1)
            {
                idChangeValue -= 1;

            }

            if (nextId == 0)
            {
                idChangeValue = 1;

            }

            nextId += idChangeValue;


        }

        // 
    }

    public void Morte()
    {
        inimigoAnim.SetTrigger("Morte");
        
    }
    public void DestroyInimigo()
    {
        Destroy(this.gameObject);
    }
}
