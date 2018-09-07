using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWallController : MonoBehaviour
{
    /// <summary>MoveUp() の停止地点</summary>
    [SerializeField] Transform m_moveUpAnchor;
    /// <summary>MoveDown() の停止地点</summary>
    [SerializeField] Transform m_moveDownAnchor;
    /// <summary>MovingWall の移動時間（停止地点までたどり着く時間）</summary>
    [SerializeField] float m_movingTime = 5.0f;

    public void MoveUp()
    {
        // MovingWall を m_movingWallAnchor まで動かす
        iTween.MoveTo(gameObject, m_moveUpAnchor.position, m_movingTime);
    }
    public void MoveDown()
    {
        iTween.MoveTo(gameObject, m_moveDownAnchor.position, m_movingTime);
    }
}
