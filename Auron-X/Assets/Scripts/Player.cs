using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Playerdata data;
    Animator _anim;
    float direcao;
    GameObject _controler, _area;
    public Slider _barraDeVida, _barraDeXp;
    public Text _txtNivel;

    void Start()
    {
        data = _controler.GetComponent<Playerdata>();
        data._vida = data._maxVida;
        data._poder = data._maxpoder;
        _anim = GetComponent<Animator>();
        _area = GameObject.Find("Area");
    }

    void Update()
    {
        setarValores();
        if (data._xp >= data._maxXp)
        {
            proximoNivel();
        }
        movimentação(data._velocidadeDeMovimento);
    }

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
