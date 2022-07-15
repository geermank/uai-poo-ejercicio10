namespace KindergardenLib
{
    public class BasicEnrollment : IEnrollment
    {
        public BasicEnrollment()
        {
            // empty constructor
        }

        public float CalculatePrice(float coursePrice)
        {
            return coursePrice;
        }

        public override string ToString()
        {
            return "Cuota básica";
        }
    }
}