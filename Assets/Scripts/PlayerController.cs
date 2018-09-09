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
    public Rigidbody2D m_rb2d;
    /// <summary>操作対象切り替え</summary>
    public bool m_moveFrag { get; set; }
    /// <summary>Acquire spawn coedinates</summary>
    public Vector2 m_initPos { get; set; }

    //debug
    GameObject gameobject;
    MovingWallController movingWallController;

    //Temp
    internal BoxCollider2D m_cbc2d;


    /// <summary>Mover</summary>
    void Move()
    {
        //x,y軸を入力しそのベクトルを取得,速度に代入
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 direction = new Vector2(x, y);
        m_rb2d.velocity = direction * m_myStatus.speed;
        //GetAxisの入力値が一定以上ある場合のみ角度変換する事で勝手に上に向いてしまわない様にする
        if (x > 0.5f || x < -0.5f || y > 0.5f || y < -0.5f)
        {
            transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
        }

    }

    /// <summary>Change for each type</summary>
    void Initialize()
    {
        //RigidBoy2Dを取得
        m_rb2d = GetComponent<Rigidbody2D>();
        //angularDragを設定可能最大値にして、壁等との接触による回転をほぼ無くす
        m_rb2d.angularDrag = 1000000;
        //ステージ開始初期位置を保存
        m_initPos = this.transform.position;


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
            case "Carp":
                m_SetMyStatus.speed = 5;
                m_SetMyStatus.weight = 1;
                m_SetMyStatus.inkGuage = 1;
                m_ability = Ability.Through;
                break;
            case "SmokedShark":
                m_SetMyStatus.speed = 5;
                m_SetMyStatus.weight = 1;
                m_SetMyStatus.inkGuage = 1;
                m_ability = Ability.Destroy;
                break;
            default:
                break;
        }

        switch(m_ability)
        {
            case Ability.Through:
                break;
            default:
                break;
        }


    }

    void Start()
    {
        Initialize();

        //debug
        gameobject = GameObject.Find("MovingWall");
        movingWallController = gameobject.GetComponent<MovingWallController>();


        //Temp
        m_cbc2d = GetComponentInChildren<BoxCollider2D>();

    }

    void Update()
    {
        if(m_moveFrag)
        {
            Move();

            //debug
            if (Input.GetMouseButtonDown(0))
            {
                movingWallController.MoveUp();
            }
            if (Input.GetMouseButtonDown(1))
            {
                movingWallController.MoveDown();
            }
        }
        else
        {
            m_rb2d.velocity = Vector2.zero;
        }

        //Temp
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
