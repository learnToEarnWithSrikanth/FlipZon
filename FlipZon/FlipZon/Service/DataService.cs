namespace MauiSampleTest.Service
{
    public class DataService : IDataService
    {
        public bool ClearDetailPageStack { get; set; }

        public DataService()
        {
            ClearDetailPageStack = true;
        }
    }
}

