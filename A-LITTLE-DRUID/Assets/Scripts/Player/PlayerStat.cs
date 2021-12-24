using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    public static PlayerStat instance;

    public int hp;
    public int currentHp;
    public int enemyCount = 0;

    public Text countText;

    public float time;
    private float current_time;

    public Slider hpSlider;
    public Slider mpSlider;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        currentHp = hp;
        current_time = time;

        countText.text = "EnemyCount " + enemyCount; // 처치한 적 count
    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.maxValue = hp; // Slider를 Player의 Max체력으로

        hpSlider.value = currentHp;


    }

    // 적 죽일때마다 count하는 함수 써야함.
}
