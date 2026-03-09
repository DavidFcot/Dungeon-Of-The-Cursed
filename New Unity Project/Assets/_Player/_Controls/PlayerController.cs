using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInput playerInput;
    PlayerInput.OnFootActions input;

    CharacterController controller;
    Animator animator;
    AudioSource audioSource;

    Vector3 _PlayerVelocity;

    public bool swordEquipped = false;

    [Header("Camera")]
    public Camera cam;

    void Awake()
    { 
        //Gets all needed components + inputs.
        controller = GetComponentInParent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();

        playerInput = new PlayerInput();
        input = playerInput.OnFoot;
        AssignAttack();
    }

    void Update()
    {
        if(swordEquipped == true)
        {
            //Repeats inputs.
            if(input.Attack.IsPressed())
            { 
                Attack(); 
            }

            SetAnimations();
        }
    }

    void OnEnable() 
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }

    void AssignAttack()
    {
        input.Attack.started += ctx => Attack();
    }

    [Header("Attacking")]
    //Attack Stats
    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public int attackDamage = 1;
    public LayerMask attackLayer;

    public GameObject hitEffect;
    public AudioClip swordSwing;
    public AudioClip hitSound;

    bool attacking = false;
    bool readyToAttack = true;
    int attackCount;

    public void Attack()
    {
        if(swordEquipped == true)
        {
            if(!readyToAttack || attacking) return;

            readyToAttack = false;
            attacking = true;

            Invoke(nameof(ResetAttack), attackSpeed);
            Invoke(nameof(AttackRaycast), attackDelay);

            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(swordSwing);

            //Switches between the 2 attack animations.
            if(attackCount == 0)
            {
                ChangeAnimationState(ATTACK1);
                attackCount++;
            }
            else
            {
                ChangeAnimationState(ATTACK2);
                attackCount = 0;
            }
        }
    }

    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }

    void AttackRaycast()
    {
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        { 
            HitTarget(hit.point);

            if(hit.transform.TryGetComponent<Actor>(out Actor T))
            {
                //Deals Damage to the actor if they are hit.
                T.TakeDamage(attackDamage);
            }
        } 
    }

    void HitTarget(Vector3 pos)
    {
        audioSource.pitch = 1;
        audioSource.PlayOneShot(hitSound);

        GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
        Destroy(GO, 20);
    }

    //Stores the Animations.
    public const string IDLE = "Idle";
    public const string WALK = "Walk";
    public const string ATTACK1 = "Attack 1";
    public const string ATTACK2 = "Attack 2";

    string currentAnimationState;

    public void ChangeAnimationState(string newState) 
    {
        //Stops the same animation from inturrupting with itself.
        if (currentAnimationState == newState) return;

        //Plays the animations.
        currentAnimationState = newState;
        animator.CrossFadeInFixedTime(currentAnimationState, 0.2f);
    }

    void SetAnimations()
    {
        //These will play if the player is not attacking
        if(!attacking)
        {
            if(_PlayerVelocity.x == 0 &&_PlayerVelocity.z == 0)
            {
                //Plays idle animation if player is not moving.
                ChangeAnimationState(IDLE);
            }
            else
            {
                //Plays walk animation if player is moving.
                ChangeAnimationState(WALK);
            }
        }
    }
}