using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour {

	public float speed;
	public float length;
	public Transform playerTransform;
	public SpriteRenderer spriteObject;
	public Vector3 vectorPosition;

	private float startPos;


	void Start () 
	{
		startPos = this.transform.position.x;
		length = spriteObject.bounds.size.x;
		vectorPosition = this.transform.position;
	}
	
	void Update () 
	{
		float dist = (playerTransform.position.x * speed);
		vectorPosition.x = vectorPosition.x = startPos + dist;
		transform.position = vectorPosition;


	}
}
