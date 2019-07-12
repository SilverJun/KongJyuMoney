using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator _anim;
    float _speed = 5.0f;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.SetBool("IsMoving", true);
    }

    void FixedUpdate()
    {
        _anim.SetFloat("PosX", Input.GetAxis("Horizontal"));
        _anim.SetFloat("PosY", Input.GetAxis("Vertical"));

        float translation = Input.GetAxis("Vertical") * _speed;
        float straffe = Input.GetAxis("Horizontal") * _speed;
        translation *= Time.fixedDeltaTime;
        straffe *= Time.fixedDeltaTime;
        
        transform.Translate(straffe, 0, translation);
    }
}
