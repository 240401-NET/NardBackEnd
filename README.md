# NardBackEnd

Team
Nakiyyah Price
Adam Williams
Ricardo PenaMcKnight
Dean Grey



Proposal

PokéBattle Arena
Project Overview
The application, now called "PokéBattle Arena", allows users to engage in simulated Pokémon battles against the computer. The system will simulate real-time battles by considering each Pokémon's stats, moves, effects, and types, similar to how battles work in Pokémon games.
User Stories
The user will land on a page displaying two pokemon slots (user pokemon and opponent pokemon)
The user will be able to select their pokemon and their opponent pokemon randomly, from a list or by search. 
A button will allow the user to initiate a battle once both pokemon have been selected. 
If one or both pokemon have not been selected prior to clicking the button to initiate a battle then both pokemon will be chosen randomly. 
An animation will play between pages during loading time. 
The user will be redirected to the battle page which will display both pokemon on opposite sides of the screen as well as their names and health with a message log indicating what is taking place in the battle and allowing the user to select between moves during a selection phase.
Once the battle begins, the player will be prompted to choose between one of four moves which their pokemon will use for the turn during the combat phase.
Once the selection is made, speed will determine which pokemon will act first in combat. 
A message will appear on screen during the combat phase indicating what move each pokemon is attempting to enact on its turn and whether or not it has succeeded or failed. 
Moves that cause damage will cause a health near the pokemon’s name to decrease as appropriate. 
Once health for either pokemon has reached zero the battle will end and the screen will display the name of the victorious monster. 
The battle record will record the results of the battle and display them on a leaderboard page. 
Once the battle is over the user will be redirected back from the battle page to the selection page. 

MVP Goals
Pokemon selection
Custom movesets
Battle Simulation
Leaderboard
Battle Music

Stretch Goals
Expand to larger pokemon team (6)
User/Account Registration/Login/Management
Implement Items
Web Sockets for online multiplayer
Animated Moves
Custom Pokemon / Fakemon
Custom Moves
Battle Replays and Sharing
Team Battles
Custom Rules
Tournaments

Tech Stack
C#
Entity Framework Core (ORM)
SQL Server 
Azure Cloud Server
ASP.NET Core (Web API Framework)
xUnit/Moq 
Azure App Service (for application hosting)
React 

Outside API
We will use the PokeAPI which will allow us to fetch Pokemon and Move data.


ERD




Wireframe
https://excalidraw.com/#json=kpz5nRY_Wv0GpT5h6J2mS,ptjazr5d8ZIYr_Ie5VkXCg
