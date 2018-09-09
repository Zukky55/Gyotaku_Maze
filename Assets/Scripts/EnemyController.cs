using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Enemy's status</summary>
[System.Serializable]
struct EnemyStatus
{
    /// <summary>Player's speed</summary>
    public float m_speed;
}

//アタッチするオブジェクトに対して必須コンポーネント追加を義務付ける
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class EnemyController : MonoBehaviour
{
    /// <summary>Start position</sumTmary>
    [SerializeField] Vector3 m_startPos;
    /// <summary>Position at end of frame</sumTmary>
    [SerializeField] Vector3 m_endPos;
    /// <summary>Set time</summary>
    [SerializeField] float m_time;
    /// <summary>Amount of movement per second</summary>
    private Vector3 m_deltaPos;
    /// <summary>Elapsed time count</summary>
    private float m_elapsedTime;
    /// <summary>Fragment of end</summary>
    private bool m_directionToggle = true;

    private void Start()
    {

        // StartPosをオブジェクトに初期位置に設定
        transform.position = m_startPos;
        // 1秒当たりの移動量を算出
        m_deltaPos = (m_endPos - m_startPos) / m_time;
        m_elapsedTime = 0;
        //transform.rotation = Quaternion.FromToRotation(Vector3.up, m_endPos);
    }

    void Update()
    {
        // 1秒当たりの移動量にTime.deltaTimeを掛けると1フレーム当たりの移動量となる
        // Time.deltaTimeは前回Updateが呼ばれてからの経過時間
        transform.position += m_deltaPos * Time.deltaTime;
        // 往路復路反転用経過時間
        m_elapsedTime += Time.deltaTime;
        // 移動開始してからの経過時間がtimeを超えると往路復路反転
        if (m_elapsedTime > m_time)
        {
            if (m_directionToggle)
            {
                // StartPos→EndPosだったので反転してEndPos→StartPosにする
                // 現在の位置がEndPosなので StartPos - EndPosでEndPos→StartPosの移動量になる
                m_deltaPos = (m_startPos - m_endPos) / m_time;
                // 誤差があるとずれる可能性があるので念のためオブジェクトの位置をEndPosに設定
                transform.position = m_endPos;
                //transform.rotation = Quaternion.FromToRotation(Vector3.up, m_endPos);
            }
            else
            {
                // m_endPos→m_startPosだったので反転してにm_startPos→m_endPosする
                // 現在の位置がm_startPosなので m_endPos - m_startPos→m_endPosの移動量になる
                m_deltaPos = (m_endPos - m_startPos) / m_time;
                // 誤差があるとずれる可能性があるので念のためオブジェクトの位置をSrartPosに設定
                transform.position = m_startPos;
                //transform.rotation = Quaternion.FromToRotation(Vector3.up, m_startPos);
            }
            // 往路復路のフラグ反転
            m_directionToggle = !m_directionToggle;
            // 往路復路反転用経過時間クリア
            m_elapsedTime = 0;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            PlayerController pc;
            pc = collision.gameObject.GetComponent<PlayerController>();
            pc.m_rb2d.position = pc.m_initPos;
        }
    }
}
