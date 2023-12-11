using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAnimHall : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    	animator.SetInteger( "walkCount", animator.GetInteger("walkCount") - 1 ) ;
    }
}
