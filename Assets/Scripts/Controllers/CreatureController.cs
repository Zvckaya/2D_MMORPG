using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CreatureController : MonoBehaviour
{
    float _speed = 5.0f;

    protected Vector3Int _cellPos = Vector3Int.zero;
    protected bool _isMoving = false;
    protected Animator _animator;

    MoveDir _dir = MoveDir.Down;
    public MoveDir Dir
    {
        get { return _dir; }
        set
        {
            if (_dir == value)
                return;

            switch (value)
            {
                case MoveDir.Up:
                    _animator.Play("WALK_DOWN");
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    break;
                case MoveDir.Down:
                    _animator.Play("WALK_UP");
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    break;
                case MoveDir.Left:
                    _animator.Play("WALK_RIGHT");
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    break;
                case MoveDir.Right:
                    _animator.Play("WALK_RIGHT");
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    break;
                case MoveDir.None:
                    if (_dir == MoveDir.Up)
                    {
                        _animator.Play("IDLE_DOWN");
                        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    }
                    else if (_dir == MoveDir.Down)
                    {
                        _animator.Play("IDLE_UP");
                        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    }
                    else if (_dir == MoveDir.Left)
                    {
                        _animator.Play("IDLE_RIGHT");
                        transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    }
                    else
                    {
                        _animator.Play("IDLE_RIGHT");
                        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    }
                    break;
            }
            _dir = value;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateController();

    }

    protected virtual void Init()
    {
        _animator = GetComponent<Animator>();
        Vector3 pos = Managers.Map.CurrentGrid.CellToWorld(_cellPos) + new Vector3(0.5f, 0.5f);
        transform.position = pos;
    }

    protected virtual void UpdateController()
    {
        UpdatePosition();
        UpdateIsMoving();
    }

    

    //�̵������� �����϶� ������ �̵� 
    void UpdatePosition()
    {
        if (_isMoving == false)
            return;

        Vector3 destPos = Managers.Map.CurrentGrid.CellToWorld(_cellPos) + new Vector3(0.5f, 0.5f); //������ġ
        Vector3 moveDir = destPos - transform.position;

        float dist = moveDir.magnitude; //���⺤���� ũ��, �� �󸶳� ���Ҵ°�?
        if (dist < _speed * Time.deltaTime)  //��ǻ� �����ߴ°�? 
        {
            transform.position = destPos;
            _isMoving = false;
        }
        else
        {
            transform.position += moveDir.normalized * _speed * Time.deltaTime; //�ʹ� ���ǵ尡 ������ �� �̻� �� ���� ���� 
            _isMoving = true;
        }

    }


    //������ �̵� 
    void UpdateIsMoving()
    {
        if (_isMoving == false && _dir != MoveDir.None)
        {
            Vector3Int destPos = _cellPos;
            switch (_dir)
            {
                case MoveDir.Up:
                    destPos += Vector3Int.up;
                    break;
                case MoveDir.Down:
                    destPos += Vector3Int.down;
                    break;
                case MoveDir.Left:
                    destPos += Vector3Int.left;
                    break;
                case MoveDir.Right:
                    destPos += Vector3Int.right;
                    break;
            }

            if (Managers.Map.CanGo(destPos))
            {
                _cellPos = destPos;
                Debug.Log($"player move{destPos.ToString()}");
                _isMoving = true;
            }
        }
    }

    //Ű���� �Է��� �޾� ���� ���� 
  }
