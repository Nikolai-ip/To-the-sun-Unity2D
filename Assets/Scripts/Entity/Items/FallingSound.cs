using AISystem;
using DefaultNamespace;
using UnityEngine;

public class FallingSound : MonoBehaviour
{
    [SerializeField] private float _soundRadius;
    [SerializeField] private AudioSource _fallingAudio;
    public bool InFlight { get; set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //_fallingAudio.Play();
        if (InFlight)
        {
            InFlight = false;
            var enemyColliders =
                Physics2D.OverlapCircleAll(transform.position, _soundRadius, LayerMask.GetMask("Enemy"));
            StateMachine[] enemies;
            TryGetSMComponents(enemyColliders, out enemies);
            enemies.RemoveSameObjects();
            foreach (var enemy in enemies) enemy?.HearNoise();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _soundRadius);
    }

    private void TryGetSMComponents(Collider2D[] enemyColliders, out StateMachine[] enemies)
    {
        enemies = new StateMachine[enemyColliders.Length];
        for (var i = 0; i < enemyColliders.Length; i++)
            if (enemyColliders[i].TryGetComponent(out StateMachine enemy))
                enemies[i] = enemy;
    }
}