using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class mamatosenLight : MonoBehaviour
{

    public float range = 1;
    public float intensity = 1;
    [Range(0, 360)]public float angle = 360;
    public int resolution;
    public int edgeFinderRep = 4;
    public float edgeFinderThreshold = 0.5f;
    public bool sw = false;
    public spriteMaker sm;
    public MeshFilter viewMeshFilter;
    

    Mesh viewMesh;

    void Start()
    {
        viewMeshFilter = GetComponent<MeshFilter>();
        viewMesh = new Mesh();
        viewMesh.name = "view mesh";
        viewMeshFilter.mesh = viewMesh;
    }

    public struct cast
    {
        public float angle;
        public Vector3 point;
        public Collider2D collider;

        public cast(float _angle, Vector3 _point, Collider2D _collider)
        {
            angle = _angle;
            point = _point;
            collider = _collider;
        }
    }

    List<cast> rays = new List<cast>();

    void OnDrawGizmosSelected()
    {
        float halfFOV = angle / 2.0f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.back);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.back);
        Vector3 leftRayDirection = leftRayRotation * transform.right;
        Vector3 rightRayDirection = rightRayRotation * transform.right;
        Gizmos.DrawRay(transform.position, leftRayDirection * range);
        Gizmos.DrawRay(transform.position, rightRayDirection * range);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (sw)
        {
            drawRays();
        }
    }

    void drawRays()
    {

        rays.Clear();

        float step = (angle / resolution);

        for (int i = 0; i < resolution; i++)
        {
            float rayAngle = (i - resolution / 2) * step;

            cast newRay = DrawRay(rayAngle);
            cast lastRay = new cast();

            if(i > 0)
            {
                lastRay = rays[rays.Count - 1];
            }
            else
            {
                lastRay = newRay;
            }

            if (newRay.collider == lastRay.collider)
            {
                rays.Add(newRay);
            }
            else
            {
                findEdge(lastRay, newRay);
            }

        }

        int vertexCount = rays.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[3 * (vertexCount - 2)];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            
            vertices[i + 1] = transform.InverseTransformPoint(rays[i].point);

            if (i == vertexCount - 2)
                continue;
            triangles[3 * i] = 0;
            triangles[3 * i + 1] = i + 1;
            triangles[3 * i + 2] = i + 2;
        }

        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;

    }

    Vector3 dirFromAngle(float angle)
    {
        Quaternion rayRotation = Quaternion.AngleAxis(angle, Vector3.back);
        return(rayRotation * transform.right);
    }

    cast DrawRay(float angle)
    {
        Vector3 dir = dirFromAngle(angle);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, range);
        //Debug.DrawRay(transform.position, dir * range);

        if (hit.collider)
        {
            return new cast(angle, hit.point, hit.collider);
        }
        else
        {
            return new cast(angle, (Vector2)transform.position + range * (Vector2)dir, hit.collider);
        }
    }

    void findEdge(cast lastRay, cast newRay)
    {
        for(int i = 0; i < edgeFinderRep; i++)
        {
            cast middle = DrawRay((lastRay.angle + newRay.angle) / 2);

            if(middle.collider == lastRay.collider)
            {
                lastRay = middle;
            }
            if(middle.collider == newRay.collider)
            {
                newRay = middle;
            }
        }

        rays.Add(lastRay);
        rays.Add(newRay);
    }
}
