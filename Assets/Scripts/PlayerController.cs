using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Player type</summary>
public enum Type
{
    small, medium, large
}

/// <summary>Player's ability</summary>
public enum Ability
{
    thorough, destroy, highSpeed
}
/// <summary>Status of each player type</summary>
[System.Serializable]
struct MyStatus
{
    /// <summary>Player's speed</summary>
    public float speed;
    /// <summary>Player's weight</summary>
    public int weight;
    /// <summary>Ink gauge</summary>
    public float inkGuage;
}

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    /// <summary>Status of each player type</summary>
    [SerializeField] MyStatus m_MyStatus;
    /// <summary>player's type</summary>
    [SerializeField] Type m_type;
    /// <summary>player's ability</summary>
    [SerializeField] Ability m_ability;
    /// <summary>Rigidbody2D</summary>
    Rigidbody2D m_rb2d;
    /// <summary>Position of the previous frame</summary>
    private Vector3 m_offsetPos;

    /// <summary>Mover</summary>
    void Move()
    {
        //x,y軸を入力しそのベクトルを取得,速度に代入
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 direction = new Vector2(x, y);
        m_rb2d.velocity = direction * m_MyStatus.speed;
        //前フレームのオフセットを取り、現フレームとの差異が一定量以上ある時だけ回転する様にして差異が0の時に上を向いてしまわない様にする
        Vector3 diff = transform.position - m_offsetPos;
        if (diff.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
        }
        m_offsetPos = transform.position;
    }
    /// <summary>Change for each type</summary>
    void Initialize()
    {
        switch(m_type)
        {
            case Type.small:
                Debug.Log("this object is small type");
                m_MyStatus.speed = 5;
                m_MyStatus.weight = 1;
                break;
            case Type.medium:
                m_MyStatus.speed = 4;
                m_MyStatus.weight = 3;
                break;
            case Type.large:
                m_MyStatus.speed = 3;
                m_MyStatus.weight = 5;
                break;
            default:
                Debug.Log("m_type is not set properly!!");
                break;
        }

    }

    void Start()
    {
        Initialize();
        m_rb2d = GetComponent<Rigidbody2D>();
        
    }

    void FixedUpdate()
    {
        Move();


    }

}
