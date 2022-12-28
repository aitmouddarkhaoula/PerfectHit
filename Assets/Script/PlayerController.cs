using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour {
    [SerializeField] float mSpeed = 25;
    [SerializeField] float bodySpeed = 10;
    [SerializeField] int gap = 2;
    [SerializeField] int n = 4;
    public int force;
    public TextMesh count;
    float touchRightVal;
    //[SerializeField] Obstacles _obstacle;
    [SerializeField] Ball _ball;
    public GameObject BodyPrefab;
    Vector2 startPosition;
    Vector2 newPosition;
    public Floor floor;

    public List<GameObject> _body = new List<GameObject>();
    private List<Vector3> _positionsHistory = new List<Vector3>();

    //animation
   

    void Start() {
        Snake();


    }
	

   
    void FixedUpdate() {
        if(!GameManager.instance.play) {
            return;
        }
        float n = floor.floorLenght;
        float k = transform.position.z;
        GameManager.instance.SetProgress(k / n);
        count.text = _body.Count + "";
        transform.position += transform.forward * mSpeed * Time.fixedDeltaTime;
        
        if (transform.position.z >= 400)
        {
            print("win");
            GameManager.instance.ShowWinUI();
            Time.timeScale = 0;
            Application.Quit();

        }

        bool left = false;
        bool right = false;
        /*if (Input.GetMouseButton(0))
        {
            Vector3 currentMousee = new Vector3(Input.mousePosition.x * 3.5f / Screen.width, transform.position.y, transform.position.z);
            transform.position = currentMousee;
        }

        rd.velocity = new Vector3(0, 0, i);
        if (Time.timeScale == 1)
            i += 0.04f;*/

        if (Input.GetMouseButtonDown(0))
        {
            startPosition.x = Input.mousePosition.x ;
        }
        if (Input.GetMouseButton(0))
        {
            float newX = Input.mousePosition.x;
            if (newX != startPosition.x)
                newPosition.x = newX;
            if (newPosition.x> startPosition.x)
                right = true;
            else if(newPosition.x < startPosition.x)
                left = true;

            startPosition = newPosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            startPosition = Vector2.zero;
        }


        if (left || Input.GetKey(KeyCode.A))
            {
            transform.position -= transform.right * Time.fixedDeltaTime * force;
            }
        if (right || Input.GetKey(KeyCode.D))
            {
                transform.position += transform.right * Time.fixedDeltaTime * force;
            }
        if (transform.position.x > floor.floorWidth)
        {
            transform.position = new Vector3(floor.floorWidth, transform.position.y, transform.position.z);
        }
        if(transform.position.x < -floor.floorWidth)
        {
            transform.position = new Vector3(-floor.floorWidth, transform.position.y, transform.position.z);
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchRightVal = touch.deltaPosition.x;
            transform.position += transform.right * touchRightVal * Time.deltaTime;
        }

        // Store position history
        _positionsHistory.Insert(0, transform.position);

        // Move body parts
        int index = 0;
        foreach (var body in _body) {
            Vector3 point = _positionsHistory[Mathf.Clamp(index * gap, 0, _positionsHistory.Count - 1)];

            // Move body towards the point along the snakes path
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * bodySpeed * Time.deltaTime;

            // Rotate body towards the point along the snakes path
            body.transform.LookAt(point);

            index++;
        }
        if (_body.Count == 0 && transform.position.z >= 40)
        {
            GameManager.instance.play = false;
            GameManager.instance.ShowLoseUI();
            Time.timeScale = 0;
            Application.Quit();

        }
        if(transform.position.y < -1)
        {
            GameManager.instance.ShowLoseUI();
            Time.timeScale = 0;
            Application.Quit();
        }
    }
    public void RemoveBall(Ball ball)
	{
        Destroy(ball.gameObject);
        _body.Remove(ball.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.HideGrowUI();
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
            Ball ball = Instantiate(this._ball);
            ball.transform.position = transform.position - Vector3.forward * _body.Count;
            ball.Grow(this);
            GameManager.instance.ShowGrowUI();
        }
        if (other.gameObject.name == "End")
        {
            print("win");
            GameManager.instance.ShowWinUI();
            Time.timeScale = 0;
            Application.Quit();

        }
    }
    public void Snake()
    {
        for (int i = 0; i < n; i++){
            Ball ball = Instantiate(this._ball);
            ball.Grow(this);
        }
        

    }
}