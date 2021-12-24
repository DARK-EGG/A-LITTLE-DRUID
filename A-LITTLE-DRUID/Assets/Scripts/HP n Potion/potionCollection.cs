using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class potionCollection : MonoBehaviour
{
    public Dictionary<string, (string, float)> potionList = new Dictionary<string, (string, float)>();

    potionName potionName;


    void Awake()
    {
        potionList.Add("redIsGoodOne", ("health", +100));//HP 회복 30
        potionList.Add("brownIsNotGoodOne", ("health", -30));//HP -30
        potionList.Add("blackFullCharge", ("specialHealth", +1000));
        potionList.Add("greenIsLoseHealth", ("healthWithTime", -5f));//몇 초에 걸쳐서 플레이어의 HP -//210303기준으로 작동 안함...
        potionList.Add("fasterThanBefore", ("speed", +0.3f));//플레이어의 스피드 증가 0.3f만큼(층을 이동할 때 돌아옴)
        potionList.Add("slowerThanBefore", ("speed", -0.1f));//플레이어의 스피드 감소 0.1f만큼(층을 이동할 때 돌아옴)
    }

}