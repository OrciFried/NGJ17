using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PowerTools;

[RequireComponent(typeof(SpriteAnim)), RequireComponent(typeof(SpriteRenderer)), RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField, Tooltip("Speed and Idle threshold speed")]
    private float f_speed = 200f, f_threshold = 0.1f;

    [SerializeField]
    private AnimationClip m_run, m_idle;

    private SpriteAnim SPRITEANIM = null;
    private Rigidbody2D RB2D = null;
    private int turnDirection = 1;

    private void Start()
    {
        RB2D = GetComponent<Rigidbody2D>();
        SPRITEANIM = GetComponent<SpriteAnim>();
        SPRITEANIM.Play(m_idle);
    }

    private void Update()
    {
        RB2D.velocity = new Vector3(Input.GetAxis("Horizontal") * f_speed * Time.deltaTime, RB2D.velocity.y, 0);

        if (Mathf.Abs(RB2D.velocity.x) >= f_threshold)
            HandleRunAnimation();

        if (Mathf.Abs(RB2D.velocity.x) < f_threshold && SPRITEANIM.Clip != m_idle)
            SPRITEANIM.Play(m_idle);
    }

    private void HandleRunAnimation()
    {
        if (SPRITEANIM.GetCurrentAnimation() != m_run)
            SPRITEANIM.Play(m_run);

        if (Input.GetAxis("Horizontal") > 0 && turnDirection != 1)
        {
            turnDirection = 1;
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetAxis("Horizontal") < 0 && turnDirection != -1)
        {
            turnDirection = -1;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}