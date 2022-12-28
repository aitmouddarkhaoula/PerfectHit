using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	[SerializeField] int _number;
	[SerializeField] TextMesh _textMesh;
	[SerializeField] int direction = 3;
	//[SerializeField] PlayerController floor;
	private Vector3 movement;

	public int number => _number;


	public void SetNumber(int num)
	{
		_number = num;
		_textMesh.text = num + "";
	}
    private void Update()
    {
		movement = new Vector3(direction, 0f, 0f);
		transform.position = transform.position + movement * Time.deltaTime;
		if (transform.position.x >= GameManager.instance.floorWidth)
		{
			direction = direction * -1;
		}
		if (transform.position.x <= -GameManager.instance.floorWidth)
		{
			direction = direction * -1;
		}
    }

}
