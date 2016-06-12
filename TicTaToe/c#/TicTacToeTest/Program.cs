using System;
using System.Collections.Generic;
using TicTacToeEngine;

namespace TicTacToeTest
{
    internal class Program
    {
        private static TicTacToeGame game;

        private static void Main()
        {
            AfterTheGivenPlays_TheWinnerIs(FakePlaySeries.NoPlay, "No winner - Game in progress.");
            AfterPlaying_TheGivenValueIsAtTheGivenPosition(FakePlaySeries.PlayOnceAtZeroZero, new Position(0, 0), "X");
            AfterPlaying_TheGivenValueIsAtTheGivenPosition(FakePlaySeries.PlayTwiceSecondAtOneOne, new Position(1, 1), "O");
            AfterTheGivenPlays_TheWinnerIs(FakePlaySeries.XWinsWithThreeXsAtColumnZero, "X");
            AfterTheGivenPlays_TheWinnerIs(FakePlaySeries.OWinsWithThreeOsAtColumnOne, "O");
            AfterTheGivenPlays_TheWinnerIs(FakePlaySeries.XWinsWithThreeXsAtRowZero, "X");
            AfterTheGivenPlays_TheWinnerIs(FakePlaySeries.OWinsWithThreeOsAtRowTwo, "O");
            AfterTheGivenPlays_TheWinnerIs(FakePlaySeries.XWinsWithThreeXsAtFirstDiagonal, "X");
            AfterTheGivenPlays_TheWinnerIs(FakePlaySeries.XWinsWithThreeXsAtSecondDiagonal, "X");

            EmptyPositionCanBePlayed();

            AlreadyUsedPositionCannotBePlayedAgain();

            Console.ReadKey();
        }

        private static void AlreadyUsedPositionCannotBePlayedAgain()
        {
            game = new TicTacToeGame();

            var position = new Position(1, 2);

            game.PlayNextTurnAt(position);
            bool canBePlayed = game.PlayNextTurnAt(position);

            if (!canBePlayed)
            {
                Console.WriteLine("Success: Position " + position + " is already used.");
            }
            else
            {
                Console.WriteLine("Failure: Position " + position + " could be played.");
            }
        }

        private static void EmptyPositionCanBePlayed()
        {
            game = new TicTacToeGame();

            var position = new Position(1, 2);

            bool playSucceded = game.PlayNextTurnAt(position);

            if (playSucceded)
            {
                Console.WriteLine("Success: Position " + position + " can be played");
            }
            else
            {
                Console.WriteLine("Failure: Position " + position + " can NOT be played");
            }
        }

        #region Tests

        private static void AfterPlaying_TheGivenValueIsAtTheGivenPosition(List<Position> positionsToBePlayed, Position position, string expectedValue)
        {
            CreateEmptyTicTacToeGame();
            PlayTheGivenPositions(positionsToBePlayed);
            AreEqual(game.ValueAt(position), expectedValue);
        }

         private static void AfterTheGivenPlays_TheWinnerIs(List<Position> positionsToBePlayed, string theWinner)
        {
             CreateEmptyTicTacToeGame();
             PlayTheGivenPositions(positionsToBePlayed);
             AreEqual(game.Winner, theWinner);
        }

        #endregion

        #region Glue code

        private static void PlayTheGivenPositions(List<Position> positions)
        {
            positions.ForEach(p => game.PlayNextTurnAt(p));
        }

        private static void CreateEmptyTicTacToeGame()
        {
            game = new TicTacToeGame();
        }
        #endregion

        #region asserts

        private static void AreEqual(string actual, string expected)
        {
            string message = "Expected value was " + expected + ", actual value was " + actual;
            var prefix = actual == expected ? "Succes: " : "Failure: ";
            Console.WriteLine(prefix + message);
        }

        #endregion
    }
}