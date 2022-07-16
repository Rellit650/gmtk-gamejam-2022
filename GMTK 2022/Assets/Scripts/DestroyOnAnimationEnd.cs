using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAnimationEnd : MonoBehaviour
{

    private void Start()
    {
        /*
        AnimationClip clip;
        Animator anim = GetComponent<Animator>();

        clip = anim.runtimeAnimatorController.animationClips[0];

        Keyframe[] keys = new Keyframe[1];

        keys[0] = new Keyframe(1.1f, Random.Range(-2f, 2f));

        AnimationCurve curve = new AnimationCurve(keys);

        clip.SetCurve("", typeof(RectTransform), "anchoredPosition.x", curve);
        */
    }

    public void DestroyParentObject() 
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
