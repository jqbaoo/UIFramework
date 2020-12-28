using UnityEngine;
using System.Collections;
using GameCore;

public class CameraMove : MonoBehaviour
{
    //相机移动速度
    public float speed = 5f;
    //相机移动速度
    private Vector3 velocity = Vector3.zero;
    //移动时间
    private float time;
    //相机向左移动限定范围
    public float left = -10;
    //相机向右移动限定范围
    public float right = 15;
    //相机向前移动限定范围
    public float forword = 56;
    //相机向后移动限定范围
    public float bardword = 40;
    //相机移动的标签
    public bool isMove = false;

    void Start()
    {
        time = 1 / speed;
        //添加监听（监听移动消息）
        EventDispatcher.AddListener<bool>(E_MessageType.CameraMove, EditorCameraMoveState);
    }
    void LateUpdate()
    {
        Move();
    }

    void Move()
    {
        //当我们按住鼠标左键的时候，去执行移动相机的逻辑
        if (Input.GetMouseButton(0) && isMove)
        {   
            //移动相机的逻辑
            //当鼠标往右移动的时候，相机需要往左移动（对x坐标做减值操作,-mouse_X）
            //当鼠标往左移动的时候，相机需要往右移动（对x坐标做增值操作,-mouse_X）
            //当鼠标往下移动的时候，相机需要向前移动（对z坐标做增值操作,-mouse_Y）
            //当鼠标往上移动的时候，相机需要向后移动（对z坐标做减值操作,-mouse_Y）
            float mouse_X = Input.GetAxis("Mouse X");
            float mouse_Y = Input.GetAxis("Mouse Y");

            mouse_X = Mathf.Clamp(mouse_X, -0.5f, 0.5f);
            mouse_Y = Mathf.Clamp(mouse_Y, -0.5f, 0.5f);

            time = 1 / speed;
            this.transform.position = Vector3.SmoothDamp(this.transform.position, this.transform.position + new Vector3(-mouse_X, 0, -mouse_Y), ref velocity, time);

            //判断相机是否超出限定范围，如果是则将相机移动到限定的位置
            if (this.transform.position.x < left)
            {
                this.transform.position = new Vector3(left, transform.position.y, transform.position.z);
            }
            else if (this.transform.position.x > right)
            {
                this.transform.position = new Vector3(right, transform.position.y, transform.position.z);
            }

            if (this.transform.position.z < bardword)
            {
                this.transform.position = new Vector3(transform.position.x, transform.position.y, bardword);
            }
            else if (this.transform.position.z > forword)
            {
                this.transform.position = new Vector3(transform.position.x, transform.position.y, forword);
            }
        }

    }

    void EditorCameraMoveState(bool isMove)
    {
        this.isMove = isMove;
    }

    void OnDisable()
    {
        EventDispatcher.RemoveListener<bool>(E_MessageType.CameraMove, EditorCameraMoveState);
    }
}
