using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    [SerializeField] private int _countReferencePoints;
    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void ToogleLineRenderer(bool isEnabled)
    {
        _lineRenderer.enabled = isEnabled;
    }

    public void ShowTrajectory(Vector2 origin, Vector2 speed)
    {
        var points = new Vector3[_countReferencePoints];
        _lineRenderer.positionCount = points.Length;

        for (var i = 0; i < points.Length; i++)
        {
            var time = i * 0.1f;

            points[i] = origin + speed * time + Physics2D.gravity * time * time / 2f;

            if (points[i].y < -10)
            {
                _lineRenderer.positionCount = i + 1;
                break;
            }
        }

        _lineRenderer.SetPositions(points);
    }
}