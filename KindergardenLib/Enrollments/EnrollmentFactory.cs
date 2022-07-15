using System.Collections.Generic;

namespace KindergardenLib
{
    public class EnrollmentFactory
    {
        public List<IEnrollment> CreateEnrollments(float lunchPrice)
        {
            return new List<IEnrollment>()
            {
                new BasicEnrollment(),
                new DoubleShiftEnrollment(),
                new DoubleShiftWithLunchEnrollment(lunchPrice)
            };
        }
    }
}