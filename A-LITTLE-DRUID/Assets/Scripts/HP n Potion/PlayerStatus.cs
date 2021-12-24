using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    [System.Serializable]
    public struct PStatus
    {
        public GameObject player;
        public float distance;
        public float dmgToPlayer;
        public int dmgToEnemy;
        public float dmgFromMiddleboss;
        public float dmgFromLastboss;
        public float playerMaxHp;
        public float playerCurrentHp;

        public void playerObjSet()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        public void AttackDistanceSet()
        {
            distance = 0.5f;
        }

        public void PlayerDmgToEnemySet()
        {
            dmgToEnemy = 3;
        }

        public void PlayerStatusSet()
        {
            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 2:
                    dmgToPlayer = 0.1f;
                    dmgFromMiddleboss = 0.5f;
                    dmgFromLastboss = 6;
                    playerMaxHp = 400f;
                    break;
                case 3:
                    dmgToPlayer = 0.1f;
                    dmgFromMiddleboss = 0.5f;
                    dmgFromLastboss = 6;
                    playerMaxHp = 400f;
                    break;
                case 4:
                    dmgToPlayer = 0.3f;
                    dmgFromMiddleboss = 0.5f;
                    dmgFromLastboss = 7;
                    playerMaxHp = 500f;
                    break;
                case 5:
                    dmgToPlayer = 0.6f;
                    dmgFromMiddleboss = 0.7f;
                    dmgFromLastboss = 8;
                    playerMaxHp = 600f;
                    break;
                case 6:
                    dmgToPlayer = 1.0f;
                    dmgFromMiddleboss = 1.1f;
                    dmgFromLastboss = 10;
                    playerMaxHp = 700f;
                    break;
                default:
                    break;
            }
            playerCurrentHp = playerMaxHp;
        }
    }

    public PStatus pStatus = new PStatus();
    [SerializeField]
    Slider pHPSlider;

    private void Awake()
    {
        pHPSlider = GameObject.Find("hpSlider").GetComponent<Slider>();
        pStatus.playerObjSet();
        pStatus.AttackDistanceSet();
        pStatus.PlayerDmgToEnemySet();
        pStatus.PlayerStatusSet();
    }

    private void Start()
    {
        pHPSlider.maxValue = pStatus.playerMaxHp;
        pHPSlider.value = pStatus.playerMaxHp;
    }

    private void Update()
    {
        pHPSlider.value = pStatus.playerCurrentHp;
    }
}
