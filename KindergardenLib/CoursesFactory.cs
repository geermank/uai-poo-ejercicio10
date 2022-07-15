using System.Collections.Generic;

namespace KindergardenLib
{
    public class CoursesFactory
    {

        public List<Course> CreateCourses()
        {
            Course lacatarios = new Course("Lactarios", 0, 9, 12, 2, 0f);
            Course deambuladores = new Course("Deambuladores", 10, 18, 15, 2, 0f);
            Course deambuladores2 = new Course("Deambuladores II", 19, 24, 15, 1, 0f);
            Course salaDe2 = new Course("Sala de 2", 24, 35, 18, 1, 0f);
            Course salaDe3 = new Course("Sala de 3", 36, 47, 20, 1, 0f);
            return new List<Course>()
            {
                lacatarios,
                deambuladores,
                deambuladores2,
                salaDe2,
                salaDe3
            };
        }
    }
}