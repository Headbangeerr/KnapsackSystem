using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPanelUI: MonoBehaviour {

    public Transform[] Grids;
    /// <summary>
    /// 返回一个空格子
    /// </summary>
    /// <returns></returns>
    public Transform GetEmptyGrid()
    {
        for (int i = 0; i < Grids.Length; i++)
        {
            if (Grids[i].childCount == 0)//如果没有子物体则返回该物体
                return Grids[i];
        }
        return null;//否则返回空
    }
}
 