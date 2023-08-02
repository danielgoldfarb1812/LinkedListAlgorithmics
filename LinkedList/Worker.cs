using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    internal class Worker : IComparable<Worker>, ICloneable // מממש את הממשק IComparable לצורך השוואות
    {
        private string workerName;
        private double salary;

        public Worker(string workerName, double salary)
        {
            this.workerName = workerName;
            this.salary = salary;
        }
        //החזרת שם העובד
        public string GetWorker()
        {
            return workerName;
        }
        //החזרת המשכורת
        public double GetSalary()
        {
            return salary;
        }
        //הגדרת השם לעובד
        public void SetWorker(string worker)
        {
            this.workerName = worker;
        }
        //הגדרת המשכורת לעובד
        public void SetSalary(double salary)
        {
            this.salary = salary;
        }
        //הדפסת העובד
        public override string ToString()
        {
            return $@"Name: {workerName}, Salary: {salary}";
        }
        //השוואה בין המשכורות של העובדים בשיטת CompareTo
       public int CompareTo(Worker other)
        {
            if (this.salary > other.salary) return 1;
            if (this.salary < other.salary) return -1;
            return 0;
        }
        public override bool Equals(object other)
        {
            if (other is Worker)
            {
                return this.workerName == ((Worker)other).workerName && this.salary == ((Worker)other).salary;
            }
            return false;
        }

        public object Clone()
        {
            return new Worker(this.workerName, this.salary);
        }

    }
}

