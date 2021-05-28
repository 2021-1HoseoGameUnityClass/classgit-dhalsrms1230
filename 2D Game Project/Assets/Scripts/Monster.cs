using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private GameObject rayPos;
    
    [SerializeField]
    private float moveSpeed = 3f;
    
    [SerializeField]
    private int HP;

    private bool moveRight;
    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckRay();
    }

    private void CheckRay()
    {
        // 죽지 않았다면 체크.
       if(isDead == false)
        {
            // 레이어 마스크
            LayerMask layerMask = new LayerMask();
            layerMask = LayerMask.GetMask("Platform"); // 해당하는 layerMAsk를 가져옴.

            // 레이 케스트
            RaycastHit2D ray = Physics2D.Raycast(rayPos.transform.position, new Vector2(0, -1), 1.1f, layerMask.value);

            Debug.DrawRay(rayPos.transform.transform.position, new Vector3(0, -1, 0), Color.red);


            // Hit가 되지 않으면
            if(ray == false)
            {
                Debug.Log("반대 방향으로");
                if(moveRight)
                {
                    moveRight = false;
                }
                else
                {
                    moveRight = true;
                }

                //moveRight = !moveRight; -> 반대로 바꿔주는 인벌스
            }
            Move();
        }
    }

    private void Move()
    {
        float direction = 0;
        if(moveRight)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        // 플립(좌우반전)
        Vector3 vector3 = new Vector3(direction, 1, 1);
        transform.localScale = vector3;

        // 이동
        float speed = moveSpeed * Time.deltaTime * direction;
        vector3 = new Vector3(speed, 0, 0);
        transform.Translate(vector3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       bool check = collision.CompareTag("Bullet");
       if(check)
        {
            HP -= 1;
            if(HP < 1)
            {
                GetComponent<Animator>().SetBool("Death", true);
                isDead = true;
                Destroy(this.gameObject, 1);
            }
        }
    }
}
