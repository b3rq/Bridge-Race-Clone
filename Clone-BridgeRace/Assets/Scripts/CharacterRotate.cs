using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CharacterRotate : MonoBehaviour
{
    private float _fpsf;
    [SerializeField] private TextMeshProUGUI _fpsCounter;

    [SerializeField] public Animator _animator;

    [SerializeField] public GameObject DoorTriggerCheck;
    public bool KeepGoing = false;

    #region Joystick
    [SerializeField] private float _speed;
    [SerializeField] private float _turnSpeed;
    [HideInInspector] public bool IsCanMoveZ = true;
    public FloatingJoystick floatingJoystick;
    [SerializeField] private CharacterController cc;
    public static CharacterRotate Instance { get; private set; }
    #endregion

    #region Gravity
    private float _gravityPower = -9.81f;
    private Vector3 _velocityY;
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {

        _fpsf = (int)(1f / Time.unscaledDeltaTime);
        _fpsCounter.SetText($"Fps : {_fpsf}");

        if (UI.Instance.IsStart)
        {
            if (FinishSystem.Instance.IsFinish) return;
            if (Input.GetButton("Fire1"))
            {
                _animator.SetBool("isRun", true);
                JoystickMovement();
            }
            else
            {
                _animator.SetBool("isRun", false);
            }
            _velocityY.y += _gravityPower * Time.deltaTime;
            cc.Move(_velocityY * Time.deltaTime);
        }
    }

    private void JoystickMovement()
    {
        float horizontal = floatingJoystick.Horizontal;
        float vertical = floatingJoystick.Vertical;

        Vector3 addedPos = new(horizontal * _speed * Time.deltaTime, 0, vertical * _speed * Time.deltaTime);
        if (!IsCanMoveZ) addedPos.z = 0;

        cc.Move(addedPos);

        //transform.position += addedPos;

        Vector3 direction = Vector3.forward * vertical + Vector3.right * horizontal;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), _turnSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == DoorTriggerCheck)
        {
            TopLayerOn();
        }
    }

    public void TopLayerOn()
    {
        KeepGoing = true;

        foreach (var block in BlockData.Instance.AllBlocks)
        {
            if (block.GetComponent<MeshRenderer>().material.color == GetComponent<MeshRenderer>().material.color)
            {
                if (block.transform.position.y < 1)
                {
                    block.SetActive(false);
                }
            }
        }
    }
}
