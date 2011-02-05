namespace BibtexEntryManager.Data
{
    public static class Query
    {
        public const string Article = "SELECT * FROM Article";
        public const string Book = "SELECT * FROM Book";
        public const string BookLet = "SELECT * FROM BookLet";
        public const string Conference = "SELECT * FROM Conference";
        public const string InBook = "SELECT * FROM InBook";
        public const string InCollection = "SELECT * FROM InCollection";
        public const string InProceedings = "SELECT * FROM InProceedings";
        public const string Manual = "SELECT * FROM Manual";
        public const string MasterThesis = "SELECT * FROM MastersThesis";
        public const string Misc = "SELECT * FROM Misc";
        public const string PhdThesis = "SELECT * FROM PhdThesis";
        public const string Proceedings = "SELECT * FROM Proceedings";
        public const string TechReport = "SELECT * FROM TechReport";
        public const string UnPublished = "SELECT * FROM UnPublished";
    }
}