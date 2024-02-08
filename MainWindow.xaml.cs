using System;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private bool isPlayerXTurn = true;
        private bool gameEnded = false;
        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            DisableAllButtons();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.Content.ToString() == "" && !gameEnded)
            {
                if (isPlayerXTurn)
                {
                    button.Content = "X";
                }
                else
                {
                    button.Content = "O";
                }
                isPlayerXTurn = !isPlayerXTurn;
                CheckForWinner();
            }
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            btn1.Content = btn2.Content = btn3.Content = btn4.Content = btn5.Content = btn6.Content = btn7.Content = btn8.Content = btn9.Content = null;
            gameEnded = false;
            EnableAllButtons();
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            gameEnded = false;
            EnableAllButtons();
        }

        private void DisableAllButtons()
        {
            btn1.IsEnabled = btn2.IsEnabled = btn3.IsEnabled = btn4.IsEnabled = btn5.IsEnabled = btn6.IsEnabled = btn7.IsEnabled = btn8.IsEnabled = btn9.IsEnabled = false;
        }

        private void EnableAllButtons()
        {
            btn1.IsEnabled = btn2.IsEnabled = btn3.IsEnabled = btn4.IsEnabled = btn5.IsEnabled = btn6.IsEnabled = btn7.IsEnabled = btn8.IsEnabled = btn9.IsEnabled = true;
        }

        private void MakeRobotMove()
        {
            int move = random.Next(1, 10);
            Button button = (Button)FindName("btn" + move);
            if (button.Content == null)
            {
                button.Content = "O";
                isPlayerXTurn = true;
            }
            else
            {
                MakeRobotMove();
            }
        }

        private void CheckForWinner()
        {
            if (CheckRowWinner() || CheckColumnWinner() || CheckDiagonalWinner())
            {
                gameEnded = true;
                DisableAllButtons();
                MessageBox.Show((isPlayerXTurn ? "Крестики" : "Нолики") + " выиграли!");
            }
            else if (CheckDraw())
            {
                gameEnded = true;
                DisableAllButtons();
                MessageBox.Show("Ничья!");
            }
        }
        private bool CheckRowWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                if (CheckLine(gameGrid.Children, i * 3, i * 3 + 1, i * 3 + 2))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckColumnWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                if (CheckLine(gameGrid.Children, i, i + 3, i + 6))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckDiagonalWinner()
        {
            return CheckLine(gameGrid.Children, 0, 4, 8) || CheckLine(gameGrid.Children, 2, 4, 6);
        }

        private bool CheckLine(UIElementCollection controls, int index1, int index2, int index3)
        {
            if (((Button)controls[index1]).Content == ((Button)controls[index2]).Content &&
                ((Button)controls[index2]).Content == ((Button)controls[index3]).Content &&
                ((Button)controls[index1]).Content.ToString() != "")
            {
                return true;
            }
            return false;
        }

        private bool CheckDraw()
        {
            foreach (var control in gameGrid.Children)
            {
                if (control is Button)
                {
                    if (((Button)control).Content.ToString() == "")
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}