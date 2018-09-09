using UnityEngine;
using System.Collections;
public class CameraController : MonoBehaviour
{
    /// <summary>プレイヤーオブジェクト</summary>
    [SerializeField] GameObject m_player;
    /// <summary>オフセット(プレイヤーとカメラ位置の差異)</summary>
    private Vector3 m_offset;
    private void Start()
    {
        m_offset = transform.position - m_player.transform.position;
    }
    private void LateUpdate()
    {
        transform.position = m_player.transform.position + m_offset;
    }
}