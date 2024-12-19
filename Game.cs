namespace Nicolae_Balatici_Cross;
public class Game
{
    private List<Player> _players;
    private string _tromf;
    private int _starterOfRound;
    public Game(List<Player> players)
    { 
        _players = players;
        _tromf = "";
        _starterOfRound = 0;
    }

    public void ChoosingTromf(List<int> points)
    {
        int maxPoint = points.Max();
        int i = points.IndexOf(maxPoint);
        Console.WriteLine($"\nPlayer {i + 1} will choose the tromf.");
        _starterOfRound = i;

        Console.Write("Input the tromf you would like to choose (Red, Green, Acorn, Duba): ");
    
        while (true)
        {
            string? inputTromf = Console.ReadLine();
            if (string.IsNullOrEmpty(inputTromf))
            {
                throw new Exception("Invalid input. Tromf cannot be null or empty. Try again.");
            }

            inputTromf = inputTromf.Trim();
            if (inputTromf.Equals("Red", StringComparison.OrdinalIgnoreCase) ||
                inputTromf.Equals("Green", StringComparison.OrdinalIgnoreCase) ||
                inputTromf.Equals("Acorn", StringComparison.OrdinalIgnoreCase) ||
                inputTromf.Equals("Duba", StringComparison.OrdinalIgnoreCase))
            {
                _tromf = inputTromf;
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. The tromf must be Red, Green, Acorn, or Duba. Try again.");
            }
        }

        Console.WriteLine($"\nThe tromf is {_tromf}!");
    }
    private void PrintCardsOfEachPlayer()
    {
        Console.WriteLine("The cards are:\n");
        for (int z = 0; z < 4; z++)
        {
            Console.WriteLine($"Player #{z + 1}");
            _players[z].PrintCards();
        }
    }
    private void ChoosingPoints(List<int> points)
    {
        List<int> aux = new List<int>(4);
        aux.Add(-1);
        aux.Add(-1);
        aux.Add(-1);
        aux.Add(-1);
        
        int i = 0;
        bool taken;
        while(i < 4)
        {
            Console.Write("Player " + (i + 1) + " choose the n.o. points: ");
            if (!int.TryParse(Console.ReadLine(), out int input) || input < 0 || input > 5)
            {
                throw new Exception("Invalid input. You must enter a number between 0 and 5.");
            }

            taken = false;
            
            for (int j = 0; j < aux.Count; j++)
            {
                if (input == 0) { break; }
                if (input == aux[j])
                {
                    Console.WriteLine("The point is taken, try again!\n");
                    taken = true;
                    break;
                }
            }

            if (taken == false)
            {
                aux[i] = input;
                points.Add(input);
                i++;
            }
        }
        
        aux.Clear();
    }
    
    private void CountdownUntilStart()
    {
        Console.WriteLine("\nThe match will start in a few seconds....");
        for (int j = 3; j >= 0; j--)
        {
            Thread.Sleep(1000);
            Console.WriteLine($"{j}...");
        }
        Console.WriteLine("\nSTART");
    }

    private void Announcement(Player player)
    {
        bool hasThree = false;
        bool hasFour = false;
        int pozThree = 0;
        int pozFour = 0;
        bool hasTromf = false;

        for (int i = 0; i < player.GetCards().Count; i++)
        {
            if (player.GetCards()[i].GetNumber() == 3)
            {
                hasThree = true;
                pozThree = i;
            }
            else if (player.GetCards()[i].GetNumber() == 4)
            {
                hasFour = true;
                pozFour = i;
            }
        }

        if (hasThree && hasFour && 
            player.GetCards()[pozThree].GetColour().Equals(player.GetCards()[pozFour].GetColour()))
        {
            hasTromf = _tromf.Equals(player.GetCards()[pozThree].GetColour());
        }

        if (hasThree && hasFour)
        {
            string card1 = $"{player.GetCards()[pozThree].GetColour()}-{player.GetCards()[pozThree].GetNumber()}";
            string card2 = $"{player.GetCards()[pozFour].GetColour()}-{player.GetCards()[pozFour].GetNumber()}";

            if (hasTromf)
            {
                Console.WriteLine($"{card1} și {card2} - 40 de puncte");
                player.AddPoints(40);
            }
            else if(player.GetCards()[pozThree].GetColour().Equals(player.GetCards()[pozFour].GetColour()))
            {
                Console.WriteLine($"{card1} și {card2} - 20 de puncte");
                player.AddPoints(20);
            }
        }
    }

