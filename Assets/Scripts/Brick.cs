using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField]
    private float _delta = 0.8f;
    private float _previousTime = 0;

    private static int _with = 10;
    private static int _height = 15;
    private static Transform[,] _grids = new Transform[_with, _height];

    [SerializeField]
    private Transform _rotationPont;

    private Vector3 _oldPosition;
    private MenuManager _menu;
    private void Awake()
    {
        _oldPosition = transform.position;
        _menu = FindObjectOfType<MenuManager>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            CheckMove();
            return;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            CheckMove();
            return;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(_rotationPont.position, transform.forward, 90f);
            CheckMove();
        }
        if(Time.time - _previousTime > (Input.GetKey(KeyCode.DownArrow) ? _delta / 10 : _delta))
        {
            transform.position += new Vector3(0, -1, 0);
            CheckMove();
            CheckForLines();
            _previousTime = Time.time;
        }
        
        _oldPosition = transform.position;
    }
    
    private void CheckMove()
    {
        foreach(Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);


            if (roundX < 0)
            {
                transform.position = _oldPosition;
                return;
            }
            else if(roundX >= _with)
            {
                transform.position = _oldPosition;
                return;
            }
            else if(roundY < 0)
            {
                transform.position = _oldPosition;
                NewBuildBrick();
                
                AddGrids();
                return;
            }
            


            if(_grids[roundX, roundY] != null)
            {
                transform.position = _oldPosition;
                AddGrids();
                NewBuildBrick();
                return;
            }
        }
    }
    private void NewBuildBrick()
    {
        this.enabled = false;
        if(_menu.IsPlaying)
            FindObjectOfType<SpawnBricks>().CreateBrick();
    }
    private void AddGrids()
    {
        foreach (Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);
            print(roundX + " - " + roundY);

            if(roundY == _height - 1)
                _menu.GameOver();

            _grids[roundX, roundY] = children;// add grid to playzone
        }
    }
    private void CheckForLines()
    {
        for(int i = 0; i < _height; i++)
        {
            if(HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }
    private bool HasLine(int i)
    {
        for(int j = 0; j < _with; j++)
        {
            if (_grids[j, i] == null)
                return false;
        }
        return true;
    }
    private void DeleteLine(int i)
    {
        for(int j = 0; j < _with; j++)
        {
            Destroy(_grids[j, i].gameObject);
            _grids[j, i] = null;
        }
    }
    private void RowDown(int i)
    {
        for (int k = i; k < _height; k++)
        {
            for (int j = 0; j < _with; j++)
            {
                if (_grids[j, k] != null)
                {
                    _grids[j, k - 1] = _grids[j, k];
                    _grids[j, k] = null;
                    _grids[j, k - 1].transform.position += new Vector3(0, -1, 0);
                    
                }
            }
        }
    }
}
