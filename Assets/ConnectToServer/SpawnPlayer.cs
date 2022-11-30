using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 startPosition;
    void Start()
    {
        PhotonNetwork.Instantiate(player.name, startPosition, Quaternion.identity);
    }
}
