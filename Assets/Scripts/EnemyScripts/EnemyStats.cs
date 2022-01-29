using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public string enemyName;

    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _attackSpeed = 1f;
    [SerializeField] private float _attackPower = 20f;
    [SerializeField] private float _stoppingDistance = 2f;
    [SerializeField] private float _range = 50f;

     




    public float Health { get { return _maxHealth; } set { _maxHealth = value; } }
    public float AttackSpeed { get { return _attackSpeed; } }
    public float AttackPower { get { return _attackPower; } set { _attackPower = value; } }
    public float StoppingDistance { get { return _stoppingDistance; }  }
    public float Range { get { return _range; }  }


}
