using CloneBookingAPI.Enums;

namespace CloneBookingAPI.ViewModels
{
    public class SortViewModel
    {
        public SortState PriceSort { get; private set; } // значение для сортировки по имени
        public SortState StarsSort { get; private set; }    // значение для сортировки по возрасту
        public SortState GradeReviewedSort { get; private set; }   // значение для сортировки по компании
        public SortState Current { get; private set; }     // текущее значение сортировки

        public SortViewModel(SortState sortOrder)
        {
            PriceSort = sortOrder == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;
            StarsSort = sortOrder == SortState.StarsAsc ? SortState.StarsDesc : SortState.StarsAsc;
            GradeReviewedSort = sortOrder == SortState.WorstReviewed ? SortState.BestReviewed : SortState.WorstReviewed;
            Current = sortOrder;
        }
    }
}
