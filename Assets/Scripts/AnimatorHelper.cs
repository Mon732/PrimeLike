using UnityEngine;
using System.Collections;

public class AnimatorHelper : MonoBehaviour
{
    Animator animator;

	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
	}

    public void SetFloat(string name)
    {
        animator.SetFloat(name, 1);
    }

    public void ResetFloat(string name)
    {
        animator.SetFloat(name, 0);
    }

    public void SetInt(string name)
    {
        animator.SetInteger(name, 1);
    }

    public void ResetInt(string name)
    {
        animator.SetInteger(name, 0);
    }

    public void SetBool(string name)
    {
        animator.SetBool(name, true);
    }

    public void ResetBool(string name)
    {
        animator.SetBool(name, false);
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
