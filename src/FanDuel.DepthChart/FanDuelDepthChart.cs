using System;
using System.Collections;
using System.Collections.Generic;
using FanDuel.DepthChart.Models;

namespace FanDuel.DepthChart
{
    public class FanDuelDepthChart
    {
        public void Run()
        {
            //for the purpose of this exercise, I've only added positions relevant to the test case
            var depthChart = new DepthChartManager(new List<string>
            {
                Constants.PlayerPositions.QB,
                Constants.PlayerPositions.LWR,
                Constants.PlayerPositions.ROLB,
                Constants.PlayerPositions.LOLB,
                Constants.PlayerPositions.LCB,
                Constants.PlayerPositions.RCB,
                Constants.PlayerPositions.KR
            });

            var tomBrady = new Player()
            {
                Name = "Tom Brady",
                Number = 12,
                Position = Constants.PlayerPositions.QB
            };

            var blaineGabbert = new Player()
            {
                Name = "Blaine Gabbert",
                Number = 11,
                Position = Constants.PlayerPositions.QB
            };

            var kyleTrask = new Player()
            {
                Name = "Kyle Trask",
                Number = 2,
                Position = Constants.PlayerPositions.ROLB,
            };

            var mikeEvans = new Player()
            {
                Name = "Mike Evans",
                Number = 13,
                Position = Constants.PlayerPositions.LOLB,
            };

            var jaelonDarden = new Player()
            {
                Name = "Jaelon Darden",
                Number = 1,
                Position = Constants.PlayerPositions.LCB,
            };

            var scottMiller = new Player()
            {
                Name = "Scott Miller",
                Number = 10,
                Position = Constants.PlayerPositions.RCB,
            };

            depthChart.AddPlayerToDepthChart(Constants.PlayerPositions.QB, tomBrady, 0);
            depthChart.AddPlayerToDepthChart(Constants.PlayerPositions.QB, blaineGabbert, 1);
            depthChart.AddPlayerToDepthChart(Constants.PlayerPositions.QB, kyleTrask, 2);
            depthChart.AddPlayerToDepthChart(Constants.PlayerPositions.LWR, mikeEvans, 0);
            depthChart.AddPlayerToDepthChart(Constants.PlayerPositions.LWR, jaelonDarden, 1);
            depthChart.AddPlayerToDepthChart(Constants.PlayerPositions.LWR, scottMiller, 2);

            Console.WriteLine(depthChart.GetBackups(Constants.PlayerPositions.QB, tomBrady));
            Console.WriteLine(depthChart.GetBackups(Constants.PlayerPositions.QB, jaelonDarden));
            Console.WriteLine(depthChart.GetBackups(Constants.PlayerPositions.QB, mikeEvans));
            Console.WriteLine(depthChart.GetBackups(Constants.PlayerPositions.QB, blaineGabbert));
            Console.WriteLine(depthChart.GetBackups(Constants.PlayerPositions.QB, kyleTrask));

            Console.WriteLine(depthChart.GetFullDepthChart());

            depthChart.RemovePlayerFromDepthChart(Constants.PlayerPositions.LWR, mikeEvans);

            Console.WriteLine(depthChart.GetFullDepthChart());
        }
    }
}