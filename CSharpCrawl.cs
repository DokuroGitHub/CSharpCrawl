using System.Threading.Tasks;

public static class CSharpCrawl
{
    public static async Task CrawlAsync(int numberOfURLs = 100, int numberOfWorkers = 5, int timeDelayInMS = 1000)
    {
        if (numberOfURLs < 0)
        {
            throw new Exception("numberOfURLs can not be negative (>=0)");
        }
        if (numberOfWorkers <= 0)
        {
            throw new Exception("numberOfWorkers must be positive (>0)");
        }
        if (timeDelayInMS < 0)
        {
            throw new Exception("timeDelayInMS can not be negative (>=0)");
        }

        var urls = new List<string>();
        for (int i = 0; i < numberOfURLs; i++)
        {
            urls.Add($"url: {i + 1}/{numberOfURLs}");
        }

        var tasks = new List<Task>();

        // Start a task for each worker
        int workingWorker = 0;
        int workingURLIndex = 0;
        while (workingURLIndex < urls.Count)
        {
            if (workingWorker < numberOfWorkers)
            {
                tasks.Add(Task.Run(async () =>
                {
                    Console.WriteLine($"workingWorker: {workingWorker}, workingURLIndex: {workingURLIndex}");
                    await Task.Delay(timeDelayInMS);
                    workingWorker--;
                }));
                workingWorker++;
                workingURLIndex++;
            }
        }

        // Wait for all tasks to complete
        await Task.WhenAll(tasks);

        Console.WriteLine($"CrawlAsync: done");
    }
}