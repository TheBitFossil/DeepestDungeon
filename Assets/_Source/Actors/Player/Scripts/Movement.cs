using System.Collections;
using System.Collections.Generic;
using _Source.Coordinators;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private bool isMovementAllowed;
    [SerializeField] private float speed;

    [Header("Dodge")] [SerializeField] private bool isDodging;
    [SerializeField] private bool canDodge = true;
    [SerializeField] private float dodgeSpeed = 1.25f;
    [SerializeField] private float dodgeTime = .12f;
    [SerializeField] private float dodgeCoolDownTimer = 3f;
    
    private enum DodgeDirection
    {
        Back = 0,
        Forward = 1,
    }

    private DodgeDirection dodgeDirection;
    
    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 20;
    [SerializeField] private float rotationDamping;
    [SerializeField] private Animator animator;
    [SerializeField] private float animatorDampTime;

    [Header("Sound Effects")] 
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSourceDodge;
    [SerializeField] private List<AudioClip> walkSounds;
    [SerializeField] private List<AudioClip> dodgeSounds;
    [SerializeField] private float minPitch = .8f;
    [SerializeField] private float maxPitch = 1f;
    
    private Camera cam;
    private bool stopControls = false;
    private readonly int baseMoveSpeedHash = Animator.StringToHash("moveSpeed");
    
    public bool StopControls
    {
        get => stopControls;
        set => stopControls = value;
    }
    
    #region Events
    private void OnEnable()
    {
        InputReader.BaseAttackEvent += OnAttackStopMovement;
        InputReader.HeavyAttackEvent += OnAttackStopMovement;
        InputReader.DodgeEvent += OnDodge;
    }

    private void OnDestroy()
    {
        InputReader.BaseAttackEvent -= OnAttackStopMovement;
        InputReader.HeavyAttackEvent -= OnAttackStopMovement;
        InputReader.DodgeEvent -= OnDodge;
    }
    
    private void OnAttackStopMovement()
    {
        isMovementAllowed = false;
    }

    #endregion

    public float Speed
    {
        get => speed;
        set => speed = value;
    }
    
    #region Unity Engine

    private void Awake()
    {
        if(cam == null && Camera.main != null)
            cam = Camera.main;
    }

    private void Update()
    {
        if (stopControls) return;
        
        if (!isMovementAllowed)
        {
            InputReader.StopForwardMovement = true;
            AnimationDuringAttack();
        }
        else if (isDodging)
        {
            AnimationDuringDodge();
        }
        else if(!isDodging && isMovementAllowed)
        {
            InputReader.StopForwardMovement = false;
            
            var velocity = AlignPlayerAndCameraRotation();

            AnimationDuringMovement(velocity);
        }
    }
    #endregion

    #region Methods

    private Vector3 AlignPlayerAndCameraRotation()
    {
        Vector3 camForward = cam.transform.forward;
        Vector3 camRight = cam.transform.right;

        camForward.y = 0;
        camRight.y = 0;
        
        camForward.Normalize();
        camRight.Normalize();

        float playerVerticalVelocity = InputReader.MovementValue.y;
        float playerHorizontalVelocity = InputReader.MovementValue.x;

        return camForward * playerVerticalVelocity +
               camRight * playerHorizontalVelocity;
    }

    private void FaceMovementDirection(Vector3 velocity, float deltaTime)
    {
        transform.rotation = Quaternion.Lerp(
            transform.rotation
            , Quaternion.LookRotation(velocity)
            , deltaTime * rotationDamping );
    }
    
    private void AnimationDuringMovement(Vector3 velocity)
    {
        if (InputReader.MovementValue != Vector2.zero)
        {
            FaceMovementDirection(velocity, Time.deltaTime);
            characterController.Move(velocity * (Time.deltaTime * speed));
            animator.SetFloat(baseMoveSpeedHash, 1, animatorDampTime, Time.deltaTime);
        }
        else if (InputReader.MovementValue == Vector2.zero)
        {
            characterController.Move(Vector3.zero);
            animator.SetFloat(baseMoveSpeedHash, 0f, animatorDampTime, Time.deltaTime);
        }
    }

    private void AnimationDuringDodge()
    {
        var steering = Vector3.zero;
        
        switch (dodgeDirection)
        {
            case DodgeDirection.Back:
                steering = transform.forward *-1;
                break;
            case DodgeDirection.Forward:
                steering = transform.forward;
                break;
        }
        characterController.Move(steering * (speed * (Time.deltaTime * dodgeSpeed)));
    }

    private void OnDodge()
    {
        if(isDodging || !canDodge) return;
        canDodge = false;
        isMovementAllowed = true;
        
        if (false == InputReader.IsStandingStill)
            dodgeDirection = DodgeDirection.Forward;
        else
            dodgeDirection = DodgeDirection.Back;
        
        StartCoroutine(Dodge());
    }

    private IEnumerator Dodge()
    {
        isDodging = true;
        PlayDashSound();
        animator.SetBool("isDodging", true);
        yield return new WaitForSeconds(dodgeTime);
        isDodging = false;
        animator.SetBool("isDodging", false);
        StartCoroutine(DodgeCoolDown());
    }

    private IEnumerator DodgeCoolDown()
    {
        canDodge = false;
        yield return new WaitForSeconds(dodgeCoolDownTimer);
        canDodge = true;
    }

    private void AnimationDuringAttack()
    {
        float playerHorizontalVelocity = InputReader.MovementValue.x;
        
        if (InputReader.MovementValue.x > .2f)
        {
            transform.Rotate(new Vector3(0, playerHorizontalVelocity * rotationSpeed, 0) * Time.deltaTime);
        }
        else if(InputReader.MovementValue.x < -.2f)
        {
            transform.Rotate(new Vector3(0, playerHorizontalVelocity * rotationSpeed, 0) * Time.deltaTime);
        }
    }
    #endregion

    #region Animation MethodCall
    public void ResumePlayerMovementAfterAttack(bool isActive)
    {
        isMovementAllowed = isActive;
    }

    public void PlayWalkSound()
    {
        var rng = Random.Range(0, walkSounds.Count);

        audioSource.pitch = Random.Range(minPitch, maxPitch);
        AudioSource.PlayClipAtPoint(walkSounds[rng], transform.position);
    }
    
    public void PlayDashSound()
    {
        var rng = Random.Range(0, dodgeSounds.Count);

        audioSourceDodge.pitch = Random.Range(minPitch, maxPitch);
        AudioSource.PlayClipAtPoint(dodgeSounds[rng], transform.position);
    }
    #endregion
    
}