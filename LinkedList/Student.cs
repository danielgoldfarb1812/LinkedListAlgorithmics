using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    internal class Student : IComparable<Student>, ICloneable
    {
        string nameStudent;
        Node<Course> list;
        //הגדרת ממוצע לצורך השוואה
        double averageGrade;

        //בנאי שלא מקבל ערך ממוצע ציונים - זה מחושב באמצעות CalcAvg
        public Student (string nameStudent, Node<Course> list)
        {
            this.nameStudent = nameStudent;
            this.list = list;
            this.averageGrade = CalcAvg();
        }
        // מתודה פנימית לחישוב ממוצע הציונים תוך ריצה על רשימת הקורסים (לצורך השוואה בסעיף 17
        private double CalcAvg()
        {
            //הגדרת הסכום של כלל הציונים
            double sum = 0;
            //הגדרת פוינטר לרשימת הקורסים
            Node<Course> temp = list;
            //שמירת אורך הרשימה (לצורך החילוק בסוף החישוב
            int courseAmount = list.NumberOfNodes();
            //ריצה על רשימת הקורסים של הסטודנט
            while (temp != null)
            {
                //הוספת הערך של הקורס הנוכחי לסכום
                sum += temp.GetValue().GetGrade();
                //קידום הפוינטר
                temp = temp.GetNext();
            }
            //החזרת הממוצע של הציונים
            return sum / courseAmount;
        }
        public string GetNameStudent()
        {
            return nameStudent;
        }
        public Node<Course> GetCourseList()
        {
            return list;
        }
        public void SetNameStudent(string nameStudent)
        {
            this.nameStudent = nameStudent;
        }
        public void SetList(Node<Course> list)
        {
            this.list = list;
        }
        public override string ToString()
        {
            return $@"Name: {nameStudent}, Courses:
{list}
"; 
        }
        public double GetAvg()
        {
            return averageGrade;
        }
        public int CompareTo(Student other)
        {
            if (this.averageGrade > other.averageGrade) return 1;
            if (this.averageGrade < other.averageGrade) return -1;
            return 0;
        }
        public override bool Equals(object other)
        {
            if (other is Student)
            {
                return this.nameStudent.Equals(((Student)other).nameStudent) && this.list.Equals(((Student)other).list);
            }
            return false;
        }

        public object Clone()
        {
            return new Student(this.nameStudent, this.list);
        }
    }
}
