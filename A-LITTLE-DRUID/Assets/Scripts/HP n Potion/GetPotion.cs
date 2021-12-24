using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPotion : MonoBehaviour
{
    public GameObject player;
    public GameObject[] potions;
    public float interval;//포션과 떨어진 거리
    potionName potionName;
    potionCollection potionCollection;
    PotionEfficacy potionEfficacy;
    public Vector3 potionOutPos;
    AudioEffect audioEffect;

    bool potion_Down;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        potions = GameObject.FindGameObjectsWithTag("Potion");
        potionCollection = GameObject.Find("PotionStuff").GetComponent<potionCollection>();
        potionEfficacy = FindObjectOfType<PotionEfficacy>();
        audioEffect = FindObjectOfType<AudioEffect>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) || potion_Down)
        {
            var pl = player.transform.position;//지역변수 pl : player's location

            for (int i = 0; i < potions.Length; i++)
            {
                var po = potions[i].transform.position;

                if ((po.x < pl.x + interval) && (po.x > pl.x - interval) && (po.y < pl.y + interval) && (po.y > pl.y - interval))
                {
                    potionName = GameObject.Find(potions[i].name).GetComponent<potionName>();
                    potions[i].transform.position = potionOutPos;
                    Debug.Log("포션을 획득했습니다!");
                    Debug.Log(potionName.myName);
                    var potionEffectCategory = potionCollection.potionList[potionName.myName].Item1;
                    var potionEffect = potionCollection.potionList[potionName.myName].Item2;
                    switch (potionEffectCategory) {
                        case "health":
                            potionEfficacy.HealthImmediately(potionEffect);
                            break;
                        case "specialHealth":
                            potionEfficacy.SpecialHealth(potionEffect);
                            break;
                        case "healthWithTime":
                            StartCoroutine(potionEfficacy.HealthWithTime(potionEffect));
                            break;
                        case "speed":
                            potionEfficacy.YourSpeedFeelLike(potionEffect);
                            break;
                    }
                    if (potionName.myName != "blackFullCharge")
                    {
                        audioEffect.GetItemSoundPlay();
                    }
                    else
                    {
                        audioEffect.GetSuperItemSound();
                    }
                }
            }
        }

        InitMobileVar();
    }

    public void ButtonDown(string type)
    {
        switch (type)
        {
            case "P":
                potion_Down = true;
                break;

        }
    }

    private void InitMobileVar()
    {
        potion_Down = false;
    }
}
