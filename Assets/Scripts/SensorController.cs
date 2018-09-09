using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//アタッチするオブジェクトに対して必須コンポーネント追加を義務付ける
[RequireComponent(typeof(BoxCollider2D))]

public class SensorController : MonoBehaviour
{
    /// <summary>プレイヤーの重さを記憶する変数</summary>
    private int m_weightBuffer;
    /// <summary>GameObject of MovingWall</summary>
    GameObject m_go;
    /// <summary>MovingController</summary>
    MovingWallController m_mwc;
    PlayerController m_pc;

    // Use this for initialization
    void Start ()
    {
        m_go = GameObject.Find("MovingWall");
        m_mwc = m_go.GetComponent<MovingWallController>();
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_pc = collision.gameObject.GetComponent<PlayerController>();
            m_weightBuffer += m_pc.m_myStatus.weight;

            if (m_weightBuffer >= 4)
            {
                m_mwc.MoveUp();
            }
            Debug.Log("OnTriggerEnter2D : " + m_weightBuffer);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            m_pc = collision.gameObject.GetComponent<PlayerController>();
            m_weightBuffer -= m_pc.m_myStatus.weight;
            m_mwc.MoveDown();
            Debug.Log("OnTriggerExit2D : " + m_weightBuffer);
        }


    }
}