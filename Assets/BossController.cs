using UnityEngine;
using Invector;
using System.Collections;

public class BossController : MonoBehaviour
{
    public enum BossState { Flying, Landing, Vulnerable, TakingOff, Dead }
    private BossState currentState = BossState.Flying;

    [Header("Stats")]
    public int maxHealth = 2000;
    public int damagePerHit = 50;
    public float attackRange = 3f;
    public float attackCooldown = 2f;

    [Header("Cycle Timings")]
    public float flyTime = 5f;
    public float landTime = 1.5f;
    public float vulnerableTime = 6f;
    public float takeOffTime = 2f;

    [Header("Flame Attack")]
    public GameObject flameEffect;                // prefab-ul de foc (vizual)
    public Collider flameDamageZone;              // collider pentru damage AOE
    public float flameDuration = 1.5f;            // cât ține flacăra

    [Header("Audio")]
    public AudioClip sleepSound;
    public AudioClip roarSound;

    private AudioSource audioSource;

    private int currentHealth;
    private float stateTimer = 0f;
    private float lastAttackTime = 0f;


    private Animator animator;
    private Transform player;
    private bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
        EnterState(BossState.Flying);

        if (flameDamageZone != null)
            flameDamageZone.enabled = false;

        // Asigură-te că flameEffect e activ, dar PlayOnAwake e debifat în ParticleSystem
    }

    void Update()
    {
        if (isDead || player == null) return;

        stateTimer += Time.deltaTime;

        switch (currentState)
        {
            case BossState.Flying:
                if (stateTimer >= flyTime)
                    EnterState(BossState.Landing);
                break;

            case BossState.Landing:
                if (stateTimer >= landTime)
                    EnterState(BossState.Vulnerable);
                break;

            case BossState.Vulnerable:
                HandleCombat();
                if (stateTimer >= vulnerableTime)
                    EnterState(BossState.TakingOff);
                break;

            case BossState.TakingOff:
                if (stateTimer >= takeOffTime)
                    EnterState(BossState.Flying);
                break;
        }
    }

    void EnterState(BossState newState)
    {
        currentState = newState;
        stateTimer = 0f;

        switch (newState)
        {
            case BossState.Flying:
                animator.SetTrigger("TakeOff");
                PlaySound(roarSound);
                break;

            case BossState.Landing:
                animator.SetTrigger("Land");
                break;

            case BossState.Vulnerable:
                animator.SetTrigger("Sleep");
                PlaySound(sleepSound);
                break;

            case BossState.TakingOff:
                animator.SetTrigger("TakeOff");
                PlaySound(roarSound);
                break;

            case BossState.Dead:
                animator.SetTrigger("Die");
                break;
        }

    }

    void HandleCombat()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist <= attackRange && Time.time - lastAttackTime >= attackCooldown)
        {
            int roll = Random.Range(0, 100);

            if (roll < 80)
            {
                animator.SetTrigger("Flame Attack");
                StartCoroutine(PlayFlameEffect());
            }
            else
            {
                animator.SetTrigger("Scream");
            }

            lastAttackTime = Time.time;
        }
    }

    private IEnumerator PlayFlameEffect()
    {
        Debug.Log("🔥 Pornim flacăra!");

        if (flameEffect != null)
        {
            var ps = flameEffect.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                ps.Play();
            }
        }

        if (flameDamageZone != null)
            flameDamageZone.enabled = true;

        yield return new WaitForSeconds(flameDuration);

        if (flameDamageZone != null)
            flameDamageZone.enabled = false;

        if (flameEffect != null)
        {
            var ps = flameEffect.GetComponent<ParticleSystem>();
            if (ps != null && ps.isPlaying)
            {
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        if (currentState != BossState.Vulnerable || isDead) return;

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            isDead = true;
            EnterState(BossState.Dead);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (flameDamageZone != null && flameDamageZone.enabled &&
            other.CompareTag("Player"))
        {
            var health = other.GetComponent<vHealthController>();
            if (health != null)
            {
                health.TakeDamage(new vDamage(damagePerHit));
            }
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

}
