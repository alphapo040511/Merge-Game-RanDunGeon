using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalRecoveryCard : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float playerMaxHp;
        float recoveryRank = 0.05f;
        float finalHeal;
        int CardRank;

        if (GetComponent<CardState>().skill == true)
        {
            playerMaxHp = player.GetComponent<CharacterStatus>().MaxHp;
            CardRank = GetComponent<CardState>().cardRank;

            if (CardRank == 1) recoveryRank = 0.05f;
            if (CardRank == 2) recoveryRank = 0.09f;
            if (CardRank == 3) recoveryRank = 0.17f;

            finalHeal = playerMaxHp * recoveryRank + 5;

            player.GetComponent<CharacterStatus>().Hp += finalHeal;
            GetComponent<CardState>().skill = false;
            Destroy(gameObject);
        }
    }
}
