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

var info = Enumerable
    .Repeat(Enumerable.Repeat(new Info { possible = false, nOnes = 0, last = -1, prevResult = -1 }, n + 1).ToList(),
        2).ToList();

var f = new int[2, 2];

for (var i = 0; i < 2; i++)
{
    for (var j = 0; j < 2; j++)
    {
        f[i, j] = funcValuesResult[i + j];
    }
}

var element = new Info
{
    possible = true,
    nOnes = 0,
    last = 0,
    prevResult = -1
};
info[0].Insert(1, element);
info[0].RemoveAt(2);

element = new Info
{
    possible = true,
    nOnes = 1,
    last = 1,
    prevResult = -1
};

info[1].Insert(1, element);
info[1].RemoveAt(2);

element = new Info()
{
    possible = false,
    nOnes = -1,
    last = -1,
    prevResult = -1
};

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
                element = new Info
                {
                    possible = true,
                    nOnes = nOnes,
                    last = lastDigit,
                    prevResult = prevResult
                };
                info[result].Insert(len, element);
                info[result].RemoveAt(len + 1);

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
        ans.Add(info[1][len].last);
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