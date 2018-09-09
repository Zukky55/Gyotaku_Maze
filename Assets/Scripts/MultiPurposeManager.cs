using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPurposeManager : MonoBehaviour
{
    /// <summary>Small player camera</summary>
    [SerializeField] Camera m_sCamera;
    /// <summary>Medium player camera</summary>
    [SerializeField] Camera m_mCamera;
    /// <summary>Large player camera</summary>
    [SerializeField] Camera m_lCamera;
    /// <summary>SmallPlayer</summary>
    private GameObject m_sgo;
    /// <summary>MediumPlayer</summary>
    private GameObject m_mgo;
    /// <summary>LargePlayer</summary>
    private GameObject m_lgo;
    /// <summary>SmallP's PlayerController</summary>
    private PlayerController m_spc;
    /// <summary>MediumP's PlayerController</summary>
    private PlayerController m_mpc;
    /// <summary>LargeP's PlayerController</summary>
    private PlayerController m_lpc;


    /// <summary>LoopArray.cs</summary>
    private LoopArray m_la;

    /// <summary>呼び出される度にカメラを切り替える</summary>
    public void CameraChange()
    {
        if (m_sCamera.enabled == true)
        {
            m_sCamera.enabled = false;
            m_mCamera.enabled = true;
            m_lCamera.enabled = false;
        }
        else if (m_mCamera.enabled == true)
        {
            m_sCamera.enabled = false;
            m_mCamera.enabled = false;
            m_lCamera.enabled = true;
        }
        else if (m_lCamera.enabled == true)
        {
            m_sCamera.enabled = true;
            m_mCamera.enabled = false;
            m_lCamera.enabled = false;
        }
    }

    public void ChangeOperatingPlayer()
    {
        if (m_spc.m_moveFrag == true)
        {
            m_spc.m_moveFrag = false;
            m_mpc.m_moveFrag = true;
            m_lpc.m_moveFrag = false;
        }
        else if (m_mpc.m_moveFrag == true)
        {
            m_spc.m_moveFrag = false;
            m_mpc.m_moveFrag = false;
            m_lpc.m_moveFrag = true;
        }
        else if (m_lpc.m_moveFrag == true)
        {
            m_spc.m_moveFrag = true;
            m_mpc.m_moveFrag = false;
            m_lpc.m_moveFrag = false;
        }

    }

    public void Initialize()
    {
        //各Playerコンポーネント取得
        m_sgo = GameObject.Find("SmallPlayer");
        m_mgo = GameObject.Find("MediumPlayer");
        m_lgo = GameObject.Find("LargePlayer");
        m_spc = m_sgo.GetComponent<PlayerController>();
        m_mpc = m_mgo.GetComponent<PlayerController>();
        m_lpc = m_lgo.GetComponent<PlayerController>();

        //ゲーム開始時最初に適応させるカメラの初期設定
        m_sCamera.enabled = true;
        m_mCamera.enabled = false;
        m_lCamera.enabled = false;

        //ゲーム開始時最初に操作するプレイヤーフラグの初期設定
        m_spc.m_moveFrag = true;
        m_mpc.m_moveFrag = false;
        m_lpc.m_moveFrag = false;
    }

    public void Start()
    {
        Initialize();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            CameraChange();
            ChangeOperatingPlayer();
        }
    }
}
