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


//アタッチするオブジェクトに対して必須コンポーネント追加を義務付ける
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    /// <summary>Status of each player type</summary>
    [SerializeField] MyStatus m_MyStatus;
    //m_typeをインスペクタ上で設定可能にしつつ他クラスからは読み取り専用にしたい為
    //バッキングフィールドをインスペクタ上で設定させm_typeに代入する様にして、m_type自体は読み取り専用にしている
    /// <summary>this is backing field of "m_type"</summary>
    [SerializeField] private Type m_setType;
    /// <summary>player's type</summary>
    public Type m_type
    {
        get { return m_setType; }
        private set { m_setType = m_type;}
    }
    /// <summary>player's ability</summary>
    [SerializeField] Ability m_ability;
    /// <summary>Rigidbody2D</summary>
    Rigidbody2D m_rb2d;
    /// <summary>Position of the previous frame</summary>
    private Vector3 m_offsetPos;

    GameObject gameobject;
    MovingWallController movingWallController;

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
        //RigidBoy2Dを取得
        m_rb2d = GetComponent<Rigidbody2D>();
        //angularDragを設定可能最大値にして、壁等との接触による回転をほぼ無くす
        m_rb2d.angularDrag = 1000000;

        

        //プレイヤーのサイズジャンルに応じてステータスを決定する
        switch (m_type)
        {
            case Type.Small:
                Debug.Log("this object is small type");
                m_MyStatus.speed = 5;
                m_MyStatus.weight = 1;
                gameObject.tag = "SmallPlayer";
                break;
            case Type.Medium:
                Debug.Log("this object is medium type");
                m_MyStatus.speed = 4;
                m_MyStatus.weight = 3;
                gameObject.tag = "MediumPlayer";
                break;
            case Type.Large:
                Debug.Log("this object is large type");
                m_MyStatus.speed = 3;
                m_MyStatus.weight = 5;
                gameObject.tag = "LargePlayer";
                break;
            default:
                Debug.Log("m_type is not set properly!!");
                break;
        }

        //今後はここにキャラクター名に応じてステータスを設定していくコードを書く。現時点で書いてあるのは見本
        switch(gameObject.name)
        {
            case "Koi":
                m_MyStatus.speed = 5;
                m_MyStatus.weight = 1;
                m_MyStatus.inkGuage = 1;
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
        Debug.Log("m_type = " + m_type);
        Initialize();

        gameobject = GameObject.Find("MovingWall");
        movingWallController = gameobject.GetComponent<MovingWallController>();
    }

    void FixedUpdate()
    {
        Move();
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
