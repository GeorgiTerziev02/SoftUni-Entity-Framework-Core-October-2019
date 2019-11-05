﻿namespace EFCodeFirstTest.Data
{
    public static class DataValidations
    {
        public static class Student
        {
            public const int NameMaxLength = 30;
        }

        public static class Course
        {
            public const int NameMaxLength = 100;
            public const int DescriptionMaxLength = 5000;
        }
    }
}
