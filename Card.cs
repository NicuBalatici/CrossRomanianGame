namespace Nicolae_Balatici_Cross;

public class Card
{
    private String _colour;
    private int _number;

    public Card(){}
    public Card(String colour, int number)
    {
        this._colour = colour;
        this._number = number;
    }

    public static List<String> AddColour()
    {
        return new List<String>{"Red", "Duba", "Green", "Acorn"};
    }

    public static List<int> AddNumber()
    {
        return new List<int>{2,3,4,9,10,11};
    }

    public override String ToString()
    {
        return this._colour+"-"+this._number;
    }

    public String GetColour()
    {
        return this._colour;
    }

    public int GetNumber()
    {
        return this._number;
    }
}