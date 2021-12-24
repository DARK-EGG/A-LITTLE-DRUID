using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemiesHP : MonoBehaviour
{
    public int EnemyCategoryNum;

    public GameObject floorLastBoss;
    public float floorLastBossHp;

    public float[] EnemyHp;
    public string[] EnemyName;

    public float[] middleBossHp;
    public string[] middleBossName;

    public GameObject[] Enemies;
    public GameObject[] MiddleBoss;

    public int[] EnemyHpInt;
    public GameObject[] monsters;

    public int[] middleBossHpInt;
    public GameObject[] middleBosses;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 6)
            floorLastBoss = GameObject.Find("Boss");
        else
        {
            floorLastBoss = GameObject.FindGameObjectWithTag("Boss");
        }
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        MiddleBoss = GameObject.FindGameObjectsWithTag("MiddleBoss");
        int sizeEnemy = Enemies.Length;
        EnemyHp = new float[sizeEnemy];
        EnemyName = new string[sizeEnemy];
        int sizeMB = MiddleBoss.Length;
        middleBossHp = new float[sizeMB];
        middleBossName = new string[sizeMB];

        for (int i = 0; i < Enemies.Length; i++)
        {
            EnemyCategory(i);
        }
        for(int l = 0; l < MiddleBoss.Length; l++)
        {
            MiddleBossCategory(l);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void EnemyCategory(int i)
    {
        for (int t = 0; t < monsters.Length; t++)
        {
            if (Enemies[i].name == monsters[t].name)
            {
                EnemyName[i] = monsters[t].name;
                EnemyHp[i] = EnemyHpInt[t];
            }
        }
    }
    public void MiddleBossCategory(int i)
    {
        for (int t = 0; t < middleBosses.Length; t++)
        {
            if (MiddleBoss[i].name == middleBosses[t].name)
            {
                middleBossName[i] = middleBosses[t].name;
                middleBossHp[i] = middleBossHpInt[t];
            }
        }
    }
}
