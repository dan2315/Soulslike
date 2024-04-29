using System;
using UnityEngine;

class AnimatorEvents : MonoBehaviour
{
    public Action OnAnimationCompleted;
    public void AnimationCompleted()
    {
        OnAnimationCompleted?.Invoke();
    }
}