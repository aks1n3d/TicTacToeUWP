using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace TicTacToeUWP
{
    public sealed partial class MainPage : Page
    {
        private bool isXTurn;
        private Button[] buttons;

        public MainPage()
        {
            this.InitializeComponent();
            isXTurn = true;
            buttons = new Button[] { btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9 };
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton.Content == null)
            {
                clickedButton.Content = isXTurn ? "X" : "O";
                isXTurn = !isXTurn;

                if (CheckForWin())
                {
                    string winner = isXTurn ? "O" : "X";
                    ShowWinMessage(winner);
                }
                else if (CheckForDraw())
                {
                    ShowDrawMessage();
                }
            }
        }


        private bool CheckForWin()
        {
            // Шаблони переможців
            int[][] winPatterns = new int[][]
            {
                new int[] { 0, 1, 2 },
                new int[] { 3, 4, 5 },
                new int[] { 6, 7, 8 },
                new int[] { 0, 3, 6 },
                new int[] { 1, 4, 7 },
                new int[] { 2, 5, 8 },
                new int[] { 0, 4, 8 },
                new int[] { 2, 4, 6 }
            };

            for (int i = 0; i < winPatterns.Length; i++)
            {
                if (buttons[winPatterns[i][0]].Content != null &&
                    buttons[winPatterns[i][0]].Content == buttons[winPatterns[i][1]].Content &&
                    buttons[winPatterns[i][0]].Content == buttons[winPatterns[i][2]].Content)
                {
                    return true;
                }
            }

            return false;
        }
        private async void ShowDrawMessage()
        {
            ContentDialog drawDialog = new ContentDialog()
            {
                Title = "Нічия",
                Content = "Гра закінчилась нічиєю!",
                CloseButtonText = "ОК"
            };

            await drawDialog.ShowAsync();
            ResetGame();
        }
        private async void ShowWinMessage(string winner)
        {
            ContentDialog winDialog = new ContentDialog()
            {
                Title = "Переможець",
                Content = $"Гравець {winner} виграв!",
                CloseButtonText = "ОК"
            };

            await winDialog.ShowAsync();
            ResetGame();
        }

        private bool CheckForDraw()
        {
            foreach (Button button in buttons)
            {
                if (button.Content == null)
                {
                    return false;
                }
            }
            return true;
        }
        private void ResetGame()
        {
            foreach (Button button in buttons)
            {
                button.Content = null;
            }

            isXTurn = true;
        }
    }
}
