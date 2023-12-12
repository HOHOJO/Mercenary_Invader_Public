using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimTime : MonoBehaviour
{
    private float curAnimationTime; // 애니메이션 시간
    public Monster monster;
    public RuntimeAnimatorController controller;
    // Start is called before the first frame update
    void Start()
    {
        curAnimationTime = 0f;
        controller = monster.Animator.runtimeAnimatorController;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float AnimationTime(string name)
    {
        for (int i = 0; i < controller.animationClips.Length; i++)
        {
            if (controller.animationClips[i].name == name)
            {
                curAnimationTime = controller.animationClips[i].length;
                break;
            }
        }
        return curAnimationTime;
    }
}
