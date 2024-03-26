using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateChanger : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string currentState = "Idle";

    void StopGame() {
        Time.timeScale = 0;
    }
    public void ChangeAnimationState(string newState, float speed = 1, System.Action callback = null, float extraDelayTime = 0)
    {
        animator.speed = speed;
        if(currentState == newState){return;}
        currentState = newState;
        animator.Play(currentState); 
        StartCoroutine(WaitForAnimation(callback, extraDelayTime));     
    }

    private IEnumerator WaitForAnimation(System.Action callback, float extraDelayTime)
    {
        yield return new WaitForSeconds((animator.GetCurrentAnimatorStateInfo(0).length / animator.speed) + extraDelayTime);
        callback?.Invoke();
    }
}
