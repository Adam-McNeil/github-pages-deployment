using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    PhotonView view;
    private float speed = 2;
    [SerializeField] private float rightBound;
    [SerializeField] private float leftBound;
    [SerializeField] private float topBound;
    [SerializeField] private float botttomBound;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }
    void Update()
    {
        if (view.IsMine)
        {
            float xInput = Input.GetAxis("Horizontal");
            float yInput = Input.GetAxis("Vertical");
            this.transform.position = new Vector3(this.transform.position.x + (xInput * Time.deltaTime * speed), this.transform.position.y, this.transform.position.z + (yInput * Time.deltaTime * speed));
            if (this.transform.position.x > rightBound)
            {
                this.transform.position = new Vector3(leftBound, this.transform.position.y, this.transform.position.z);
            }
            else if (this.transform.position.x < leftBound)
            {
                this.transform.position = new Vector3(rightBound, this.transform.position.y, this.transform.position.z);
            }
            else if (this.transform.position.z > topBound)
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, botttomBound);
            }
            else if (this.transform.position.z < botttomBound)
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, topBound);
            }
        }
    }
}
