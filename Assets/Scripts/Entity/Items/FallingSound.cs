using UnityEngine;

public class FallingSound : MonoBehaviour
{
    [SerializeField] private float _soundRadius;
    [SerializeField] private AudioSource _fallingAudio;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //_fallingAudio.Play();

        var enemys = Physics2D.OverlapCircleAll(transform.position, _soundRadius, LayerMask.GetMask("Enemy")); 
        
        foreach (var enemy in enemys)
        {
            Debug.Log(enemy.gameObject.name);
            //if (enemy.TryGetComponent(out StateMachine enemyStateMachine)) 
            //{ 
            //    enemyStateMachine.HearNoise(transform.position); 
            //} 
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _soundRadius);
    }
}
