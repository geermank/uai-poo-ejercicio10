namespace KindergardenLib
{
    public class DoubleShiftEnrollment : IEnrollment
    {
        public DoubleShiftEnrollment()
        {
            // empty constructor
        }

        public float CalculatePrice(float coursePrice)
        {
            return coursePrice + ((coursePrice * 75) / 100);
        }

        public override string ToString()
        {
            return "Cuota doble jornada";
        }
    }
}