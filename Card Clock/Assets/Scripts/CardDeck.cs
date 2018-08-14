using UnityEngine;

[System.Serializable]
public class CardDeck : MonoBehaviour {

    [SerializeField]
    private const int DECKSIZE = 52;

    private int orderInLayer;
    public int activeChildren;

    public int DeckSize { get { return DECKSIZE; } }


    public GameObject SpawnCard(int _cardIndex, Vector2 _spawnPosition)
    {
        Debug.Log("Spawned " + transform.GetChild(_cardIndex).GetComponent<Card>().CardNumber.ToString() + " of " + transform.GetChild(_cardIndex).GetComponent<Card>().CardType.ToString());

        GameObject rGo = transform.GetChild(_cardIndex).gameObject;

        transform.GetChild(_cardIndex).gameObject.SetActive(true);
        transform.GetChild(_cardIndex).transform.position = _spawnPosition;
        transform.GetChild(_cardIndex).gameObject.GetComponentInChildren<SpriteRenderer>().sprite = transform.GetChild(_cardIndex).GetComponent<Card>().CardSprite;
        transform.GetChild(_cardIndex).gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = orderInLayer++;
        transform.GetChild(_cardIndex).SetAsLastSibling();
        activeChildren++;

        return rGo;
    }
}

