using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Player type</summary>
public enum Type
{
    Small, Medium, Large
}

/// <summary>Player's ability</summary>
public enum Ability
{
    Through,Destroy,HighSpeed
}
/// <summary>Status of each player type</summary>
public struct MyStatus
{
    /// <summary>Player's speed</summary>
    public float speed { get; internal set; }
    /// <summary>Player's weight</summary>
    public int weight { get; internal set; }
    /// <summary>Ink gauge</summary>
    public float inkGuage { get; internal set; }
}


//アタッチするオブジェクトに対して必須コンポーネント追加を義務付ける
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{

    //各プロパティは、インスペクタ上で設定可能にしつつ他クラスからは読み取り専用にしたい為
    //バッキングフィールドをインスペクタ上で設定させ代入する様にして、使う変数自体は読み取り専用にしている
    /// <summary>this is backing field of "m_MyStatus"</summary>
    [SerializeField] private MyStatus m_SetMyStatus;
    /// <summary>Status of each player type</summary>
    public MyStatus m_myStatus
    {
        get { return m_SetMyStatus; }
        private set { m_myStatus = m_SetMyStatus; }
    }

    /// <summary>this is backing field of "m_type"</summary>
    [SerializeField] private Type m_setType;
    /// <summary>player's type</summary>
    public Type m_type
    {
        get { return m_setType; }
        private set { m_type = m_setType; }
    }

    /// <summary>player's ability</summary>
    [SerializeField] Ability m_ability;
    /// <summary>Rigidbody2D</summary>
    Rigidbody2D m_rb2d;
    /// <summary>Position of the previous frame</summary>
    private Vector3 m_offsetPos;

    //debug
    GameObject gameobject;
    MovingWallController movingWallController;

    /// <summary>Mover</summary>
    void Move()
    {
        //x,y軸を入力しそのベクトルを取得,速度に代入
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(x, y);
        m_rb2d.velocity = direction * m_myStatus.speed;
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
        //RigidBoy2Dを取得
        m_rb2d = GetComponent<Rigidbody2D>();
        //angularDragを設定可能最大値にして、壁等との接触による回転をほぼ無くす
        m_rb2d.angularDrag = 1000000;

        

        //プレイヤーのサイズジャンルに応じてステータスを決定する
        switch (m_type)
        {
            case Type.Small:
//                Debug.Log("this object is small type");
                m_SetMyStatus.speed = 5;
                m_SetMyStatus.weight = 1;
                break;
            case Type.Medium:
//                Debug.Log("this object is medium type");
                m_SetMyStatus.speed = 4;
                m_SetMyStatus.weight = 3;
                break;
            case Type.Large:
//                Debug.Log("this object is large type");
                m_SetMyStatus.speed = 3;
                m_SetMyStatus.weight = 5;
                break;
            default:
                Debug.Log("m_type is not set properly!!");
                break;
        }

        //今後はここにキャラクター名に応じてステータスを設定していくコードを書く。現時点で書いてあるのは見本
        switch(gameObject.name)
        {
            case "Koi":
                m_SetMyStatus.speed = 5;
                m_SetMyStatus.weight = 1;
                m_SetMyStatus.inkGuage = 1;
                m_ability = Ability.Through;

                break;
            default:
                break;
        }

        switch(m_ability)
        {
            case Ability.Through:
                break;
        }
    }

    void Start()
    {
        Initialize();

        //debug
        gameobject = GameObject.Find("MovingWall");
        movingWallController = gameobject.GetComponent<MovingWallController>();
    }

    void FixedUpdate()
    {
        Move();

        //debug
        if(Input.GetMouseButtonDown(0))
        {
            movingWallController.MoveUp();
        }
        if (Input.GetMouseButtonDown(1))
        {
            movingWallController.MoveDown();
        }


    }

}
