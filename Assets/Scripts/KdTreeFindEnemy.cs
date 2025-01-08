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
        if (Input.GetKeyDown(KeyCode.Space)) // ���¿ո����ѯ�������
        {
            if (transform == null) // �ɿ��ո��ɾ���������
            {
                return;
            }

            GameObject nearestEnemy = enemyManager.FindNearestEnemy(transform.position);
            if (nearestEnemy != null)
            {

                nearestEnemy.transform.GetComponent<MeshRenderer>().material.color = Color.red;
                //enemyManager.RemoveEnemy(nearestEnemy);

                //GameObject.Destroy(nearestEnemy, 1f);

                Debug.Log($"������˵�λ��{nearestEnemy.name}.....{nearestEnemy.transform.position}");
            }
        }




      
       






    }
}
