using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopArray : MonoBehaviour
{
    /// <summary>配列サイズ</summary>
    private int m_size;
    /// <summary>配列本体</summary>
    public int[] m_list;

    /// <summary>Constructorの代わり</summary>
    /// <param name="size">配列の要素数</param>
    public void Initialize(int size)
    {
        m_size = size;
        m_list = new int[size];
    }

    /// <summary>インデクサー</summary>
    /// <param name="index">要素指定値</param>
    /// <returns>要素数を指定値が超えていたらその余剰分を計算し指定値を返す</returns>
    public int this[int index]
    {
        set
        {
            m_list[GetIndex(index)] = value;
        }

        get
        {
            return m_list[GetIndex(index)];
        }
    }

    private int GetIndex(int index)
    {
        //0未満の値は強制的に0で再設定
        if (index < 0)
        {
            return 0;
        }
        //元のインデックス値と配列サイズの余剰でインデックス地を再設定
        //(配列サイズが3で指定地は4ならば、新しいインデックス値は1)
        return index % m_size;
    }
}
