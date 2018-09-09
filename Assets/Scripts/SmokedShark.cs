using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokedShark : PlayerController
{
    /// <summary>Judgment of hitting hark's destruction skill</summary>
//    internal BoxCollider2D m_cbc2dl;

    private void Start()
    {
            m_cbc2d = GetComponentInChildren<BoxCollider2D>();
    }

    private void Update()
    {
        if (m_type == Type.Large)
        {
            if (Input.GetKeyDown(KeyCode.Space))  // スペースを押したとき
            {
                m_cbc2d.enabled = true;         // AttackRange の　BoxCollider2D を enable にする
            }
            if (Input.GetKeyUp(KeyCode.Space))    // スペースを離したとき
            {
                m_cbc2d.enabled = false;        // AttackRange の　BoxCollider2D を disable にする
            }
        }
    }
}
