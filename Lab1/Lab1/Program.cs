using System.Globalization;

var reader = File.OpenText("INPUT.txt");
int days;
int.TryParse(reader.ReadLine(), out days);

float profit = 100;
float dollarRate, euroRate, nextDollarRate, nextEuroRate;

if (days != default)
{
    for (var i = 0; i < days; i++)
    {
        var line = reader.ReadLine();
        if (line != null)
        {
            float.TryParse(line.Split(" ")[0], out dollarRate);
            Console.WriteLine(dollarRate);
            float.TryParse(line.Split(" ")[1], out euroRate);
        }
        else
        {
            Console.WriteLine("Number of lines must be equal to number of days");
            break;
        }

        if (i + 1 - days < 0)
        {
            line = reader.ReadLine();
            float.TryParse(line.Split(" ")[0], out nextDollarRate);
            float.TryParse(line.Split(" ")[1], out nextEuroRate);
            var dollarProfit = GetProfit(dollarRate, nextDollarRate, profit);
            var euroProfit = GetProfit(euroRate, nextEuroRate, profit);
            var bestProfit = dollarProfit > euroProfit ? dollarProfit : euroProfit;
            profit = bestProfit > 0 ? bestProfit : profit;
            i++;
        }
    }
}
else
{
    Console.WriteLine("First line must be equal to days");
}

reader.Close();

File.WriteAllText("OUTPUT.txt", profit.ToString(CultureInfo.CurrentCulture));

float GetProfit(float rateToday, float rateTomorrow, float amount)
{
    if (rateTomorrow > rateToday) return amount * rateTomorrow / rateToday;
    return -1;
}