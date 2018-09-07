using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalController : MonoBehaviour
{
    /// <summary>メッセージ表示用 Text</summary>
    [SerializeField] Text m_goalText;
    /// <summary>ゴールにいるかどうか判断する配列</summary>
    private List<GameObject> m_goalList = new List<GameObject>();

	// Use this for initialization
	void Start ()
    {
        m_goalText.text = "";   /// メッセージを消す
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
            m_goalList.Add(collision.gameObject);  //ゴール判定があったら、配列に追加する
    }

    private void FixedUpdate()
    {
        if (m_goalList.Count == 3)  //配列の要素数が 3 になったら(プレイヤーが3匹ともゴール内に入ったら)
        {
            m_goalText.text = "Clear";  //メッセージを表示する
        }
        m_goalList.Clear();  //要素数が 3 以外なら毎回配列内をクリアする
    }
}
