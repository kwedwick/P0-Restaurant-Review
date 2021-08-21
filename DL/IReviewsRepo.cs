using Models;
using System.Collections.Generic;

namespace DL
{
    public interface IReviewsRepo
    {
        List<Review> GetAllReviews();
    }
}