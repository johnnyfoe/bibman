namespace BibtexEntryManager.Error
{
    public static class ErrorMessages
    {
        public const string NoteIsRequired = "The note is required";
        public const string ChapterIsRequired = "The chapter is required";
        public const string BooktitleIsRequired = "The book title is required";
        public const string PublisherIsRequired = "The publisher is required";
        public const string SchoolIsRequired = "The school is required";
        public const string CiteKeyIsRequired = "The cite key is required";
        public const string JournalIsRequired = "Journal is required";
        public const string YearIsRequired = "Year is required";
        public const string InstitutionIsRequired = "Institution is required";
        public const string TitleIsRequired = "Title is required";
        public const string AuthorsIsRequired = "The author is required";
        public const string CiteKeyNotUnique = "The cite key provided is not unique. Please try a different cite key.";
        public const string FileContentTypeError = "The file type is not text/plain. Please upload only plain text files.";
        public const string FileIsNull = "File cannot be null";
        public const string FileContentLengthError = "No file was selected or the file length is too short. Please choose a file or upload a file with a length greater than 0.";
        public const string CannotReadStream = "Cannot read the stream - please try again";
        public const string IncorrectFileFormat = "The file is not an appropriate format";
    }
}