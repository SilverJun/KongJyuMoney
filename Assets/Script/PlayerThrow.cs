using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    Animator _anim;

    [SerializeField]
    [Range(100.0f, 750.0f)]
    float _throwSpeed = 500.0f;     // maybe Range(100, 750);

    void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.SetBool("IsMoving", true);
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            StartCoroutine(ThrowKongjyu());
        }
    }

    IEnumerator ThrowKongjyu()
    {
        _anim.SetLayerWeight(1, 1.0f);
        yield return new WaitUntil(()=>Input.GetMouseButtonUp(0));

        // throw

        // restore animation layer
        _anim.SetLayerWeight(1, 0.0f);
    }
}
