using System.Collections;
using UnityEngine;

namespace FirstPerson.PlayerController
{

    public class PlayerMovement : MonoBehaviour
    {

        [Header("Movement")]
        [SerializeField] bool enableRun = true;
        [SerializeField] bool enableSneak = true;
        [SerializeField] float walkSpeed = 6.0f;
        [SerializeField] float runSpeed = 12.0f;
        [SerializeField] float sneakSpeed = 3.0f;
        [SerializeField] float speedBuildUp = 4.0f;

        private float movementSpeed;

        [Header("Jump")]
        [SerializeField] bool enableJump = true;
        [SerializeField] AnimationCurve jumpFallOff = default;
        [SerializeField] float jumpMultiplier = 10.0f;

        [Header("Slant Movement")]
        [SerializeField] float slopeForce = 5.0f;
        [SerializeField] float slopeRayLength = 1.5f;

        private float distanceToGround;

        private static bool canMove = true;


        #region Properties
        public static bool MovementLocked { set => canMove = !value; }
        public static bool IsJumping { get; private set; }
        #endregion


        #region Cached references
        private CharacterController character;
        #endregion


        private void Awake()
        {
            SetReferences();
        }

        private void Start()
        {
            distanceToGround = character.height / 2;
        }

        private void Update()
        {
            if (canMove)
            {
                MovementInput();
                JumpInput();
            }
        }

        private void MovementInput()
        {
            float horizontalInput = Input.GetAxis(Axis.Horizontal);
            float verticalInput = Input.GetAxis(Axis.Vertical);

            Vector3 forwardMovement = transform.forward * verticalInput;
            Vector3 rightMovement = transform.right * horizontalInput;

            character.SimpleMove(Vector3.ClampMagnitude(forwardMovement + rightMovement, 1.0f) * movementSpeed);

            if ((horizontalInput != 0 || verticalInput != 0) && OnSlope())
            {
                character.Move(Vector3.down * distanceToGround * slopeForce * Time.deltaTime);
            }

            SetSpeed();
        }

        private void SetSpeed()
        {
            float endSpeed = walkSpeed;

            if (enableRun && Input.GetButton(Control.Run))
            {
                endSpeed = runSpeed;
            }
            else if (enableSneak && Input.GetButton(Control.Sneak))
            {
                endSpeed = sneakSpeed;
            }

            movementSpeed = Mathf.Lerp(movementSpeed, endSpeed, Time.deltaTime * speedBuildUp);
        }

        private void JumpInput()
        {
            if (enableJump && IsJumping == false && Input.GetButtonDown(Control.Jump))
            {
                IsJumping = true;
                StartCoroutine(Jump());
            }
        }

        private IEnumerator Jump()
        {
            float timeInAir = 0.0f;
            character.slopeLimit = 90.0f;

            do
            {
                float jumpForce = jumpFallOff.Evaluate(timeInAir);
                character.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
                timeInAir += Time.deltaTime;

                yield return null;
            }
            while (!character.isGrounded && character.collisionFlags != CollisionFlags.Above);

            character.slopeLimit = 45.0f;
            IsJumping = false;
        }

        private bool OnSlope()
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            float length = distanceToGround * slopeRayLength;

            if (IsJumping == false && Physics.Raycast(ray, out RaycastHit hit, length))
            {
                if (hit.normal != Vector3.up)
                {
                    return true;
                }
            }
            return false;
        }

        private void SetReferences()
        {
            character = GetComponent<CharacterController>();
        }

    }

}
