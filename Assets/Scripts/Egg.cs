using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField] private GameObject player;
    void HideEggShowPlayer()
    {
        AudioManager.instance.PlayCrackEgg();
        gameObject.SetActive(false);
        player.SetActive(true);
    }



}
