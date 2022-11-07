using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeMovement : MonoBehaviour
{

    [SerializeField] private float slopeCheckDistance;
    [SerializeField] private LayerMask jumpableGround;
    private BoxCollider2D collider;

    private Vector2 colliderSize;
    private Vector2 slopeNormalPerpendicular;

    private float slopeDownAngle;
    private float slopeDownAngleOld;

    private bool isOnSlope;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        colliderSize = collider.size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        SlopeCheck();
    }

    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - new Vector3(0.0f, colliderSize.y / 2);

        VerticalSlope(checkPos);
    }

    private void HorizontalSlope(Vector2 checkPos)
    {

    }

    private void VerticalSlope(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, jumpableGround);

        if (hit)
        {
            slopeNormalPerpendicular = Vector2.Perpendicular(hit.normal);

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            Debug.DrawRay(hit.point, slopeNormalPerpendicular, Color.red);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }
    }
}
