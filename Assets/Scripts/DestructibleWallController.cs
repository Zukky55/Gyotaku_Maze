using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アタッチするオブジェクトに対して必須コンポーネント追加を義務付ける
[RequireComponent(typeof(BoxCollider2D))]

public class DestructibleWallController : MonoBehaviour
{
    private PlayerController m_pc;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "LargePlayer") //今後ここの名前は適宜変更する
        {
            m_pc = collision.gameObject.GetComponent<PlayerController>();
            if (m_pc.m_cbc2d.enabled == true)
            {
                Destroy(gameObject);
            }
        }
    }
}
