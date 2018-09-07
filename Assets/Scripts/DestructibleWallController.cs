using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アタッチするオブジェクトに対して必須コンポーネント追加を義務付ける
[RequireComponent(typeof(BoxCollider2D))]

public class DestructibleWallController : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        //プレイヤーと当たり判定があった場合プレイヤーのPlayerController.cs取得。且つm_typeの値がLargeだった場合壁破壊。
        if (collision.gameObject.tag == "Player")
        {
           PlayerController pc = collision.gameObject.GetComponent<PlayerController>();

            if (pc.m_type == Type.Large)
            {
                Destroy(gameObject);
            }
        }
    }
}
