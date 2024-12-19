namespace Nicolae_Balatici_Cross;

public class Bunch
{
    private List<Card> _bunch;

    public Bunch(int countNumber, int countColour)
    {
        this._bunch = new List<Card>(countNumber * countColour);
    }
    
    public List<Card> AddCards(List<int> numbers, List<String> colours)
    {
        for (int i = 0; i < numbers.Count; i++)
        {
            for (int j = 0; j < colours.Count; j++)
            {
                this._bunch.Add(new Card(colours[j], numbers[i]));
            }
        }
        return this._bunch;
    }

    public Card RemoveCards()
    {
        Random random = new Random();
        Card removedItem = default(Card);
        
        if (this._bunch.Count > 0)
        {
            int index = random.Next(this._bunch.Count);
            removedItem = this._bunch[index];
            this._bunch.RemoveAt(index);
        }

        if (removedItem == null)
        {
            throw new IndexOutOfRangeException();
        }
        
        return removedItem;
    }
    
    public int GetBunchCount() => _bunch.Count;

    public void PrintBunch()
    {
        for (int i = 0; i < _bunch.Count; i++)
        {
            Console.WriteLine(_bunch[i]);
        }
    }
}