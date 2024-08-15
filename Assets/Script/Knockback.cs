using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public Jogador jogador;
    public float knockBackForca;
    public float knockTempo;

    void Awake()
    {
        jogador = GetComponent<Jogador>(); 
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Inimigo"))
        {
            var inimigo = other.GetComponent<Inimigo>();

            if(jogador.rbJogador.velocity.y < -1)
            {
                jogador.rbJogador.AddForce(Vector2.up * knockBackForca, ForceMode2D.Impulse);
                inimigo.Morte();
            }
        }
    }
}
