using System;
using System.Collections.Generic;
using System.Text;
using FanDuel.DepthChart.Models;
using FluentAssertions;
using Xunit;

namespace FanDuel.DepthChart.Tests
{
    public class DepthChartsTests
    {
        public class AddPlayerToDepthChart
        {
            [Fact]
            public void AddPlayerToDepthChart_AtSpecifiedPosition()
            {
                var testPlayer1 = new Player()
                {
                    Number = 1,
                    Name = "Test1",
                    Position = Constants.PlayerPositions.QB
                };
                var testPlayer2 = new Player()
                {
                    Number = 2,
                    Name = "Test2",
                    Position = Constants.PlayerPositions.LWR
                };

                var depthChart = new DepthChartManager(new List<string>
                {
                    Constants.PlayerPositions.QB,
                    Constants.PlayerPositions.LWR
                });

                depthChart.AddPlayerToDepthChart(Constants.PlayerPositions.QB, testPlayer1, 0);
                depthChart.AddPlayerToDepthChart(Constants.PlayerPositions.LWR, testPlayer2, 1);

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("QB: (#1,Test1)");
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append("LWR: (#2,Test2)");

                var expectedOutput = stringBuilder.ToString();

                var output = depthChart.GetFullDepthChart();

                output.Should().Be(expectedOutput);
            }

            [Fact]
            public void AddPlayerToDepthChart_PositionNotSpecified()
            {
                var testPlayer1 = new Player()
                {
                    Number = 1,
                    Name = "Test1",
                    Position = Constants.PlayerPositions.QB
                };
                var testPlayer2 = new Player()
                {
                    Number = 2,
                    Name = "Test2",
                    Position = Constants.PlayerPositions.QB,
                };
                var testPlayer3 = new Player()
                {
                    Number = 3,
                    Name = "Test3",
                    Position = Constants.PlayerPositions.QB,
                };

                var depthChart = new DepthChartManager(new List<string>
                {
                    Constants.PlayerPositions.QB
                });

                depthChart.AddPlayerToDepthChart(Constants.PlayerPositions.QB, testPlayer1, 0);
                depthChart.AddPlayerToDepthChart(Constants.PlayerPositions.QB, testPlayer2, 1);
                depthChart.AddPlayerToDepthChart(Constants.PlayerPositions.QB, testPlayer3);

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("QB: (#1,Test1),(#2,Test2),(#3,Test3)");

                var expectedOutput = stringBuilder.ToString();

                var output = depthChart.GetFullDepthChart();

                output.Should().Be(expectedOutput);
            }
            
            [Fact]
            public void AddPlayerToDepthChart_NegativePosition()
            {
                var testPlayer1 = new Player()
                {
                    Number = 1,
                    Name = "Player1",
                    Position = Constants.PlayerPositions.QB
                };
                var testPlayer2 = new Player()
                {
                    Number = 2,
                    Name = "Player2",
                    Position = Constants.PlayerPositions.QB
                };
                var testPlayer3 = new Player()
                {
                    Number = 3,
                    Name = "Player3",
                    Position = Constants.PlayerPositions.QB
                };

                var depthChart = new DepthChartManager(new List<string>
                {
                    Constants.PlayerPositions.QB
                });

                depthChart.AddPlayerToDepthChart(Constants.PlayerPositions.QB, testPlayer1, -1);
                depthChart.AddPlayerToDepthChart(Constants.PlayerPositions.QB, testPlayer2);
                depthChart.AddPlayerToDepthChart(Constants.PlayerPositions.QB, testPlayer3, -5);

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("QB: (#1,Player1),(#2,Player2),(#3,Player3)");

                var expectedOutput = stringBuilder.ToString();

                var output = depthChart.GetFullDepthChart();

                output.Should().Be(expectedOutput);
            }

