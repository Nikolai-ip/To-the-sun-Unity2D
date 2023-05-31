using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideController : MonoBehaviour
{
    [SerializeField] private Transform _leftHideBorder;
    [SerializeField] private Transform _rightHideBorder;
    [SerializeField] private float _radius;
    public bool IsHidden { get; private set; }
    private void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        if (_leftHideBorder && _rightHideBorder)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_leftHideBorder.position, _radius);
            Gizmos.DrawWireSphere(_rightHideBorder.position, _radius);
        }
    }
}
