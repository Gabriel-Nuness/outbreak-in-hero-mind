using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Player / hud")]
    Playerdata data;
    GameObject _controler, _area;
    public Slider _barraDeVida, _barraDeXp;
    public Text _txtNivel;

    [Header("Movimentação")]
    float direcao;
    Animator _anim;
    GameObject _pe;
    bool podePular = true;

    void Start()
    {
        _controler = GameObject.Find("Controler");
        data = _controler.GetComponent<Playerdata>();
        data._vida = data._maxVida;
        data._poder = data._maxpoder;
        _anim = GetComponent<Animator>();
        _area = GameObject.Find("Area");
        _pe = GameObject.Find("Pe");
    }

    void Update()
    {
        movimentação(data._velocidadeDeMovimento);
        condicaoDePulo();
        setarValores();
        if (data._xp >= data._maxXp)
        {
            proximoNivel();
        }
    }

    RaycastHit2D ray(Vector2 direcao, float distancia, GameObject origem)
    {
        RaycastHit2D rayCast = Physics2D.Raycast(origem.transform.position, direcao, distancia);
        return rayCast;
    }

    //Movimentação
    void movimentação(float velodidade)
    {
        direcao = Input.GetAxis("Horizontal");
        if (direcao != 0)
            _area.transform.localPosition = direcao > 0 ? new Vector3(0, 0, 0) : new Vector3(-1, 0, 0);
        transform.Translate(direcao * velodidade * Time.deltaTime, 0, 0);
        _anim.SetBool("Andando", Input.GetButton("Horizontal"));
        Flip(GetComponent<SpriteRenderer>());
    }

    private void Flip(SpriteRenderer _playerSpriteRender)
    {
        if (_playerSpriteRender.flipX == true && direcao > 0 || _playerSpriteRender.flipX == false && direcao < 0)
        {
            _playerSpriteRender.flipX = !_playerSpriteRender.flipX;
        }
    }

    //pulo
    public void condicaoDePulo()
    {
        RaycastHit2D rayCast = ray(-Vector2.up, 0.15f, _pe);
        Debug.DrawLine(transform.position, -Vector2.up, Color.green);
        if (rayCast)
            if (rayCast.collider.tag == "chao")
            {
                podePular = true;
                Pular(Input.GetKeyDown(KeyCode.W), GetComponent<Rigidbody2D>());
            }
            else
                podePular = false;

    }

    public void Pular(bool apertou, Rigidbody2D rb2D)
    {
        if (apertou && podePular)
        {
            _anim.SetBool("Caiu", false);
            _anim.SetBool("Pulando", true);
            _anim.SetBool("Andando", false);
            rb2D.AddForce(Vector2.up * 350);
        }
    }

    IEnumerator tempoDeQueda()
    {
        yield return new WaitForSeconds(0.15f);
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        camera.GetComponent<Animator>().SetBool("Balancar", false);
        _anim.SetBool("Caiu", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "chao")
        {
            _anim.SetBool("Pulando", false);
            _anim.SetBool("Caiu", true);
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            camera.GetComponent<Animator>().SetBool("Balancar", true);
            StartCoroutine(tempoDeQueda());
        }
    }

    //////////

    private void proximoNivel()
    {
        data._xp = 0;
        data.nivel++;
        data._maxXp += data._vidaPorNivel;
        data._dano += data._danoPorNivel;
        data._maxXp = data._vidaPorNivel;
        data._maxpoder += data._poderPorNivel;
    }

    private void setarValores()
    {
        _barraDeVida.maxValue = data._maxVida;
        _barraDeVida.value = data._vida;
        _barraDeXp.maxValue = data._maxXp;
        _barraDeXp.value = data._xp;
        _txtNivel.text = "lvl: " + data.nivel;
    }
}
