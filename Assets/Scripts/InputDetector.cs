
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDetector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(2))//捕获鼠标事件，0：鼠标左键，1：鼠标右键，2：鼠标中键
        {
            int index = Random.Range(0, 10);//使用随机数生成id为0~9的物品
            KnapsackManager.Instance.StoreItem(index);
        }
	}
}
