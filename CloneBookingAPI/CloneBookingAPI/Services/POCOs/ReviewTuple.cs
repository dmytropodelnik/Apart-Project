namespace CloneBookingAPI.Services.POCOs
{
    public class ReviewTuple
    {
        public int ReviewCategoryId { get; set; }
        public double Grade { get; set; }

        public ReviewTuple(int reviewCategoryId, double grade)
        {
            ReviewCategoryId = reviewCategoryId;
            Grade = grade;
        }
    }
}
