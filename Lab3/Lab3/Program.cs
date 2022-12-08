var reader = File.OpenText("INPUT.txt");
var n = int.Parse(reader.ReadLine());
var time = new int[1 + n];

for (var i = 0; i < 1 + n; i++)
{
    time[i] = 0;
}

var line = reader.ReadLine().Split(" ");

for (var i = 1; i <= n; i++)
{
    time[i] = int.Parse(line[i - 1]);
}

var depends = new List<int>[1 + n];
for (var i = 0; i < 1 + n; i++)
{
    depends[i] = new List<int>();
}

for (var i = 1; i <= n; i++)
{
    line = reader.ReadLine().Split(" ");
    var count = int.Parse(line[0]);
    for (var j = 0; j < count; j++)
    {
        var x = int.Parse(line[j+1]);
        depends[i].Add(x);
    }
}

var made = new bool[1 + n];
for (var i = 0; i < made.Length; i++)
{
    made[i] = false;
}

var order = new List<int>();
long totalTime = 0;

Action<int> make = null;

make = (int cur) =>
{
    if (made[cur])
    {
        return;
    }
    
    made[cur] = true;

    foreach (var x in depends[cur])
    {
        make(x);
    }
    
    order.Add(cur);
    totalTime += time[cur];
};

make(1);

File.WriteAllText("OUTPUT.txt", totalTime + " " + order.Count);
File.AppendAllText("OUTPUT.txt", Environment.NewLine);

for (var i = 0; i < order.Count; i++)
{
    if (i > 0)
    {
        File.AppendAllText("OUTPUT.txt", " ");
    }

    File.AppendAllText("OUTPUT.txt", order[i].ToString());
}