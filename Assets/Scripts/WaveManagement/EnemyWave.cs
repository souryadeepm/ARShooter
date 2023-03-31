using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class EnemyWave :ScriptableObject
{
    public string waveName;
    public List<GameObject> enemies = new List<GameObject>();
    public float enemySpawnTime;
    
}
