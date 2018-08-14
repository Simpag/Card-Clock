using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Todo
 * //Remember amount of cards in stack
 * //Remember amount of cards in stack on screen
 * //Show amount of cards left in stack
 * //One more spawnpoint in the middle
 * //Return cards in stack on screen when the right number is placed
 * //Remember which number is supposed to be ontop of the stack on screen
 * //Fix endless loop when game is completed
 * Use ads to see the next card and possibly skip one position
 * Fix card positions
 * Throw cards in a nicer pile, like the uno game by unbisoft
 * Show deck graphics
 * Animate card draw
 * Animate when cards are retrived
 */

public class GameManager : MonoBehaviour {

    [Header ("Game Cheats")]
    [SerializeField]
    private bool instantWin; // $$$

    [Header ("Gameplay Variables")]
    [SerializeField]
    private Vector2[] spawnPosition;
    [SerializeField]
    private Vector2 deckPosition;
    [SerializeField]
    private GameObject cardDeck;
    [SerializeField]
    private Text deckSizeText;

    [SerializeField]
    private Button playShownCardButton;

    [SerializeField]
    private int maxCardPositions;
    private int cardPosition = 0;
    private int deckSize;
    private bool finishedGame;

    private List<GameObject>[] cardsInPosition;
    private bool[] isCardPositionOccupied;

    private void Awake()
    {
        deckSize = cardDeck.GetComponent<CardDeck>().DeckSize;
        isCardPositionOccupied = new bool[deckSize];
        finishedGame = false;

        cardsInPosition = new List<GameObject>[deckSize];
        for (int i = 0; i < cardsInPosition.Length; i++)
            cardsInPosition[i] = new List<GameObject>();
    }

    public void ShowNextCard()
    {
        int randomCard = Random.Range(0, deckSize);
        GameObject _cardSpawned = cardDeck.GetComponent<CardDeck>().SpawnCard(randomCard, deckPosition);
        deckSize--;
        deckSizeText.text = deckSize.ToString();

        playShownCardButton.gameObject.SetActive(true);
    }

    public void PlayShowedCard()
    {
        PlayCardOnNextPile(cardDeck.GetComponent<CardDeck>().DeckSize - 1);
        playShownCardButton.gameObject.SetActive(false);
    }

    public void PlayCard()
    {
        if (finishedGame)
            return;

        int randomCard = Random.Range(0, deckSize);
        if (instantWin)
            randomCard = 0;

        PlayCardOnNextPile(randomCard);
        deckSize--;
        deckSizeText.text = deckSize.ToString();
    }

    private void PlayCardOnNextPile(int _cardIndex)
    {
        if (deckSize <= 0)
        {
            Debug.Log("Deck's Empty!");
            return;
        }

        GameObject _cardSpawned = cardDeck.GetComponent<CardDeck>().SpawnCard(_cardIndex, spawnPosition[cardPosition]);
        
        cardsInPosition[cardPosition].Add(_cardSpawned);

        IsCardOnRightPile(_cardSpawned);

        if (cardPosition + 1 < maxCardPositions)
            cardPosition++;
        else
            cardPosition = 0;
        
        while(isCardPositionOccupied[cardPosition] && !finishedGame) 
        {
            Debug.Log("Skipped position: " + cardPosition.ToString() + " !");
            if (cardPosition + 1 < maxCardPositions)
                cardPosition++;
            else
                cardPosition = 0;
        }
        
    }

    private void IsCardOnRightPile(GameObject _card)
    {
        if (_card.GetComponent<Card>().CardNumber == cardPosition + 1) 
        {
            Debug.Log("Position " + (cardPosition+1).ToString() + " is now occupied!");

            RemoveCardsInPile(cardPosition);

            deckSizeText.text = deckSize.ToString();
            isCardPositionOccupied[cardPosition] = true;
        }

        IsGameOver();
    }

    private void RemoveCardsInPile(int _cardPosition) 
    {
        for (int i = 0; i < cardsInPosition[_cardPosition].Count - 1; i++)
        {
            cardsInPosition[_cardPosition][i].gameObject.SetActive(false);
            cardsInPosition[_cardPosition][i].transform.SetAsFirstSibling();
            deckSize++;
        }
        cardsInPosition[cardPosition].RemoveRange(0, cardsInPosition[cardPosition].Count - 1);
    }

    private void IsGameOver()
    {
        int _iCantMakeThisWorkTheWayIThinkItWouldWork = 0;
        for (int i = 0; i < maxCardPositions; i++)
        {
            if (isCardPositionOccupied[i])
                _iCantMakeThisWorkTheWayIThinkItWouldWork++;
        }

        if (_iCantMakeThisWorkTheWayIThinkItWouldWork == maxCardPositions)
        {
            finishedGame = true;
            Debug.Log("You finished the game!");   
        }
    }
}
