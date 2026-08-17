using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class Matchmaking
{
    private List<Player> queue = new List<Player>();
    private const int MAX_PLAYERS = 10;

    public void JoinQueue(Player player)
    {
        if (queue.Count >= MAX_PLAYERS)
        {
            Console.WriteLine("Queue is full!");
            return;
        }

        if (queue.Any(p => p.PlayerID == player.PlayerID))
        {
            Console.WriteLine("Player is already in the queue!");
            return;
        }

        queue.Add(player);
        Console.WriteLine(player.Username + " joined the queue.");
    }

    public void LeaveQueue(int playerID)
    {
        Player player = queue.FirstOrDefault(p => p.PlayerID == playerID);

        if (player == null)
        {
            Console.WriteLine("Player is not in the queue.");
            return;
        }

        queue.Remove(player);
        Console.WriteLine(player.Username + " left the queue.");
    }

    public void ViewQueue()
    {
        Console.WriteLine("\n--- MATCHMAKING QUEUE ---");

        if (queue.Count == 0)
        {
            Console.WriteLine("Queue is empty.");
            return;
        }

        foreach (Player player in queue)
        {
            Console.WriteLine(player.PlayerID + " - " +
                              player.Username + " - Level " +
                              player.Level);
        }

        Console.WriteLine("Players: " + queue.Count + "/10");
    }

    public void CreateMatch()
    {
        if (queue.Count < MAX_PLAYERS)
        {
            Console.WriteLine("Not enough players. 10 players are required.");
            return;
        }

        List<Player> players = queue.OrderBy(p => p.Level).Take(10).ToList();

        int levelDifference = players[9].Level - players[0].Level;

        if (levelDifference > 10)
        {
            Console.WriteLine("Players have levels that are too far apart.");
            return;
        }

        Console.WriteLine("\n--- MATCH CREATED ---");
        Console.WriteLine("Team A:");

        for (int i = 0; i < 10; i += 2)
        {
            Console.WriteLine(players[i].Username +
                              " - Level " + players[i].Level);
        }

        Console.WriteLine("\nTeam B:");

        for (int i = 1; i < 10; i += 2)
        {
            Console.WriteLine(players[i].Username +
                              " - Level " + players[i].Level);
        }

        queue.RemoveRange(0, 10);
    }
}