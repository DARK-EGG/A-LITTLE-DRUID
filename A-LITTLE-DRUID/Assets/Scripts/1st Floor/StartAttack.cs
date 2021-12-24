using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAttack : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      if(BossTalk.canTalk==false && BossTalk.canBoss == true)
        {
            animator.SetTrigger("StartAttack");
        }
    }
}
