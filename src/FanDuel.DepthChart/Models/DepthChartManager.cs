using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FanDuel.DepthChart.Models
{
    public class DepthChartManager
    {
        private readonly Dictionary<string, List<Player>> _depthChart;

        public DepthChartManager(List<string> playerPositions)
        {
            _depthChart = new Dictionary<string, List<Player>>();

            foreach (var position in playerPositions)
            {
                _depthChart.Add(position, new List<Player>());
            }
        }

        public void AddPlayerToDepthChart(string position, Player player, int? positionDepth = null)
        {
            if (_depthChart.TryGetValue(position, out List<Player> players))
            {
                //check if position is a valid positive int
                if (IsValidPosition(positionDepth, players))
                {
                    // Add player to the depth chart at the position depth.
                    players.Insert((int) positionDepth, player);
                }
                else
                {
                    // Add player to the end of the depth chart for that position.
                    players.Add(player);
                }
            }
        }

        public void RemovePlayerFromDepthChart(string position, Player player)
        {
            if (_depthChart.TryGetValue(position, out List<Player> players))
            {
                //remove player from depth chart
                players.Remove(player);
            }
        }

        public string GetBackups(string position, Player player)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (_depthChart.TryGetValue(position, out List<Player> players))
            {
                var index = players.IndexOf(player);

                // Return string of backup players
                var backupPlayers = string.Join("\n #", players.Skip(index + 1).Select(p => string.Join(" - ", p.Number, p.Name)));

                stringBuilder.Append($"#{backupPlayers}");
            }

            return stringBuilder.ToString().Trim();
        }

        public string GetFullDepthChart()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var position in _depthChart)
            {
                // if no players for this position, skip position
                if (!position.Value.Any())
                {
                    continue;
                }

                // Return string of players
                var players = string.Join("),(#", position.Value.Select(p => string.Join(",", p.Number, p.Name)));

                stringBuilder.Append($"{position.Key}: (#{players})");
                stringBuilder.Append(Environment.NewLine);
            }

            return stringBuilder.ToString().Trim();
        }

        private bool IsValidPosition(int? positionDepth, List<Player> players)
        {
            return positionDepth != null
                   && positionDepth <= players.Count - 1
                   && positionDepth >= 0;
        }
    }
}