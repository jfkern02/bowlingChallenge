using Bowling;
using Bowling.Serivces.Interfaces;
using Bowling.Serivces;
using Bowling.Games;
using Bowling.Games.Interfaces;
using Bowling.ExternalServices.Interfaces;
using Bowling.ExternalServices;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("!!RULES!!");
Console.WriteLine("When you select your game you will be able to select your ball and shoe type.");
Console.WriteLine("When you bowl the application will generate a psudeo random number based on few factors.");
Console.WriteLine("You start with 3 random rolls per bowl.");
Console.WriteLine("You shoe type and ball type effect the roll. Each selection will tell you its impact.");
Console.WriteLine("For example, Bowling with a Heavy Ball and Slick Shoes will re-roll the number of pins hit 5 times.");
Console.WriteLine("You can change your setup between rounds in the Manual game, however the Random game will be locked in.");
Console.WriteLine("Good luck and have fun!");
Console.WriteLine("-----------------------------------------------------");

var services = new ServiceCollection();
services.AddSingleton<IBowlAction, BowlAction>();
services.AddSingleton<IGame, Game>();
services.AddSingleton<IConsoleActions, ConsoleActions>();
services.AddSingleton<IRandomHelper, RandomHelper>();

services.AddTransient<StartGame>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<StartGame>();

app.Start();