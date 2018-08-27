using System;

class checkers
{
    static int[,]init_board()
    {
        int[,] board = new int[,]
        {
            {0, 1, 0, 1, 0, 1, 0, 1, 0, 1},
            {1, 0, 1, 0, 1, 0, 1, 0, 1, 0},
            {0, 1, 0, 1, 0, 1, 0, 1, 0, 1},
            {1, 0, 1, 0, 1, 0, 1, 0, 1, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 2, 0, 2, 0, 2, 0, 2, 0, 2},
            {2, 0, 2, 0, 2, 0, 2, 0, 2, 0},
            {0, 2, 0, 2, 0, 2, 0, 2, 0, 2},
            {2, 0, 2, 0, 2, 0, 2, 0, 2, 0}
        };

        return (board);
    }


    static void draw_board(int[,] board)
    {
        Console.Write("\n");
        Console.WriteLine("   | A | B | C | D | E | F | G | H | I | J |");
        Console.WriteLine("--------------------------------------------");
        for (int i = 0; i < 10; i++)
        {
            if (i != 9)
                Console.Write(" " + (i + 1) + " ");
            else
                Console.Write(" " + (i + 1));
            for (int j = 0; j < 10; j++)
            {
                Console.Write("|");
                if (board[i, j] == 0)
                    Console.Write("   ");
                else if (board[i, j] == 1)
                    Console.Write(" O ");
                else if (board[i, j] == 2)
                    Console.Write(" X ");
            }
            Console.WriteLine("|");
        }
        Console.WriteLine("--------------------------------------------");
        Console.Write("\n");
    }


    static int my_abs_sub(int i, int j)
    {
        int result = i - j;

        if (result >= 0)
            return (result);
        else
            return (result * -1);
    }


    static bool is_there_enemy(int[,] board, int player, int i, int j, int k, int n)
    {
        if (j + 2 == n && i + 2 == k && board[i + 1, j + 1] == get_enemy_num(player))
            return (true);
        if (j - 2 == n && i - 2 == k && board[i - 1, j - 1] == get_enemy_num(player))
            return (true);
        if (j + 2 == n && i - 2 == k && board[i - 1, j + 1] == get_enemy_num(player))
            return (true);
        if (j - 2 == n && i + 2 == k && board[i + 1, j - 1] == get_enemy_num(player))
            return (true);
        return (false);
    }


    static bool check_move(int[,] board, int player, int i, int j, int k, int n)
    {
        if (i == k && my_abs_sub(j, n) != 2)
            return (false);
        if (j == n && player == 1 && k - i != 2)
            return (false);
        if (j == n && player == 2 && i - k != 2)
            return (false);
        if (i != k && j != n && (my_abs_sub(i, k) != 2 || my_abs_sub(j, n) != 2))
            return (false);
        if (i != k && j != n && my_abs_sub(i, k) == 2 && my_abs_sub(j, n) == 2)
        {
            if (is_there_enemy(board, player, i, j, k , n))
                return (true);
            else
                return (false);
        }
        return (true);
    }


    static void select_coin(int[,] board, int player, ref int i, ref int j)
    {
        string response = null;
        int len = 0;

        do
        {
            Console.Write("Player " + player + ", choose your coin : ");
            response = Console.ReadLine();
            j = response[0] - 65;
            len = response.Length;
            if (len == 2)
                i = response[1] - 49;
            if (len == 3)
                i = (response[1] - 48) * 10 + (response[2] - 48) - 1;
            if (board[i, j] != player)
                Console.WriteLine("You don't have a coin in " + response + " !");
        }while (board[i, j] != player);
    }


    static void select_destination(int[,] board, int player, ref int k, ref int n)
    {
        string response = null;
        int len = 0;

        do
        {
            Console.Write("Choose a destination : ");
            response = Console.ReadLine();
            n = response[0] - 65;
            len = response.Length;
            if (len == 2)
                k = response[1] - 49;
            if (len == 3)
                k = (response[1] - 48) * 10 + (response[2] - 48) - 1;
            if (board[k, n] != 0)
                Console.WriteLine("There is a coin in " + response + ", please choose a free place");
        }while (board[k, n] != 0);
    }


    static int get_enemy_num(int player)
    {
        if (player == 1)
            return (2);
        else if (player == 2)
            return (1);
        else
            return (0);
    }


    static bool affect_board(int[,] board, int player, int i, int j, int k, int n)
    {
        board[i, j] = 0;
        board[k, n] = player;
        if (j + 2 == n && i + 2 == k && board[i + 1, j + 1] == get_enemy_num(player))
            board[i + 1, j + 1] = 0;
        if (j - 2 == n && i - 2 == k && board[i - 1, j - 1] == get_enemy_num(player))
            board[i - 1, j - 1] = 0;
        if (j + 2 == n && i - 2 == k && board[i - 1, j + 1] == get_enemy_num(player))
            board[i - 1, j + 1] = 0;
        if (j - 2 == n && i + 2 == k && board[i + 1, j - 1] == get_enemy_num(player))
            board[i + 1, j - 1] = 0;
        return (false);
    }


    static void reset(ref int i, ref int j, ref int k, ref int n)
    {
        i = 0;
        j = 0;
        k = 0;
        n = 0;
    }


    static void verify_destination(int[,] board, int player, ref int i, ref int j, ref int k, ref int n)
    {
        string response = null;

        select_destination(board, player, ref k, ref n);
            if (!check_move(board, player, i, j, k, n))
            {
                Console.WriteLine("This deplacement is impossible, please choose another one");
                Console.Write("Would you like to begin your turn again ? [y/n] : ");
                response = Console.ReadLine();
                if (response[0] == 'y')
                {
                    reset(ref i, ref j, ref k, ref n);
                    select_coin(board, player, ref i, ref j);
                }
            }
    }


    static void play(int[,] board, int player)
    {
        int i = 0;
        int j = 0;
        int k = 0;
        int n = 0;

        select_coin(board, player, ref i, ref j);
        while (!check_move(board, player, i, j, k, n))
        {
            verify_destination(board, player, ref i, ref j, ref k, ref n);
        }
        affect_board(board, player, i, j, k, n);
    }


    static bool game_finished(int[,] board)
    {
        int actual = 0;

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (actual == 0 && (board[i, j] == 1 || board[i, j] == 2))
                    actual = board[i, j];
                if (actual != 0 && board[i, j] != 0 && actual != board[i, j])
                    return (false);
            }
        }
        return (true);
    }


    static void Main()
    {
        int[,] board = init_board();
        int player = 1;

        while (!game_finished(board))
        {
            draw_board(board);
            play(board, player);
            player = get_enemy_num(player);
        }
        Console.Read();
    }
}