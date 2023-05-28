using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolController : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private float _angle;
    private Transform _tr;
    private Player _player;
    private void Start()
    {
        _tr = GetComponent<Transform>();
        _player = FindObjectOfType<Player>();
        
    }
    private bool IsPlayerVisible()
    {
       
        Vector2 playerPosition = _player.transform.position;
        Vector2 position = new Vector2(_tr.position.x, _tr.position.y);
        Vector2 guardFacing = new Vector2(Mathf.Sign(_tr.localScale.x),0);
        Vector2 dir = playerPosition - position;

        if (Vector2.Distance(playerPosition, position)  > _distance) return false;
        float angle = Mathf.Acos(Vector2.Dot(guardFacing.normalized, dir.normalized)) * Mathf.Rad2Deg;

        return angle < _angle && angle > -_angle;
    }
    private void OnDrawGizmos()
    {
        float ctg = 1 / Mathf.Tan(_angle * Mathf.Deg2Rad);
        float cos = Mathf.Cos(_angle * Mathf.Deg2Rad);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(_distance * Mathf.Sign(transform.localScale.x), 0, 0));  
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(_distance * cos * Mathf.Sign(transform.localScale.x), new Vector3(_distance, _distance / ctg).y));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(_distance * cos * Mathf.Sign(transform.localScale.x), new Vector3(_distance, -_distance / ctg).y));
    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(140, 20, 100, 50), new GUIContent("IsPlayerVisible")))
        {
           Debug.Log(IsPlayerVisible());
        }
    }

}
