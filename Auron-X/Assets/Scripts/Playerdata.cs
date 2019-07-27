using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerdata : MonoBehaviour
{
    public float _vida, _maxVida = 32, _velocidadeDeMovimento = 8, _dano = 3, _poder = 50;
    [HideInInspector]
    public float _vidaPorNivel = 5, _danoPorNivel = 2, _poderPorNivel = 10, _maxXp = 100, _maxpoder = 50;
    public int _xp = 0, nivel = 1;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
