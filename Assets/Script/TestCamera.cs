using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{
    Vector2 _mouseLook;
    Vector2 _smooth;
    public float _speed = 10.0f;
    public float _sensitivity = 5.0f;
    public float _smoothing = 2.0f;
    GameObject _kongjyu;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _kongjyu = Resources.Load<GameObject>("Prefab/Kongjyumoney");
    }

    void RotateCam()
    {
        // rotation
        var mouseDir = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseDir = Vector2.Scale(mouseDir, new Vector2(_sensitivity * _smoothing, _sensitivity * _smoothing));
        _smooth.x = Mathf.Lerp(_smooth.x, mouseDir.x, 1.0f / _smoothing);
        _smooth.y = Mathf.Lerp(_smooth.y, mouseDir.y, 1.0f / _smoothing);
        _mouseLook += _smooth;

        var quat = Quaternion.AngleAxis(-_mouseLook.y, Vector3.right);
        transform.rotation = Quaternion.AngleAxis(_mouseLook.x, Vector3.up) * quat;
    }

    void MoveCam()
    {
        // translation
        float translation = Input.GetAxis("Vertical") * _speed;
        float straffe = Input.GetAxis("Horizontal") * _speed;
        translation *= Time.fixedDeltaTime;
        straffe *= Time.fixedDeltaTime;

        var forward = new Vector3(straffe, 0, translation);

        Vector3.RotateTowards(forward, transform.forward, 0, 0);
        transform.Translate(forward);
    }

    void FireKongjyu()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var throwKongjyu = Instantiate(_kongjyu, transform.position, transform.rotation);
            var force = transform.forward * 500.0f;
            var kongjyuRigid = throwKongjyu.GetComponent<Rigidbody>();
            kongjyuRigid.AddForce(force);       /// add throw force. It must be changed.
            kongjyuRigid.AddTorque(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f));        /// random torque
        }
    }

    void FixedUpdate()
    {
        RotateCam();
        MoveCam();

        FireKongjyu();
    }
}
