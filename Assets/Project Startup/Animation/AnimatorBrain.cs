using System;
using UnityEngine;

public class AnimatorBrain : MonoBehaviour
{
    [field: SerializeField]
    private readonly static int[] animations =
    {
        /*Animator.StringToHash("Idle")*/
        // Add Extra Animations To This List
        // ANY ANIMATIONS IN THE GAME
        // Order Of This Determines The Order Of The Enum List
    };
    private Animator animator;
    private E_Animations[] currentAnimation;
    private bool[] layerLocked;
    private Action<int> DefaultAnimation;


    // Call Method In Start To Create A New Animator Brain
    protected void InitializeAnimatorBrain(int _layers, E_Animations _startingAnimation, Animator _animator, Action<int> DefaultAnimation)
    {
        layerLocked = new bool[_layers];
        currentAnimation = new E_Animations[_layers];
        this.animator = _animator;
        this.DefaultAnimation = DefaultAnimation;

        for (int i = 0; i < _layers; i++)
        {
            layerLocked[i] = false;
            currentAnimation[i] = _startingAnimation;
        }
    }

    public E_Animations GetCurrentAnimation(int _layer) { return currentAnimation[_layer]; }

    public void SetLocked(bool _lockLayer, int _layer) { layerLocked[_layer] = _lockLayer; }

    public void Play(E_Animations _animation, int _layer, bool _lockLayer, bool _bypassLock, float _crossFade = .2f)
    {
        if (_animation == E_Animations.None) { DefaultAnimation(_layer); return; }
        if (layerLocked[_layer] && !_bypassLock) { return; }

        SetLocked(_lockLayer, _layer);
        if (currentAnimation[_layer] == _animation) { return; }

        currentAnimation[_layer] = _animation;
        animator.CrossFade(animations[(int)currentAnimation[_layer]], _crossFade, _layer);
    }
}
public enum E_Animations
{
    None
    // Add Extra Enums That Correspond With The Animator Hash Set Order
}