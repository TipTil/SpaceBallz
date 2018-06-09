using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] compToDisable;

    Camera lobbyCamera;

    void Start()
    {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < compToDisable.Length; i++)
            {
                compToDisable[i].enabled = false;
            }
        }
        else
        {
            lobbyCamera = Camera.main;
            if(lobbyCamera != null)
            {
                lobbyCamera.gameObject.SetActive(false);
            }
        }
    }

    void OnDisable()
    {
        if (lobbyCamera != null)
        {
            lobbyCamera.gameObject.SetActive(true);
        }
    }


}
