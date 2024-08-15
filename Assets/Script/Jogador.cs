using UnityEngine;



public enum JogadorEstado
{
    parado = 0,
    andando = 1,
    pulando = 2,
    caindo = 3
}


public class Jogador : MonoBehaviour
{
    public JogadorEstado jogadorEstado;
    public Rigidbody2D rbJogador;
    public Animator jogadorAnimacao;
    public float velocidade;
    public float direcao;

    public SpriteRenderer jogadorSprite;


    public float forcaPulo;
    public bool estaChao;
    public Transform posicaoPe;
    public LayerMask chao;
    public float raio;


    void Awake()
    {
        rbJogador = GetComponent<Rigidbody2D>();
        jogadorSprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        direcao = Input.GetAxisRaw("Horizontal");
        //A = -1, D = 1, 0
        estaChao = Physics2D.OverlapCircle(posicaoPe.position, raio, chao);
        AtualizaAnimacao();
        Flip();
        AtualizaEstado();
        if (Input.GetKeyDown(KeyCode.Space) && estaChao)
            Pulo();

    }

    private void FixedUpdate()
    {
        MovePlayer();
        

    }

    void MovePlayer()
    {
        rbJogador.velocity = new Vector2(velocidade * direcao, rbJogador.velocity.y);
        // (x, y)
        // (1, 0)
    }

    void Pulo()
    {
        rbJogador.velocity = Vector2.up * forcaPulo;
        jogadorAnimacao.SetTrigger("Pulo");
        // (0, 1) * forcaPulo
        
    }

    void AtualizaEstado()
    {
        jogadorAnimacao.SetInteger("State", (int) jogadorEstado);

    }

    void AtualizaAnimacao()
    {
        if (direcao != 0)
        {
            jogadorEstado = JogadorEstado.andando;
        }

        if (rbJogador.velocity.magnitude < 0.2f)
        {
            jogadorEstado = JogadorEstado.parado;
        }

        if (rbJogador.velocity.y < -1)
        {
            jogadorEstado = JogadorEstado.caindo;
        }
    }

    void Flip()
    {
        if(direcao != 0)
        {
            jogadorSprite.flipX = direcao < 0;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(posicaoPe.position, raio);
    }
}
