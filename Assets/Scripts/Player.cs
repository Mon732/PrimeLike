using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public float lookAngle;
    public float jumpRayLength;

    Rigidbody rigidbody;
    public bool onGround;
    GameObject camera;
    Vector3 movementVector;
    bool jumping;
    

	// Use this for initialization
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
        onGround = true;
        jumping = false;
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        //jumpRayLength = 1.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameManager.Instance.GetState() == GameManager.GameState.Game)
        {
            movementVector = Vector3.zero;

            if (InputManager.Instance.GetKey(InputManager.Controls.Forward))
            {
                movementVector += transform.forward;
            }

            if (InputManager.Instance.GetKey(InputManager.Controls.Backward))
            {
                movementVector -= transform.forward;
            }

            if (InputManager.Instance.GetKey(InputManager.Controls.Left))
            {
                movementVector -= transform.right;
            }

            if (InputManager.Instance.GetKey(InputManager.Controls.Right))
            {
                movementVector += transform.right;
            }

            movementVector.Normalize();

            if (InputManager.Instance.GetKeyDown(InputManager.Controls.Jump))
            {
                if (onGround)
                {
                    movementVector += transform.up;
                    jumping = true;
                    onGround = false;
                }
            }
            else if (jumping)
            {
                movementVector += transform.up;
            }

            Vector2 mouseAxes = InputManager.Instance.GetMouseAxes();
            float mouseStrength = 5.0f;

            Quaternion playerRot = transform.rotation * Quaternion.Euler(0, mouseAxes.x * mouseStrength, 0);
            transform.rotation = playerRot;

            Quaternion cameraRot = camera.transform.rotation * Quaternion.Euler(-mouseAxes.y * mouseStrength, 0, 0);
            Quaternion cameraForward = playerRot * Quaternion.Euler(camera.transform.forward);

            if (Quaternion.Angle(cameraForward, cameraRot) < lookAngle)
            {
                camera.transform.rotation = cameraRot;
            }
        }

        if (movementVector == Vector3.zero)
        {
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
        }
        else
        {
            movementVector *= 100 * Time.deltaTime;

            if (jumping)
            {
                movementVector.y *= jumpSpeed;
                jumping = false;
            }
            else
            {
                movementVector.y = rigidbody.velocity.y;
            }

            rigidbody.velocity = new Vector3(movementVector.x * speed, movementVector.y, movementVector.z * speed);

            //Debug.Log("Velocity: " + rigidbody.velocity);
            //Debug.Log("movementVector: " + movementVector);
        }

        Ray ray = new Ray(transform.position + new Vector3(0, 0.1f, 0), -transform.up);
        //int layerMask = 1 << 8;

        Debug.DrawRay(transform.position + new Vector3(0, 0.1f, 0), -transform.up * jumpRayLength);

        if (Physics.Raycast(ray, jumpRayLength))
        {
            if (rigidbody.velocity.y <= 0)
            {
                onGround = true;
                //Debug.Log("On ground");
            }
        }
        else if (onGround)
        {
            onGround = false;
        }

        //DEBUG CODE
        if (Input.GetKeyDown(KeyCode.X))
        {
            Time.timeScale = 0.1f;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            GameManager.Instance.SaveSettings();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            GameManager.Instance.LoadSettings();
        }
	}

    void FixedUpdate()
    {
        
    }
}
