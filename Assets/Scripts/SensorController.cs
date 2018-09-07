using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorController : MonoBehaviour
{
    private int getWeight = 0;
    /// <summary>プレイヤーの重さを記憶する配列</summary>
    private List<int> weightList = new List<int>();
    /// <summary>PlayerController のスクリプトを得るための変数</summary>
    GameObject gameobject;
    MovingWallController movingWallController;

    // Use this for initialization
    void Start ()
    {
        gameobject = GameObject.Find("MovingWall");
        movingWallController = gameobject.GetComponent<MovingWallController>();
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //weightList.Add(collision.gameObject.GetComponent<PlayerController>().m_playerWeight);

        foreach (int weight in weightList)
        {
            getWeight += weight;

            Debug.Log("weight = " + weight);
            Debug.Log("getWeight = " + getWeight);
        }
        if (getWeight >= 4)
        {
            movingWallController.MoveUp();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        weightList.Clear();
        getWeight = 0;

        Debug.Log("weightList と getWeight をクリアしました。");

        movingWallController.MoveDown();
    }
}