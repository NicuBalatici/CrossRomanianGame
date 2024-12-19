namespace Nicolae_Balatici_Cross;

public class Player
{
    private List<Card> _cards = new List<Card>(6);
    private int _points;
    public void AddCard(Card card)
    {
        _cards.Add(card);
    }

    public Player AddDeck(Bunch bunch, int playerNumber, int colourNumber, int numberNumbers)
    {
        for (int j = 0; j < (colourNumber * numberNumbers)/playerNumber; j++)
        { 
            Card c = bunch.RemoveCards();
            this.AddCard(c);
        }
        return this;
    }
  
    public int AddPoints(int points)
    {
        _points += points;
        return _points;
    }
    public void PrintCards()
    {
        foreach (Card card in _cards)
        {
            Console.WriteLine(card);
        }
        Console.WriteLine();
    }

    public Card RemoveCardFromCertainPosition(int position)
    {
        Card c = _cards[position];
        _cards.RemoveAt(position);
        return c;
    }

    public List<Card> GetCards()
    {
        return _cards;
    }

    public int GetPoints()
    {
        return _points;
    }
}