    private Card DecidingTheWinnerCard(List<Card> auxCards, Player previousRoundWinner)
    {
        List<Card> tromfCards = auxCards.Where(card => card.GetColour().Equals(_tromf)).ToList();

        if (tromfCards.Count == 0)
        {
            string newTromf = previousRoundWinner.GetCards()[0].GetColour();
            Console.WriteLine($"No tromf left in the cards. The new tromf is decided as {newTromf} by the previous round's winner!");
            _tromf = newTromf;

            tromfCards = auxCards.Where(card => card.GetColour().Equals(_tromf)).ToList();
        }

        List<Card> cardsToConsider = tromfCards.Count > 0 ? tromfCards : auxCards;

        Card winnerCard = cardsToConsider[0];
        int maxNumber = winnerCard.GetNumber();

        for (int i = 1; i < cardsToConsider.Count; i++)
        {
            if (cardsToConsider[i].GetNumber() > maxNumber)
            {
                winnerCard = cardsToConsider[i];
                maxNumber = cardsToConsider[i].GetNumber();
            }
        }

        int originalIndex = auxCards.IndexOf(winnerCard);
        Console.WriteLine($"{winnerCard} is the winner card from Player{originalIndex + 1}!\n");

        _starterOfRound = originalIndex;
        return winnerCard;
    }

    private void ComputePoints(Player player,List<Card> auxCards)
    {
        int sum = 0;
        foreach (Card card in auxCards)
        {
            if (card.GetNumber() != 9)
            {
                sum+=card.GetNumber();
            }
        }
        player.AddPoints(sum);
    }

    private void FinalResults(List<int> smallPoints)
    {
        int max = Math.Max(
            Math.Max(_players[0].GetPoints(), _players[1].GetPoints()), 
            Math.Max(_players[2].GetPoints(), _players[3].GetPoints())
            );
        
        for(int i=0;i<_players.Count;i++)
        {
            if (_players[i].GetPoints() == max)
            {
                Console.WriteLine($"The winner is Player{i + 1} with {max} points!");
            }
            else
            {
                Console.WriteLine($"Player{i + 1} has got {_players[i].GetPoints()} points!");
            }
        }
        
        Console.WriteLine();

        for (int i = 0; i < _players.Count; i++)
        {
            Console.WriteLine($"Player{i} got {(_players[i].GetPoints()/33)} and he declared he'll get {smallPoints[i]}," + 
                              $" so in conclusion {(_players[i].GetPoints() / 33-smallPoints[i])}.");
        }

        if (_players[0].GetPoints() + _players[2].GetPoints() > _players[1].GetPoints() + _players[3].GetPoints())
        {
            Console.WriteLine("Team1 (Player1 and Player3) wins!");
        }
        else
        {
            Console.WriteLine("Team2 (Player2 and Player4) wins!");
        }
    }
    
    private void CardsFight(List<int> points)
{
    int k = 0;
    int poz = 0;
    string originalTromf = _tromf;
    List<Card> auxCards = new List<Card>(4);

    while (k <= 5)
    {
        Console.WriteLine($"\nRound {k + 1}:");

        for (int i = 0; i < _players.Count; i++)
        {
            int playerIndex = (_starterOfRound + i) % _players.Count;
            Console.WriteLine($"\nPlayer{playerIndex + 1} will show his card.");
            if (k == 0) { Announcement(_players[playerIndex]); }

            Console.Write("Card name (position of the card you would like to fight): ");
            
            if (!int.TryParse(Console.ReadLine(), out int position) || position < 1 || position > 6 - k)
            {
                throw new Exception("Invalid input.");
            }

            auxCards.Add(_players[playerIndex].RemoveCardFromCertainPosition(position - 1));
        }

        Console.WriteLine("\nThe cards that the players have chosen are the following:");
        for (int i = 0; i < auxCards.Count; i++)
        {
            int playerIndex = (_starterOfRound + i) % _players.Count;
            Console.WriteLine($"Player{playerIndex + 1}: " + auxCards[i]);
        }
        
        bool hasTromf = false;
        foreach (Card card in auxCards)
        {
            if (card.GetColour().Equals(_tromf, StringComparison.OrdinalIgnoreCase))
            {
                hasTromf = true;
                break;
            }
        }
        
        if (!hasTromf)
        {
            _tromf = auxCards[0].GetColour();
            Console.WriteLine($"No tromf played. The tromf for this round is temporarily set to {_tromf}.");
        }

        Card winnerCard = DecidingTheWinnerCard(auxCards, _players[_starterOfRound]);

        for (int i = 0; i < auxCards.Count; i++)
        {
            if (auxCards[i] == winnerCard)
            {
                poz = (_starterOfRound + i) % _players.Count;
                break;
            }
        }

        ComputePoints(_players[poz], auxCards);
        _starterOfRound = poz;
        k++;
        PrintCardsOfEachPlayer();

        auxCards.Clear();

        _tromf = originalTromf;
    }

    FinalResults(points);
}

    public void StartGame()
    {
        Console.WriteLine("Welcome to Nicolae's Cross Game!");
        CountdownUntilStart();

        PrintCardsOfEachPlayer();
        
        List<int> points = new List<int>(4);
        
        ChoosingPoints(points);
        
        Console.WriteLine("\n\nPerfect!");
        Console.WriteLine($"Player 1 has chosen {points[0]} points.");
        Console.WriteLine($"Player 2 has chosen {points[1]} points.");
        Console.WriteLine($"Player 3 has chosen {points[2]} points.");
        Console.WriteLine($"Player 4 has chosen {points[3]} points.");

        ChoosingTromf(points);

        CardsFight(points);
    }
}