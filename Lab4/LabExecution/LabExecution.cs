namespace LabExecution;

public class LabExecution
{
    public void Run(string path)
    {
        AppDomain.CurrentDomain.ExecuteAssembly(path);
    }
}