            [Fact]
            public void AddPlayerToDepthChart_MultiplePlayers()
            {
                var testPlayer1 = new Player()
                {
                    Number = 1,
                    Name = "Player1",
                    Position = Constants.PlayerPositions.QB
                };
                var testPlayer2 = new Player()
                {
                    Number = 2,
                    Name = "Player2",
                    Position = Constants.PlayerPositions.QB
                };

                var chart = new DepthChartManager(new List<string>
                {
                    Constants.PlayerPositions.QB
                });

                chart.AddPlayerToDepthChart(Constants.PlayerPositions.QB, testPlayer1, 0);
                chart.AddPlayerToDepthChart(Constants.PlayerPositions.QB, testPlayer2, 0);

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("QB: (#2,Player2),(#1,Player1)");

                var expectedOutput = stringBuilder.ToString();

                var output = chart.GetFullDepthChart();

                output.Should().Be(expectedOutput);
            }
        }


        [Fact]
        public void RemovePlayerFromDepthChart()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("QB: (#1,Player1)");

            var expectedOutput = stringBuilder.ToString();

            var testPlayer1 = new Player()
            {
                Number = 1,
                Name = "Player1",
                Position = Constants.PlayerPositions.QB,
            };
            var testPlayer2 = new Player()
            {
                Number = 2,
                Name = "Player2",
                Position = Constants.PlayerPositions.LWR,
            };

            var depthChart = new DepthChartManager(new List<string>
            {
                Constants.PlayerPositions.QB,
                Constants.PlayerPositions.LWR
            });

            depthChart.AddPlayerToDepthChart(Constants.PlayerPositions.QB, testPlayer1, 0);
            depthChart.AddPlayerToDepthChart(Constants.PlayerPositions.LWR, testPlayer2, 0);
            depthChart.RemovePlayerFromDepthChart(Constants.PlayerPositions.LWR, testPlayer2);

            var output = depthChart.GetFullDepthChart();

            output.Should().Be(expectedOutput);
        }

        [Fact]
        public void GetBackups()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("#2 - Player2");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(" #3 - Player3");

            var expectedOutput = stringBuilder.ToString();

            var testPlayer1 = new Player()
            {
                Number = 1,
                Name = "Player1",
                Position = Constants.PlayerPositions.QB,
            };
            var testPlayer2 = new Player()
            {
                Number = 2,
                Name = "Player2",
                Position = Constants.PlayerPositions.QB,
            };
            var testPlayer3 = new Player()
            {
                Number = 3,
                Name = "Player3",
                Position = Constants.PlayerPositions.QB,
            };

            var depthChart = new DepthChartManager(new List<string>
            {
                Constants.PlayerPositions.QB
            });

            depthChart.AddPlayerToDepthChart(Constants.PlayerPositions.QB, testPlayer1);
            depthChart.AddPlayerToDepthChart(Constants.PlayerPositions.QB, testPlayer2);
            depthChart.AddPlayerToDepthChart(Constants.PlayerPositions.QB, testPlayer3);

            var output = depthChart.GetBackups(Constants.PlayerPositions.QB, testPlayer1);

            output.Should().Be(expectedOutput);
        }

        [Fact]
        public void GetFullDepthChart()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("QB: (#1,Player1)");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("LWR: (#2,Player2)");

            var expectedOutput = stringBuilder.ToString();

            var testPlayer1 = new Player()
            {
                Number = 1,
                Name = "Player1",
                Position = Constants.PlayerPositions.QB,
            };
            var testPlayer2 = new Player()
            {
                Number = 2,
                Name = "Player2",
                Position = Constants.PlayerPositions.LWR,
            };

            var depthChart = new DepthChartManager(new List<string>
            {
                Constants.PlayerPositions.QB,
                Constants.PlayerPositions.LWR
            });

            depthChart.AddPlayerToDepthChart(Constants.PlayerPositions.QB, testPlayer1, 0);
            depthChart.AddPlayerToDepthChart(Constants.PlayerPositions.LWR, testPlayer2, 0);

            var output = depthChart.GetFullDepthChart();

            output.Should().Be(expectedOutput);
        }
    }
}