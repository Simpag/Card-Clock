using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Todo
 * Remember amount of cards in stack
 * Remember amount of cards in stack on screen
 * Show amount of cards left in stack
 * One more spawnpoint in the middle
 * Return cards in stack on screen when the right number is placed
 * Remember which number is supposed to be ontop of the stack on screen
 * 
 */

public class GameManager : MonoBehaviour {

    [SerializeField]
    private Vector2[] spawnPosition;
    [SerializeField]
    private GameObject cardDeck;

    [SerializeField]
    private int maxCardPositions;
    private int cardPosition = 0;
    private int deckSize;

    private void Awake()
    {
        deckSize = cardDeck.GetComponent<CardDeck>().DeckSize;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnCard();
        }
    }

    private void SpawnCard()
    {
        int randomCard = Random.Range(0, deckSize);
        cardDeck.GetComponent<CardDeck>().SpawnCard(randomCard, spawnPosition[cardPosition]);
        deckSize--;

        if (cardPosition + 1 < maxCardPositions)
            cardPosition++;
        else
            cardPosition = 0;
    }
}
