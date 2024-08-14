using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {
        public float baseSpeed = 3f; // 기본 속도
        private float currentSpeed;
        private Animator animator;
        private MapOpen mapOpen;
        private Rigidbody2D rb;

        // 만약 MapOpen이 다른 게임 오브젝트에 있을 경우 참조
        public GameObject mapOpenObject;

        private void Start()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();

            if (mapOpenObject != null)
            {
                mapOpen = mapOpenObject.GetComponent<MapOpen>();
            }
            else
            {
                mapOpen = GetComponent<MapOpen>();
            }

            if (animator == null)
            {
                Debug.LogError("Animator component is missing from this GameObject.");
            }
            if (mapOpen == null)
            {
                Debug.LogWarning("MapOpen component is not attached to this GameObject or specified MapOpenObject.");
            }
            if (rb == null)
            {
                Debug.LogError("Rigidbody2D component is missing from this GameObject.");
            }
        }

        private void Update()
        {
            if (mapOpen == null)
            {
                Debug.LogWarning("mapOpen is null. Make sure the MapOpen component is attached to this GameObject or specified MapOpenObject.");
                return;
            }

            currentSpeed = mapOpen.OnMap ? baseSpeed : 0f;

            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
                animator.SetInteger("Direction", 3);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
                animator.SetInteger("Direction", 2);
            }

            if (Input.GetKey(KeyCode.W))
            {
                dir.y = 1;
                animator.SetInteger("Direction", 1);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
                animator.SetInteger("Direction", 0);
            }

            dir.Normalize();
            animator.SetBool("IsMoving", dir.magnitude > 0);

            if (rb != null)
            {
                rb.velocity = currentSpeed * dir;
            }
        }
    }
}
