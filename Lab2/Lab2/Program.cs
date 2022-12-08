var reader = File.OpenText("INPUT.txt");

var n = int.Parse(reader.ReadLine());

var funcResult = reader.ReadLine().ToCharArray();

var funcValuesResult = funcResult.Select(chr =>
{
    return chr switch
    {
        '1' => 1,
        '0' => 0,
        _ => -1
    };
}).ToArray();

var info = Enumerable.Range(0, 2).Select(i =>
    Enumerable.Repeat(new Info { possible = false, nOnes = 0, last = -1, prevResult = -1 }, n + 1).ToArray()).ToArray();

var f = new [,]
{
    { funcValuesResult[0], funcValuesResult[1] },
    { funcValuesResult[2], funcValuesResult[3] }
};

info[0][1].possible = true;
info[0][1].nOnes = 0;
info[0][1].last = 0;
info[0][1].prevResult = -1;

info[1][1].possible = true;
info[1][1].nOnes = 1;
info[1][1].last = 1;
info[1][1].prevResult = -1;

for (int i = 0; i < info.GetLength(0); i++)
{
    for(int j = 0; j < info[i].Count(); j++)
    {
        Console.Write(info[i][j].last + "\t");
    }
    Console.WriteLine();
}

for (var len = 2; len <= n; len++)
{
    for (var prevResult = 0; prevResult <= 1; prevResult++)
    {
        if(!info[prevResult][len-1].possible) continue;
        for (var lastDigit = 0; lastDigit <= 1; lastDigit++)
        {
            var result = f[prevResult, lastDigit];
            var nOnes = info[prevResult][len - 1].nOnes + lastDigit;
            if (!info[result][len].possible || nOnes > info[result][len].nOnes)
            {
                info[result][len].possible = true;
                info[result][len].nOnes = nOnes;
                info[result][len].last = lastDigit;
                info[result][len].prevResult = prevResult;
            }
        }
    }
}


var ans = new List<int>();

if (info[1][n].possible)
{
    var len = n;
    var result = 1;
    while (len > 0)
    {
        ans.Add(info[result][len].last);
        result = info[result][len].prevResult;
        len--;
    }

    ans.Reverse();
}

File.WriteAllText("OUTPUT.txt", ans.Count != 0 ? string.Join("", ans) : "No solution");

struct Info
{
    public bool possible;
    public int nOnes;
    public int last;
    public int prevResult;
}