using Models;

public class BattleState
{
    public Pokemon playerPokemon { get; set; }
    public Pokemon opponentPokemon { get; set; }
    public int playerHealth { get; set; }
    public int opponentHealth { get; set; }

    public BattleState(Pokemon playerPokemon, Pokemon opponentPokemon, BattlePhase currentPhase)
    {
        this.playerPokemon = playerPokemon;
        this.opponentPokemon = opponentPokemon;
        this.playerHealth = playerPokemon.Hp;
        this.opponentHealth = opponentPokemon.Hp;
        
        // Move the switch statement inside the constructor or a method
        switch (currentPhase)
        {
            case BattlePhase.Selection:
                // player selects a move
                // opponent selects a move
                break;
            case BattlePhase.Attack:
                // player and opponent attack in an order determined by speed and priority
                break;
            case BattlePhase.Resolution:
                // check if player or opponent fainted
                // if both are alive, go to selection phase
                // if one has fainted, go to end phase
                break;
            case BattlePhase.End:
                // display winner
                // send battle results to database
                // signal to frontend to return to main page.
                break;
            default:

                break;
        }
    }
}

public enum BattlePhase
{
    Selection,
    Attack,
    Resolution,
    End
}