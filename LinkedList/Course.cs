using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    internal class Course : IComparable<Course>, ICloneable//מממש את ממשק IComparable לצורך השוואות ציונים
    {
        int codeCourse;
        int grade;

        public Course (int codeCourse, int grade)
        {
            this.codeCourse = codeCourse;
            this.grade = grade;
        }

        public int GetCodeCourse()
        {
          return codeCourse;
        }
        public int GetGrade()
        {
            return grade;
        }
        public void SetCodeCourse(int codeCourse)
        {
            this.codeCourse = codeCourse;
        }
        public void SetGrade(int grade)
        {
            this.grade = grade;
        }
        public override string ToString()
        {
            return $@"Course #{codeCourse}'s grade: {grade}";
        }
        public int CompareTo(Course other)
        {
            // compare courses by grade
            if (this.grade > other.grade) return 1;
            if (this.grade < other.grade) return -1;
            return 0;
        }
        public override bool Equals(object other)
        {
            if (other is Course)
            {
                return this.codeCourse == ((Course)other).codeCourse && this.grade == ((Course)other).grade;
            }
            return false;
        }

        public object Clone()
        {
            return new Course(this.codeCourse, this.grade);
        }
    }
}
