using Models;

namespace Service;

public class BattleService:IBattleService
{
    //For the moment, maybe we should just persist the battle to table for
    //  duration of combat, and remove it once we resolve the battle and
    //  persist it to the leaderboard. Not great at scale, but to get
    //  something going.
    
    /*TODO:

        OMG WE NEED A TYPE TABLE,TOO! *Update: We have a type table now. ~Ricardo

        Constructor

        Methods:
            InitiateBattle
                -create a Battle instance(receive 2 pkmn, and 2 move lists)
                    -int id
                    -bool(?)/enum(?) state
                    -Pokemon playerPokemon
                        -normalize to lvl 50 stats
                    -Pokemon opponentPokemon
                        -normalize to lvl 50 stats
                    -int playerHP
                    -int opponentHP
                    -List<Move> playerMoves
                    -List<Move> opponentMoves
                -persist Battle
                -return Battle
            ExecuteCombatRound(receive Battle, 2 moves)
                -get order
                    -move1.priority==move2.priority?
                    -move1.priority>move2.priority?
                    -player.speed==opponent.speed?
                    -player.speed>opponent.speed?
                -damage
                    -
                    -loop move list
                        -attackerAtk = 
                        -defenderDef = 
                        -STAB = attacker.Type==move.Type?1.5:1
                        -TMultiplier = getTypeMultiplier(move1,defenderPokemon,0)
                        -rand = rand(217,255)
                        -damage = (((22)*move.power*(attackerAtk/defenderDef))/50)*STAB*TMultiplier*rand
                

            ChangeState
            ResolveDamage

            //public async int getTypeMultiplier(Move move, Pokemon pokemon, int typeNumber)
            // {
                int m1 = await _context.Database.ExecuteSqlRawAsync($"SELECT {pokemon.Types[0]} FROM Type WHERE name={move}");
                int m2 = 1;
                if (pokemon.Types.GetArrayLength == 2){
                    m2 = await _context.Database.ExecuteSqlRawAsync($"SELECT {pokemon.Types[1]} FROM Type WHERE name={move}");
                }
                return (m1*m2);
            //}
    */
}