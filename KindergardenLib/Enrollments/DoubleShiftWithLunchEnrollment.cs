namespace KindergardenLib
{
    public class DoubleShiftWithLunchEnrollment : DoubleShiftEnrollment
    {
        private float lunchPrice;

        public float LunchPrice
        {
            get { return lunchPrice; }
            set { lunchPrice = value; }
        }

        public DoubleShiftWithLunchEnrollment(float lunchPrice)
        {
            this.lunchPrice = lunchPrice;
        }

        public new float CalculatePrice(float coursePrice)
        {
            return base.CalculatePrice(coursePrice) + lunchPrice;
        }

        public override string ToString()
        {
            return "Cuota doble jornada con almuerzo";
        }
    }
}