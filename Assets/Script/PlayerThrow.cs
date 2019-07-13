using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    Animator _anim;

    [SerializeField]
    [Range(100.0f, 750.0f)]
    float _throwSpeed = 500.0f;     // maybe Range(100, 750);
    bool _isThrowing = false;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.SetBool("IsMoving", true);
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && !_isThrowing)
        {
            StartCoroutine(ThrowKongjyu());
        }
    }

    IEnumerator ThrowKongjyu()
    {
        _isThrowing = true;
        _anim.SetBool("Standby", true);
        yield return new WaitUntil(()=>Input.GetMouseButtonUp(0));      // Wait for mouse up.

        // throw
        _anim.SetTrigger("ThrowNow");
        _anim.SetBool("Standby", false);

        yield return new WaitUntil(()=>_anim.GetCurrentAnimatorStateInfo(1).IsName("Idle"));        // Wait for transition to idle
        _isThrowing = false;
    }
}
