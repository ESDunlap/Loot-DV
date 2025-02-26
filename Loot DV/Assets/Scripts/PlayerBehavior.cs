using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;
    public float jumpVelocity = 5f;
    public float distanceToGround = 0.1f;
    public LayerMask groundLayer;
    public GameObject bullet;
    public GameObject secretCrash;
    public float bulletSpeed = 100f;
    public float rockSpeed = 10f;
    private float vInput;
    private float hInput;
    private Rigidbody _rb;
    private bool jumpInput = false;
    private bool fireInput = false;
    private bool secretInput = false;
    private bool rockInput = false;
    private bool hasRock = false;
    private CapsuleCollider _col;
    private GameBehavior _gameManager;
    public delegate void JumpingEvent();
    public event JumpingEvent playerJump;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col= GetComponent<CapsuleCollider>();
        _gameManager= GameObject.Find("Game Manager").GetComponent<GameBehavior>();
    }

    void Update()
    {
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            jumpInput = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            fireInput = true;
        }

        if (Input.GetMouseButtonDown(1) && hasRock)
        {
            rockInput = true;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            secretInput = true;
        }
        /*
        this.transform.Translate(Vector3.forward * vInput *
        Time.deltaTime);
        this.transform.Rotate(Vector3.up * hInput * Time.deltaTime);
        */
    }
    void FixedUpdate()
    {
        if(jumpInput)
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            jumpInput = false;
            playerJump();
        }
        if (fireInput)
        {
            GameObject newBullet = Instantiate(bullet, this.transform.position + this.transform.right, this.transform.rotation) as GameObject;

            Rigidbody bulletRB= newBullet.GetComponent<Rigidbody>();

            bulletRB.velocity = this.transform.forward * bulletSpeed;
            fireInput = false;
        }
        if (rockInput)
        {
            GameObject newBullet = Instantiate(bullet, this.transform.position + this.transform.right, this.transform.rotation) as GameObject;

            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();

            bulletRB.velocity = this.transform.forward * bulletSpeed;
            rockInput = false;
        }
        if (secretInput)
        {
            GameObject newBullet = Instantiate(secretCrash, this.transform.position + this.transform.right, this.transform.rotation) as GameObject;

            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();

            bulletRB.velocity = this.transform.forward * bulletSpeed;
            secretInput = false;
        }

        Vector3 rotation = Vector3.up * hInput;

        Quaternion angleRot = Quaternion.Euler(rotation *
            Time.fixedDeltaTime);

        _rb.MovePosition(this.transform.position +
            this.transform.forward * vInput * Time.fixedDeltaTime);

        _rb.MoveRotation(_rb.rotation * angleRot);
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);

        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);

        return grounded;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            _gameManager.HP -= 1;
        }
    }
}

