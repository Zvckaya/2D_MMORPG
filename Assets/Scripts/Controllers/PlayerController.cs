using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Define;

public class PlayerController : MonoBehaviour
{
    public Grid _grid;
    float _speed = 5.0f;

    Vector3Int _cellPos = Vector3Int.zero;
    MoveDir _dir = MoveDir.None;
    bool _isMoving = false;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = _grid.CellToWorld(_cellPos)+ new Vector3(0.5f,0.5f);
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        GetDirInput();
        UpdatePosition();
        UpdateIsMoving();
        

    }

    //이동가능한 상태일때 실제로 이동 
     void UpdatePosition()
    {
        if (_isMoving == false)
            return;

        Vector3 destPos = _grid.CellToWorld(_cellPos) + new Vector3(0.5f, 0.5f); //서버위치
        Vector3 moveDir = destPos - transform.position; 

        float dist = moveDir.magnitude; //방향벡터의 크기, 즉 얼마나 남았는가?
        if(dist< _speed * Time.deltaTime)  //사실상 도착했는가? 
        {
            transform.position = destPos;
            _isMoving = false;
        }
        else
        {
            transform.position += moveDir.normalized * _speed * Time.deltaTime; //너무 스피드가 빠르면 그 이상 갈 수도 있음 
            _isMoving = true;
        }
         
    }


    //실질적 이동 
    void UpdateIsMoving()
    {
        if (_isMoving == false)
        {
            switch (_dir)
            {
                case MoveDir.Up:
                    _cellPos += Vector3Int.up;
                    _isMoving = true;
                    break;
                case MoveDir.Down:
                    _cellPos += Vector3Int.down;
                    _isMoving = true;
                    break;
                case MoveDir.Left:
                    _cellPos += Vector3Int.left;
                    _isMoving = true;
                    break;
                case MoveDir.Right:
                    _cellPos += Vector3Int.right;
                    _isMoving = true;
                    break;
            }
        }
    }

    //키보드 입력을 받아 방향 설정 
    void GetDirInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //transform.position += Vector3.up * Time.deltaTime * _speed;
            _dir = MoveDir.Up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //transform.position += Vector3.down * Time.deltaTime * _speed;
            _dir = MoveDir.Down;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //transform.position += Vector3.left * Time.deltaTime * _speed;
            _dir = MoveDir.Left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //transform.position += Vector3.right * Time.deltaTime * _speed;
            _dir = MoveDir.Right;
        }
        else
        {
            _dir = MoveDir.None;
        }
    }
}
