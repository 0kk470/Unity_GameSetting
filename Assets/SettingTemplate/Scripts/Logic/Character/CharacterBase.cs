using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour {

    CharacterController controller;
    [SerializeField]
    [Range(0,10)]
    private float speed;
	// Use this for initialization
	void Awake () {
        controller = GetComponent<CharacterController>();
	}
	
	public void Move(Vector3 direction)
    {
        transform.LookAt(transform.position + direction);
        controller.SimpleMove(direction * speed);
    }
}
