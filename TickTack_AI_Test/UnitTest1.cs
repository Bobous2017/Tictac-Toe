using Xunit;

namespace TickTack_AI_Test
{
    /*
     tests are doing:
    1. CheckWin_Returns1_WhenFirstRowIsSame
    •	Purpose: Verifies that the game correctly detects a win when the first row is filled with the same symbol.
    •	How:
    •	Sets up the board so that positions 1, 2, and 3 are all 'X' (arr = { '0', 'X', 'X', 'X', ... }).
    •	Calls the CheckWin method.
    •	Asserts that the result is 1, which means a win is detected.
    2. CheckWin_ReturnsMinus1_WhenDraw
    •	Purpose: Verifies that the game correctly detects a draw when the board is full and there is no winner.
    •	How:
    •	Sets up the board so all positions are filled with 'X' or 'O' and no winning combination exists (arr = { '0', 'X', 'O', 'X', 'X', 'O', 'O', 'O', 'X', 'X' }).
    •	Calls the CheckWin method.
    •	Asserts that the result is -1, which means a draw is detected.
    ---
    Summary:
    You are testing the core logic of your Tic Tac Toe game:
    •	The first test checks if a win is detected when the first row is filled.
    •	The second test checks if a draw is detected when the board is full and there is no winner.
    Both tests passed, so your win/draw detection logic is working as expected!

     * */
    public class UnitTest1
    {
        [Fact]
        public void CheckWin_Returns1_WhenFirstRowIsSame()
        {
            // Arrange: set up a winning board for the first row
            var arr = new char[] { '0', 'X', 'X', 'X', '4', '5', '6', '7', '8', '9' };
            typeof(Tictac_Toe.Program)
                .GetField("arr", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
                .SetValue(null, arr);

            // Act: call the private CheckWin method
            int result = (int)typeof(Tictac_Toe.Program)
                .GetMethod("CheckWin", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
                .Invoke(null, null);

            // Assert: should be a win
            Assert.Equal(1, result);
        }

        [Fact]
        public void CheckWin_ReturnsMinus1_WhenDraw()
        {
            // Arrange: set up a draw board
            var arr = new char[] { '0', 'X', 'O', 'X', 'X', 'O', 'O', 'O', 'X', 'X' };
            typeof(Tictac_Toe.Program)
                .GetField("arr", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
                .SetValue(null, arr);

            // Act
            int result = (int)typeof(Tictac_Toe.Program)
                .GetMethod("CheckWin", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
                .Invoke(null, null);

            // Assert: should be a draw
            Assert.Equal(-1, result);
        }
    }
}