using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class KdTreeFindEnemy : MonoBehaviour
{
    public EnemyManager enemyManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 按下空格键查询最近敌人
        {
            if (transform == null) // 松开空格键删除最近敌人
            {
                return;
            }

            GameObject nearestEnemy = enemyManager.FindNearestEnemy(transform.position);
            if (nearestEnemy != null)
            {

                nearestEnemy.transform.GetComponent<MeshRenderer>().material.color = Color.red;
                //enemyManager.RemoveEnemy(nearestEnemy);

                //GameObject.Destroy(nearestEnemy, 1f);

                Debug.Log($"最近敌人的位置{nearestEnemy.name}.....{nearestEnemy.transform.position}");
            }
        }




      
       






    }
}
