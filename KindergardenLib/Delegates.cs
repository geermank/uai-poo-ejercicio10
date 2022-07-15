namespace KindergardenLib
{
    public delegate void delPaymentResult(OperationResult<Payment> result);

    public delegate void delCreateStudentResult(OperationResult<Student> result);
    public delegate void delEnrollStudentResult(OperationResult<Course> result);
    public delegate void delDeleteStudentResult(OperationResult<Student> result);

    public delegate void delCreateTeacher(OperationResult<Teacher> result);
    public delegate void delEnrollTeacherResult(OperationResult<Teacher> result);
}