namespace Arrays;

public class ArrayExercises
{

    static void RotateSquareMatrixClockwise90()
    {
        var data = new int[][]
        {
            new int[] { 1, 2, 3, 4 },
            new int[] { 5, 6, 7, 8 },
            new int[] { 9, 10, 11, 12 },
            new int[] { 13, 14, 15, 16 },
        };
        var n = 4;
        var end = n - 1;

        Log($"0, 0 -> {data[0][0]}");
        Log($"0, 0 -> {data[0][2]}");

        for (var lvl = 0; lvl < n / 2; lvl++) // 2 levels
        {
            Log($"Level {lvl}\n");
            for (var j = lvl; j < end - lvl; j++)
            {
                var topCurr = data[lvl][j]; // tracks right
                var rightCurr = data[j][end - lvl]; // tracks down
                var bottomCurr = data[end - lvl][end - j]; // tracks left
                var leftCurr = data[end - j][lvl]; // tracks up
                Log($"top={topCurr}, right={rightCurr} bottom={bottomCurr} left={leftCurr}");

                (data[lvl][j], data[j][end - lvl], data[end - lvl][end - j], data[end - j][lvl]) =
                    (data[end - j][lvl], data[lvl][j], data[j][end - lvl], data[end - lvl][end - j]);
            }
        }

        foreach (var d in data)
        {
            Console.WriteLine(string.Join(',', d));
        }
    }

    static void Log(string str) => Console.WriteLine(str);
}