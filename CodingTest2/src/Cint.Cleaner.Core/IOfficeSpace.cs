namespace Cint.Cleaner.Core
{
    public interface IOfficeSpace
    {
        long CleanedPlacesCount { get; }

        void SetPlaceCleaned(Point placeCleaned);
    }
}