using UnityEngine;

public class LineTraectory : MonoBehaviour
{
    [SerializeField] private float _widthCurve;
    private LineRenderer _lineRenderer;
    private Vector2 _prevPosition;
    private Transform _tr;

    private void Start()
    {
        _tr = transform;
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.startWidth = _widthCurve;
        _lineRenderer.endWidth = _widthCurve;
        _lineRenderer.startColor = Color.cyan;
        _lineRenderer.endColor = Color.cyan;
    }

    private void FixedUpdate()
    {
        Vector2 newPosition = _tr.position;
        if (!(Mathf.Approximately(_prevPosition.x, newPosition.x) &&
              Mathf.Approximately(_prevPosition.y, newPosition.y)))
        {
            _lineRenderer.positionCount++;
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, newPosition);
            _prevPosition = newPosition;
        }
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 100, 50), new GUIContent("Clean line"))) CleanLine();
    }

    private void CleanLine()
    {
        _lineRenderer.positionCount = 0;
    }
}