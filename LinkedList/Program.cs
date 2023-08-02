using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinkedList
{
    internal class Program
    {
        static string answer, workerName, prevName;
        static double workerSalary, prevSalary;
        static int numAdd, prevNum;
        static Node<int> newNodeInt;
        static Node<Worker> newNode;
        static Worker w;
        // 1. אורך הרשימה
        static int NumberOfNodes<T>(Node<T> head)
        {
            // הגדרת מונה
            int counter = 0;
            // ריצה על הרשימה
            while (head != null)
            {
                //העלאת המונה ב1 וקידום הפוינטר
                counter++;
                head = head.GetNext();
            }
            //החזרת המונה
            return counter;
        }
        // 2. הדפסת הרשימה
        public static void PrintList<T>(Node<T> head)
        {
            // ריצה על כל הרשימה
            while (head != null)
            {
                //הדפסת הערך הנוכחי ברשימה וקידום הפוינטר
                Console.WriteLine($"{head.GetValue()}");
                head = head.GetNext();
            }
        }
        //3. הוספת חוליה חדשה לראש הרשימה
        public static Node<T> AddToStartList<T>(Node<T> head, Node<T> newNode)
        {
            //אם הרשימה לא ריקה - הגדרת ראש הרשימה הנוכחית להיות הנקסט של החוליה החדשה
            //ובכך מוגדר שהחוליה החדשה היא ראש הרשימה
            if (head != null)
                newNode.SetNext(head);
            //החזרת הרשימה החדשה
            return newNode;
        }
        //4. הוספת חוליה לסוף הרשימה
        public static void AddToEndOfList<T>(Node<T> head, T value)
        {
            //יצירת חוליה חדשה עם הערך שנשלח
            Node<T> last = new Node<T>(value);
            //מעבר על כל הרשימה עד שמגיעים לסוף
            while (head != null)
            {
                //אם אין נקסט לחוליה הנוכחית
                if (!head.HasNext())
                {
                    //הגדרת הנקסט של החוליה הנוכחית להיות החוליה החדשה שיצרנו
                    //ובכך מוגדר שהחוליה האחרונה היא החוליה החדשה
                    head.SetNext(last);
                    break;
                }
                //התקדמות ברשימה
                head = head.GetNext();
            }
        }
        //5. הוספת חוליה לרשימה אחרי חוליה מסוימת
        static void AddAfter<T>(Node<T> prev, T value)
        {
            //יצירת חוליה חדשה עם הערך שנשלח
            Node<T> node = new Node<T>(value);
            //אם החוליה שעליה מצביעים היא לא מוגדרת
            if (prev == null)
            {
                //הגדרת החוליה שנוצרה להיות החלויה הלא מוגדרת
                prev = node;
                //סיום הפעולה
                return;
            }
            //קישור החוליה החדשה לשאר הרשימה
            node.SetNext(prev.GetNext());
            //קישור שארית הרשימה לחוליה החדשה
            prev.SetNext(node);
        }
        //6. מחיקת חוליה מראש הרשימה
        public static Node<T> DeleteStartList<T>(Node<T> head)
        {
            //מצביע זמני על ראש הרשימה
            Node<T> temp = head;
            //הגדרת ראש הרשימה להיות הנקסט של ראש הרשימה הקודם
            head = head.GetNext();
            //הגדרת הנקסט של המצביע הזמני להיות ריק
            temp.SetNext(null);
            //החזרת הרשימה החדשה ללא הראש
            return head;
        }
        //7. מחיקת ערך בסוף הרשימה

        static void DeleteLast<T>(Node<T> list)
        {
            //אם הרשימה ריקה אל תעשה כלום
            if (list == null)
                return;
            //אם אין נקסט לרשימה - כלומר הרשימה היא חוליה אחת בלבד
            //הגדר אותה להיות ריקה
            if (!list.HasNext())
            {
                list = null;
                return;
            }
            //אם לרשימה יש לפחות 2 חוליות
            while (list.GetNext().GetNext() != null)
            {
                //התקדם עד לחוליה הלפני אחרונה
                list = list.GetNext();
            }
            //הגדר את הנקסט של החוליה הלפני אחרונה להיות ריקה
            //וכך נמחקה החוליה האחרונה
            list.SetNext(null);
        }
        //8.מחיקת ערך באמצע הרשימה
        static void DeleteAfter<T>(Node<T> prev)
        {
            //אם הקודם מוגדר ויש לו נקסט
            if (prev != null && prev.HasNext())
            {
                //שמור את החוליה הנוכחית בפוינטר
                Node<T> temp = prev.GetNext();
                //הגדר את הנקסט של הנוכחית להיות הנקסט של הנקסט (קפיצה של 2
                prev.SetNext(temp.GetNext());
                //הגדר את הנקסט של הנוכחי להיות ריק
                temp.SetNext(null);
            }
        }
        //9. החזרת הערך בראש הרשימה
        static T GetHeadValue<T>(Node<T> head)
        {
            return head.GetValue();
        }
        //10. החזרת הערך בסוף הרשימה
        static T GetLastNodeValue<T>(Node<T> head)
        {
            //מעבר על כל הרשימה עד שמגיעים לאחרון
            while (head.HasNext())
            {
                head = head.GetNext();
            }
            //החזרת הערך של האחרון
            return head.GetValue();

        }
        //11. החזרת ערך לפי מיקום ברשימה
        static T GetValueByIndex<T>(Node<T> head, int index)
        {
            if (index > head.NumberOfNodes())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Index out of range!");
                return head.GetValue();
            }
            //שמירת מונה החל מ1 - כלומר האינדקס הראשון הוא 1 ולא 0 בשונה ממערך
            int counter = 1;
            //נתקדם ברשימה עד שנגיע לאינדקס
            while (counter != index)
            {
                head = head.GetNext();
                counter++;
            }
            //נחזיר את הערך של הרשימה איפה שעצרנו
            return head.GetValue();
        }
        //12. בדיקה האם ערך קיים ברשימה 
        public static bool Contains<T>(Node<T> head, T value)
        {
            //אם הרשימה ריקה החזר false
            if (head == null)
                return false;
            //מעבר על כל הרשימה
            while (head != null)
            {
                //אם הערך הנוכחי שווה לערך שאותו מחפשים החזר true
                if (head.GetValue().Equals(value))
                    return true;
                head = head.GetNext();
            }
            //במידה ולא נמצא החזר false
            return false;
        }
        //13. בדיקה האם הרשימה מעגלית - האחרון מצביע לראשון
        static bool IsCircular<T>(Node<T> head)
        {
            // אם הרשימה ריקה החזר false
            if (head == null)
            {
                return false;
            }
            //שני מצביעים - איטי ומהיר
            Node<T> slow = head;
            Node<T> fast = head;
            
            //כל עוד המצביע המהיר מוגדר ויש לו נקסט
            while (fast != null && fast.HasNext())
            {
                // מקדמים את המצביע המהיר ב2 ואת האיטי ב1
                slow = slow.GetNext();
                fast = fast.GetNext().GetNext();

                //אם המהיר עשה סיבוב והדביק את האיטי
                //סימן שהרשימה מעגלית
                if (slow == fast)
                {
                    return true;
                }
            }
            // אם המצביע המהיר הגיע לסוף הרשימה סימן שהיא לא מעגלית
            return false;
        }
        //14. מחיקת כפילויות מהרשימה והחזרתה
        static Node<T> WithoutDuplicate<T>(Node<T> node)
        {
            //אם הרשימה ריקה החזר null
            if (node == null)
                return null;
            //צור רשימה חדשה עם הערך של ראש הרשימה שנשלחה
            Node<T> newList = new Node<T>(node.GetValue());
            //הגדר פוינטר לרשימה החדשה
            Node<T> temp = newList;
            //התקדם לחוליה הבאה ברשימה שנשלחה
            node = node.GetNext();
            //מעבר על הרשימה שנשלחה
            while (node != null)
            {
                //אם הרשימה החדשה לא מכילה את הערך הנוכחי (שימוש במתודה Contains
                //שכתבנו מקודם)
                if (!Contains(newList, node.GetValue()))
                {
                    //הוספת הערך הנוכחי לרשימה החדשה
                    Node<T> newNode = new Node<T>(node.GetValue());
                    temp.SetNext(newNode);
                    temp = temp.GetNext();
                }
                node = node.GetNext();
            }
            //החזרת הרשימה החדשה
            return newList;

        }
        //15. שכפול הרשימה (העתקה עמוקה
        static Node<T> DeepCopyList<T>(Node<T> node) where T : ICloneable
        {
            //זהה לחלוטין למתודה הקודמת - רק שלא בודקים אם הרשימה החדשה מכילה ערכים קודמים
            if (node == null)
                return null;
            Node<T> newList = new Node<T>((T)(node.GetValue()).Clone());
            Node<T> temp = newList;
            node = node.GetNext();
            while (node != null)
            {
                Node<T> newNode = new Node<T>(node.GetValue());
                temp.SetNext(newNode);
                temp = temp.GetNext();
                node = node.GetNext();
            }
            return newList;
        }
        //16. היפוך הרשימה
        static Node<T> ReversList<T>(Node<T> head)
        {
            //הגדרת מצביע לחוליה הקודמת
            Node<T> prev = null;
            //הגדרת מצביע לחוליה הנוכחית
            Node<T> current = head;
            //הגדרת מצביע לחוליה הבאה
            Node<T> next = null;
            //ריצה על הרשימה שנשלחה
            while (current != null)
            {
                //הגדרת החוליה הבאה להיות הנקסט של הנוכחית
                next = current.GetNext();
                //הגדרת הנקסט של הנוכחית להיות החוליה הקודמת
                current.SetNext(prev);
                //הגדרת החוליה הקודמת להיות הנוכחית
                prev = current;
                //הגדרת החוליה הנוכחית להיות החוליה הבאה
                current = next;
            }
            //בסוף - הגדרת ראש הרשימה שנשלחה להיות החוליה האחרונה ברשימה
            head = prev;
            //החזרת הרשימה לאחר השינויים
            return head;

        }

        //16. גרסה שנייה - שימוש בref
        static void ReversList<T>(ref Node<T> head)
        {
            //זהה לחלוטין לקודם - רק שכאן מתייחסים לכתובות ולא לחוליות עצמן, ולכן לא מחזירים את הרשימה בסוף
            Node<T> prev = null;
            Node<T> current = head;
            Node<T> next = null;
            while (current != null)
            {
                next = current.GetNext();
                current.SetNext(prev);
                prev = current;
                current = next;
            }
            head = prev;
        }
        // 17. מיון הרשימה בסדר עולה
        //צריך שהערך שנשלח יהיה ניתן להשוואה
        public static void SortList<T>(Node<T> head) where T : IComparable<T>
        {
            // אם הרשימה ריקה או שההבא ברשימה ריק - אל תעשה כלום
            if (head == null || head.GetNext() == null) return;

            // שימוש במיון בועות
            // משווים את האלמנט השמאלי ביותר אל מול ההבא אחריו
            // אם השמאלי ביותר גדול מהבא - החלף ביניהם
            // בצע זאת עד שכל הזוגות לא צריכים החלפה יותר

            //הגדרת מצביע זמני על הרשימה
            Node<T> outer = head;
            //קידום הרשימה
            while (outer.GetNext() != null)
            {
                //הגדרת מצביע פנימי - לצורך השוואתו מול ההבא שלו
                Node<T> inner = head;
                // ריצה על הנוכחי והנקסט שלו
                while (inner.GetNext() != null)
                {
                    //אם הערך של הנוכחי גדול מהערך של ההבא אחריו
                    if (inner.GetValue().CompareTo(inner.GetNext().GetValue()) > 0)
                    {
                        //החלף את הערכים
                        T temp = inner.GetValue();
                        inner.SetValue(inner.GetNext().GetValue());
                        inner.GetNext().SetValue(temp);
                    }
                    //התקדם לזוג הבא
                    inner = inner.GetNext();
                }
                //לאחר מיון כל הזוגות - התקדם ברשימה וחזור על ההשוואות
                outer = outer.GetNext();
            }
        }
        // 18. השוואת ערכי הרשימות ואורכן
        static bool Equals<T>(Node<T> list1, Node<T> list2)
        {
            // אם האורכים של שתי הרשימות שונים החזר false
            if (list1.NumberOfNodes() != list2.NumberOfNodes()) return false;
            // אחרת מעבר על אחת הרשימות 
            while (list1 != null)
            {
                // בדיקת הערכים של הרשימות - אם הם לא זהים אחד לשני החזר false
                if (!list1.GetValue().Equals(list2.GetValue())) return false;
                //התקדמות בשתי הרשימות
                list1 = list1.GetNext();
                list2 = list2.GetNext();
            }
            //במידה והכל היה שווה אחד לשני החזר true
            return true;
        }
        // 19. מיזוג הרשימות
        static Node<T> MergeList<T>(Node<T> list1, Node<T> list2)
        {
            // הגדרת הנקסט של סוף הרשימה הראשונה, להיות ראש הרשימה השנייה
            Node<T> temp = list1;
            while (temp.HasNext())
            {
                temp = temp.GetNext();
            }
            temp.SetNext(list2);
            return list1;
        }
        // 20. מיזוג הרשימות ללא כפילויות
        static Node<T> MergeListNoDup<T>(Node<T> list1, Node<T> list2)
        {
            // שימוש חוזר במתודות שכתבנו
            // יצירת רשימה ממוזגת
            Node<T> mergedList = MergeList(list1, list2);
            // והחזרתה ללא כפילויות
            return WithoutDuplicate<T>(mergedList);
        }
        // 21. חיתוך רשימות
        static Node<T> CrossLists<T>(Node<T> list1, Node<T> list2)
        {
            // יצירת רשימה חדשה שמכילה את הערך הראשון של הרשימה הראשונה
            Node<T> newList = new Node<T>(list1.GetValue());
            //הגדרת פוינטר לרשימה הראשונה
            Node<T> temp = list1;
            //ריצה על הרשימה הראשונה באמצעות הפוינטר
            while (temp != null)
            {
                //אם הרשימה השנייה מכילה את הערך הנוכחי של הרשימה הראשונה
                //הוסף את הערך הזה לרשימה החדשה שיצרנו והתקדם לחוליה הבאה ברשימה
                if (Contains(list2, temp.GetValue()))
                {
                    AddToEndOfList(newList, temp.GetValue());
                    temp = temp.GetNext();
                    continue;
                }
                //במידה והערך לא נמצא ברשימה - התקדם 
                temp = temp.GetNext();
            }
            // לאחר הריצה, נבדוק שהערך הראשון של הרשימה הראשונה לא נמצא ברשימה השנייה
            //אם הוא לא קיים, נצטרך למחוק אותו מראש הרשימה
            if (!Contains(list2, list1.GetValue()))
            {
                //נחזיר את הרשימה החדשה לאחר מחיקת ראש הרשימה
                return DeleteStartList(newList);
            }
            // אחרת נחזיר את הרשימה החדשה כמו שהיא נוצרה
            return WithoutDuplicate(newList);
        }
        // 23. 
        static void PrintStudentsAverage(Node<Student> studentList)
        {
            // הגדרת סכום ציונים
            double sum = 0;
            //ריצה על רשימת הסטודנטים
            while (studentList != null)
            {
                //שמירת רשימת הקורסים של הסטודנט הנוכחי
                Node<Course> currentCourses = studentList.GetValue().GetCourseList();
                //שמירת אורך רשימת הקורסים הזאת
                int currentCourseAmount = currentCourses.NumberOfNodes();
                //ריצה על הרשימה של הקורסים
                while (currentCourses != null)
                {
                    //הוספת הציון של הקורס הנוכחי לסכום
                    sum += currentCourses.GetValue().GetGrade();
                    //התקדמות לקורס הבא
                    currentCourses = currentCourses.GetNext();
                }
                //הדפסת הממוצע של הסטודנט הנוכחי
                Console.WriteLine($"The average for {studentList.GetValue().GetNameStudent()} is {sum / currentCourseAmount}");
                //איפוס הסכום כי עוברים לסטודנט הבא
                sum = 0;
                studentList = studentList.GetNext();
            }
        }
        // 24. החזרת רשימה של סטודנטים מצטיינים - שימוש במתודה פנימית של המחלקה סטודנט למציאת ממוצע
        static Node<Student> ExcellentStudentsList(Node<Student[]> classList)
        {
            // יצירת רשימה חדשה עם סטודנט לא אמיתי לצורך עבודה על הרשימה והחזרתה בסוף הפעולה
            Node<Student> excellentStudentsList = new Node<Student>(new Student("", new Node<Course>(new Course(999, 999))));
            //הגדרת משתנים לממוצע המקסימלי והמיקום שלו
            double maxAverageInClass = int.MinValue;
            int maxAverageInClassIndex = 0;
            //ריצה על הרשימה של מערכי הסטודנטים
            while (classList != null)
            {
                //ריצה על המערך הנוכחי
                for (int i = 0; i < classList.GetValue().Length; i++)
                {
                    //שמירת הסטודנט הנוכחי במשתנה
                    Student currentStudent = classList.GetValue()[i];
                    //שימוש במתודה להחזרת הממוצע של הסטודנט ובדיקה אם הוא גבוה מהממוצע המקסימלי
                    if (currentStudent.GetAvg() > maxAverageInClass)
                    {
                        //במידה וכן - הגדרת הממוצע המקסימלי להיות הממוצע הנוכחי
                        maxAverageInClass = currentStudent.GetAvg();
                        //הגדרת המיקום של הממוצע המקסימלי במערך
                        maxAverageInClassIndex = i;
                    }
                }
                //הוספת הסטודנט במיקום של הממוצע המקסימלי לסוף הרשימה שיצרנו
                AddToEndOfList(excellentStudentsList, classList.GetValue()[maxAverageInClassIndex]);
                //איפוס הממוצע המקסימלי כי עוברים לכיתה הבאה ברשימה
                maxAverageInClass = int.MinValue;
                maxAverageInClassIndex = 0;
                classList = classList.GetNext();
            }
            // החזרת הרשימה החדשה ללא הערך הראשון - שהוא הסטודנט המזויף
            return DeleteStartList(excellentStudentsList);
        }
        // 25. Array of students with the most fails in each class
        static Student[] GetStudentsWithMostFails(Node<Student>[] classrooms)
        {
            // create an array of students with the length: amount of classrooms
            Student[] studentsWithMostFails = new Student[classrooms.Length];
            // loop over each classroom
            for (int i = 0; i < classrooms.Length; i++)
            {
                // save current classroom in a list of students
                Node<Student> currentClassroom = classrooms[i];
                // save the student with most fails as a null reference (if there will be a student with fails, it will be replaced by that student)
                Student studentWithMostFails = null;
                // set a variable for most fails in classroom
                int mostFails = 0;
                // loop over each student in current classroom
                while (currentClassroom != null)
                {
                    // save current student in variable
                    Student currentStudent = currentClassroom.GetValue();
                    // set counter for fails for this student
                    int numFails = 0;
                    // save the course list of the current student
                    Node<Course> currentCourse = currentStudent.GetCourseList();
                    // loop over the course list of current student
                    while (currentCourse != null)
                    {
                        // if current course's grade is lower than 55 - increment fails counter
                        if (currentCourse.GetValue().GetGrade() < 55)
                        {
                            numFails++;
                        }
                        currentCourse = currentCourse.GetNext();
                    }
                    // after looping over the courses, if the fails number is higher than the max fails, set the max fails to the current fails counter
                    if (numFails > mostFails)
                    {
                        mostFails = numFails;
                        // also set the current student to the student with most fails
                        studentWithMostFails = currentStudent;
                    }
                    // move on to the next classroom
                    currentClassroom = currentClassroom.GetNext();
                }
                // set the student with most fails to the current element in the array we will return
                studentsWithMostFails[i] = studentWithMostFails;
            }
            // this array is likely to have nulls in it. so we will return an array without nulls
            return RemoveNulls(studentsWithMostFails);
        }
        public static Student[] StudentFaild(Node<Student>[] allClassStudent)
        {
            Student[] worstStudentArr = new Student[allClassStudent.Length];
            int index = 0;
            int maxFailedCounter = int.MinValue;
            Student worstStudent = null;
            for (int i = 0; i < allClassStudent.Length; i++)
            {
                Node<Student> oneClass = allClassStudent[i];
                while (oneClass != null)
                {
                    int counterFaildCourses = 0;
                    Student student = oneClass.GetValue();
                    Node<Course> allCourses = student.GetCourseList();
                    while (allCourses != null)
                    {
                        Course course = allCourses.GetValue();
                        if (course.GetGrade() < 55)
                        {
                            counterFaildCourses++;
                        }
                        allCourses = allCourses.GetNext();
                    }
                    if (maxFailedCounter < counterFaildCourses)
                    {
                        maxFailedCounter = counterFaildCourses;
                        worstStudent = student;
                    }
                    oneClass = oneClass.GetNext();
                }
                if (worstStudent != null)
                    worstStudentArr[index++] = worstStudent;
            }
            return worstStudentArr;
        }
        // return array without nulls
        static Student[] RemoveNulls(Student[] arr)
        {
            int nullCount = 0;
            foreach (Student s in arr)
            {
                if (s == null)
                {
                    nullCount++;
                }
            }

            Student[] newArr = new Student[arr.Length - nullCount];
            int index = 0;
            foreach (Student s in arr)
            {
                if (s != null)
                {
                    newArr[index] = s;
                    index++;
                }
            }

            return newArr;
        }

        static void Main(string[] args)
        {
            // בדיקת מעגליות - worker
            /*   Node<Worker> nw3 = new Node<Worker>(new Worker("Tayush", 6000));
               Node<Worker> nw2 = new Node<Worker>(new Worker("Michal", 6000), nw3);
               Node<Worker> nw1 = new Node<Worker>(new Worker("Daniel", 6000), nw2);
               nw3.SetNext(nw1);
               Console.WriteLine(IsCircular<Worker>(nw3));*/
            // בדיקת מעגליות - int
/*            Node<int> n3 = new Node<int>(30);
            Node<int> n2 = new Node<int>(20,n3);
            Node<int> n1 = new Node<int>(10, n2);
            n3.SetNext(n1);
            Console.WriteLine(IsCircular<int>(n1));*/

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Hello! Do you want to see the results of the last 3 questions? (Questions 23-25)");
            Console.WriteLine("Y/N");
            string choice = Console.ReadLine();
            if (choice == "Y" || choice == "y")
            {
                TestLast3Questions();
                ShowMenu();
            }
            else if (choice == "N" || choice == "n")
            {
                ShowMenu();
            }
            else
            {
                Console.WriteLine("Illegal choice, closing the program!");
                return;
            }
        }
        //פונקציה לבדיקת השאלות האחרונות ביחד
        private static void TestLast3Questions()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Printing student's averages: \n");
            Console.ForegroundColor = ConsoleColor.White;
            TestQuestion23(); // printing student's averages
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nPrinting excellent students in each class: \n");
            Console.ForegroundColor = ConsoleColor.White;
            TestQuestion24(); // printing excellent students 
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nPrinting worst students in each class:\n");
            Console.ForegroundColor = ConsoleColor.White;
            TestQuestion25(); // printing worst students
        }
        //פונקציה לבדיקת שאלה 25
        private static void TestQuestion25()
        {
            // course list
            Course c1 = new Course(1, 30);
            Course c2 = new Course(2, 75);
            Course c3 = new Course(3, 95);
            Course c4 = new Course(4, 100);
            Course c5 = new Course(5, 45);
            Course c6 = new Course(6, 45);
            Course c7 = new Course(7, 55);
            Course c8 = new Course(8, 40);
            // 4 lists with 2 courses each
            Node<Course> nc8 = new Node<Course>(c8);
            Node<Course> nc7 = new Node<Course>(c7);
            Node<Course> nc6 = new Node<Course>(c6, nc7);
            Node<Course> nc5 = new Node<Course>(c5, nc8);
            Node<Course> nc4 = new Node<Course>(c4);
            Node<Course> nc3 = new Node<Course>(c3);
            Node<Course> nc2 = new Node<Course>(c2, nc4);
            Node<Course> nc1 = new Node<Course>(c1, nc3);

            //students
            Student s1 = new Student("Michal Goot", nc1);
            Student s2 = new Student("Tayush prince", nc2);
            Student s3 = new Student("Ido zalame", nc5);
            Student s4 = new Student("Daniel Goldfarb", nc6);
            Student s5 = new Student("Alon Ck", nc2);
            Student s6 = new Student("Ofek B", nc1);

            // 3 students lists
            Node<Student> ns6 = new Node<Student>(s6);
            Node<Student> ns5 = new Node<Student>(s5, ns6);
            Node<Student> ns4 = new Node<Student>(s4);
            Node<Student> ns3 = new Node<Student>(s3, ns4);
            Node<Student> ns2 = new Node<Student>(s2);
            Node<Student> ns1 = new Node<Student>(s1, ns2);

            Node<Student>[] classrooms = new Node<Student>[3];
            classrooms[0] = ns1;
            classrooms[1] = ns3;
            classrooms[2] = ns5;

            Student[] resultArr = new Student[classrooms.Length];
            resultArr = GetStudentsWithMostFails(classrooms);
            foreach (Student student in resultArr)
            {
                Console.WriteLine(student);
            }

        }
        //פונקציה לבדיקת שאלה 24
        private static void TestQuestion24()
        {
            // course list
            Course c1 = new Course(1, 30);
            Course c2 = new Course(2, 75);
            Course c3 = new Course(3, 95);
            Course c4 = new Course(4, 100);
            Course c5 = new Course(5, 45);
            Course c6 = new Course(6, 45);
            Course c7 = new Course(7, 55);
            Course c8 = new Course(8, 40);
            // 4 lists with 2 courses each
            Node<Course> nc8 = new Node<Course>(c8);
            Node<Course> nc7 = new Node<Course>(c7);
            Node<Course> nc6 = new Node<Course>(c6, nc7);
            Node<Course> nc5 = new Node<Course>(c5, nc8);
            Node<Course> nc4 = new Node<Course>(c4);
            Node<Course> nc3 = new Node<Course>(c3);
            Node<Course> nc2 = new Node<Course>(c2, nc4);
            Node<Course> nc1 = new Node<Course>(c1, nc3);

            //students
            Student s1 = new Student("Michal Goot", nc1);
            Student s2 = new Student("Tayush prince", nc2);
            Student s3 = new Student("Ido zalame", nc5);
            Student s4 = new Student("Daniel Goldfarb", nc6);
            Student s5 = new Student("Alon Ck", nc2);
            Student s6 = new Student("Ofek B", nc1);

            // create students array
            Student[] studentArr1 = new Student[2];
            studentArr1[0] = s1;
            studentArr1[1] = s2;
            Student[] studentArr2 = new Student[2];
            studentArr2[0] = s3;
            studentArr2[1] = s4;
            Student[] studentArr3 = new Student[2];
            studentArr3[0] = s5;
            studentArr3[1] = s6;

            Node<Student[]> class3 = new Node<Student[]>(studentArr1);
            Node<Student[]> class2 = new Node<Student[]>(studentArr2, class3);
            Node<Student[]> class1 = new Node<Student[]>(studentArr3, class2);

            Node<Student> resArr = ExcellentStudentsList(class1);
            Console.WriteLine(resArr);
        }
        //פונקציה לבדיקת שאלה 23
        private static void TestQuestion23()
        {
            // course list
            Course c1 = new Course(1, 30);
            Course c2 = new Course(2, 75);
            Course c3 = new Course(3, 95);
            Course c4 = new Course(4, 100);
            Course c5 = new Course(5, 45);
            Course c6 = new Course(6, 45);
            Course c7 = new Course(7, 55);
            Course c8 = new Course(8, 40);
            // 4 lists with 2 courses each
            Node<Course> nc8 = new Node<Course>(c8);
            Node<Course> nc7 = new Node<Course>(c7);
            Node<Course> nc6 = new Node<Course>(c6, nc7);
            Node<Course> nc5 = new Node<Course>(c5, nc8);
            Node<Course> nc4 = new Node<Course>(c4);
            Node<Course> nc3 = new Node<Course>(c3);
            Node<Course> nc2 = new Node<Course>(c2, nc4);
            Node<Course> nc1 = new Node<Course>(c1, nc3);

            //students
            Student s1 = new Student("Michal Goot", nc1);
            Student s2 = new Student("Tayush prince", nc2);
            Student s3 = new Student("Ido zalame", nc5);
            Student s4 = new Student("Daniel Goldfarb", nc6);

            //create a students list
            Node<Student> ns4 = new Node<Student>(s4);
            Node<Student> ns3 = new Node<Student>(s3, ns4);
            Node<Student> ns2 = new Node<Student>(s2, ns3);
            Node<Student> ns1 = new Node<Student>(s1, ns2);

            PrintStudentsAverage(ns1);
        }
        //פונקציה להצגת תפריט ראשי
        private static void ShowMenu()
        {
            // תפריט ראשי - צבע טקסט לבן
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($@"Welcome! Please choose the class to work with
1. Worker
2. Integer
3. Exit");
            string choice = Console.ReadLine();
            switch (choice)
            {
                // מעבר לתפריט עובדים
                case "1":
                    Node<Worker> workerList = CreateDefaultWorkerList();
                    ShowMenuForWorker(workerList);
                    break;
                case "2":
                    Node<int> intList = CreateDefaultIntList();
                    ShowMenuForInt(intList);
                    break;
                case "3":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(@"
░██████╗░░█████╗░░█████╗░██████╗░██████╗░██╗░░░██╗███████╗
██╔════╝░██╔══██╗██╔══██╗██╔══██╗██╔══██╗╚██╗░██╔╝██╔════╝
██║░░██╗░██║░░██║██║░░██║██║░░██║██████╦╝░╚████╔╝░█████╗░░
██║░░╚██╗██║░░██║██║░░██║██║░░██║██╔══██╗░░╚██╔╝░░██╔══╝░░
╚██████╔╝╚█████╔╝╚█████╔╝██████╔╝██████╦╝░░░██║░░░███████╗
░╚═════╝░░╚════╝░░╚════╝░╚═════╝░╚═════╝░░░░╚═╝░░░╚══════╝");
                    break;
                default:
                    Console.WriteLine("Illegal choice! Try again");
                    choice = Console.ReadLine();
                    break;
            }
            return;
        }
        //פונקציה להצגת תפריט מספרים
        private static void ShowMenuForInt(Node<int> intList)
        {
            Console.Clear();
            //תפריט מספרים - צבע ורוד
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("This is your list:");
            Console.WriteLine(intList);
            Console.WriteLine($@"Hello! Select your choice:
1. Find a value in list
2. Check something in list
3. Modify list
4. Work with 2 lists
5. Back");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ShowMenuForFindValueInIntList(intList);
                    break;
                case "2":
                    ShowMenuForCheckSomethingInIntList(intList);
                    break;
                case "3":
                    ShowMenuForModifyIntList(intList);
                    break;
                case "4":
                    Node<int> secondIntList = CreateSecondIntList();
                    ShowMenuFor2IntLists(intList, secondIntList);
                    break;
                case "5":
                    ShowMenu();
                    break;
                default:
                    Console.WriteLine("Illegal choice! Try again");
                    choice = Console.ReadLine();
                    break;
            }
        }
        //פונקציה להצגת תפריט עבודה על 2 רשימות מספרים
        private static void ShowMenuFor2IntLists(Node<int> intList, Node<int> secondIntList)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Your first list is:");
            Console.WriteLine(intList);
            Console.WriteLine();
            Console.WriteLine("Your second list is:");
            Console.WriteLine(secondIntList);
            Console.WriteLine($@"Choose an option:
1. Check if both lists are equal
2. Merge lists
3. Merge lists without duplicates
4. Cross lists
5. Back");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Both lists are equal: ");
                    Console.WriteLine(Equals(intList, secondIntList));
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuFor2IntLists(intList, secondIntList);
                    break;
                case "2":
                    Console.WriteLine("List after merging:");
                    Console.WriteLine(MergeList<int>(intList, secondIntList));
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuFor2IntLists(intList, secondIntList);
                    break;
                case "3":
                    Console.WriteLine("List after merging without duplicates:");
                    Console.WriteLine(MergeListNoDup<int>(intList, secondIntList));
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuFor2IntLists(intList, secondIntList);
                    break;
                case "4":
                    Console.WriteLine("List after crossing:");
                    Console.WriteLine(CrossLists<int>(intList, secondIntList));
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuFor2IntLists(intList, secondIntList);
                    break;
                case "5":
                    ShowMenuForInt(intList);
                    break;
                default:
                    Console.WriteLine("Illegal choice! Try again");
                    choice = Console.ReadLine();
                    break;
            }
        }
        //פונקציה ליצירת רשימה שנייה של מספרים
        private static Node<int> CreateSecondIntList()
        {
            int num5 = 35;
            int num6 = 40;
            int num7 = 41;
            int num8 = 27;

            Node<int> n8 = new Node<int>(num8);
            Node<int> n7 = new Node<int>(num7, n8);
            Node<int> n6 = new Node<int>(num6, n7);
            Node<int> n5 = new Node<int>(num5, n6);

            return n5;
        }
        //פונקציה להצגת תפריט שינוי רשימת מספרים
        private static void ShowMenuForModifyIntList(Node<int> intList)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Your list is:");
            Console.WriteLine(intList);
            Console.WriteLine($@"Choose an option:
1. Add number to start of list
2. Add number to end of list
3. Add number in middle of list
4. Delete number from start of list
5. Delete number from end of list
6. Delete number from middle of list
7. Remove duplicates from list
8. Reverse list
9. Sort list
10. Back");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Enter number:");
                    numAdd = int.Parse(Console.ReadLine());
                    newNodeInt = new Node<int>(numAdd);
                    intList = AddToStartList<int>(intList, newNodeInt);
                    Console.WriteLine($"Successfully added {numAdd} to the start of the list!");
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForModifyIntList(intList);
                    break;
                case "2":
                    Console.WriteLine("Enter number:");
                    numAdd = int.Parse(Console.ReadLine());
                    AddToEndOfList<int>(intList, numAdd);
                    Console.WriteLine($"Successfully added {numAdd} to the end of the list!");
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForModifyIntList(intList);
                    break;
                case "3":
                    Console.WriteLine("After what number do you want to add the new number?");
                    Console.WriteLine("Name:");
                    prevNum = int.Parse(Console.ReadLine());
                    Console.WriteLine($"Enter number to add after {prevNum}:");
                    numAdd = int.Parse(Console.ReadLine());
                    AddAfter<int>(GetFirstInstanceOfValue<int>(intList, prevNum), numAdd);
                    Console.WriteLine($"Successfully added {numAdd} after {prevNum} of the list!");
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForModifyIntList(intList);
                    break;
                case "4":
                    intList = DeleteStartList<int>(intList);
                    Console.WriteLine("Successfully deleted the first node of the list!");
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForModifyIntList(intList);
                    break;
                case "5":
                    DeleteLast<int>(intList);
                    Console.WriteLine("Successfully deleted the last node of the list!");
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForModifyIntList(intList);
                    break;
                case "6":
                    Console.WriteLine("After what number do you remove a node?");
                    prevNum = int.Parse(Console.ReadLine());
                    DeleteAfter<int>(GetFirstInstanceOfValue<int>(intList, prevNum));
                    Console.WriteLine($"Successfully deleted node after {prevNum}!");
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForModifyIntList(intList);
                    break;
                case "7":
                    intList = WithoutDuplicate<int>(intList);
                    Console.WriteLine("Successfully removed duplicates from the list!");
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForModifyIntList(intList);
                    break;
                case "8":
                    intList = ReversList<int>(intList);
                    Console.WriteLine("Successfully reversed the list!");
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForModifyIntList(intList);
                    break;
                case "9":
                    SortList<int>(intList);
                    Console.WriteLine("Successfully sorted the list!");
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForModifyIntList(intList);
                    break;
                case "10":
                    ShowMenuForInt(intList);
                    break;
                default:
                    Console.WriteLine("Illegal choice! Try again");
                    choice = Console.ReadLine();
                    break;
            }
        }
        //פונקציה להצגת תפריט עבור בדיקות תנאים על רשימת מספרים
        private static void ShowMenuForCheckSomethingInIntList(Node<int> intList)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Your list is:");
            Console.WriteLine(intList);
            Console.WriteLine($@"Choose an option:
1. Check if a value exists in list
2. Check if list is circular
3. Back");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Enter number to check:");
                    int num = int.Parse(Console.ReadLine());
                    
                    Console.WriteLine($"The value exists in the list: {Contains<int>(intList, num)}");
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForCheckSomethingInIntList(intList);
                    break;
                case "2":
                    Console.WriteLine($"List is circular: {IsCircular<int>(intList)}");
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForCheckSomethingInIntList(intList);
                    break;
                case "3":
                    ShowMenuForInt(intList);
                    break;
                default:
                    Console.WriteLine("Illegal choice! Try again");
                    choice = Console.ReadLine();
                    break;

            }
        }
        //פונקציה להצגת תפריט חיפוש ערכים ברשימת מספרים
        private static void ShowMenuForFindValueInIntList(Node<int> intList)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Your list is:");
            Console.WriteLine(intList);
            Console.WriteLine($@"Choose an option:
1. Show list length
2. Find first value
3. Find last value
4. Find value by index
5. Back");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine($"The list has {NumberOfNodes<int>(intList)} nodes.");
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForFindValueInIntList(intList);
                    break;
                case "2":
                    Console.WriteLine("First value in list:");
                    Console.WriteLine(GetHeadValue<int>(intList));
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForFindValueInIntList(intList);
                    break;
                case "3":
                    Console.WriteLine("Last value in list:");
                    Console.WriteLine(GetLastNodeValue<int>(intList));
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForFindValueInIntList(intList);
                    break;
                case "4":
                    Console.WriteLine("Enter index:");
                    int index = int.Parse(Console.ReadLine());
                    Console.WriteLine($"Value of {index}'s node: ");
                    Console.WriteLine(GetValueByIndex<int>(intList, index));
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForFindValueInIntList(intList);
                    break;
                case "5":
                    ShowMenuForInt(intList);
                    break;
                default:
                    Console.WriteLine("Illegal choice! Try again");
                    choice = Console.ReadLine();
                    break;

            }
        }
        //פונקציה ליצירת רשימת ברירת מחדל של מספרים שלמים
        private static Node<int> CreateDefaultIntList()
        {
            int num1 = 10;
            int num2 = 20;
            int num3 = 30;
            int num4 = 40;

            Node<int> n4 = new Node<int>(num4);
            Node<int> n3 = new Node<int>(num3, n4);
            Node<int> n2 = new Node<int>(num2, n3);
            Node<int> n1 = new Node<int>(num1, n2);

            return n1;
        }

        //פונקציה ליצירת רשימת ברירת מחדל של עובדים
        private static Node<Worker> CreateDefaultWorkerList()
        {
            Worker w1 = new Worker("Daniel Goldfarb", 8000);
            Worker w2 = new Worker("Michal Goot", 9000);
            Worker w3 = new Worker("Ido Zalame", 4000);

            Node<Worker> nw3 = new Node<Worker>(w3);
            Node<Worker> nw2 = new Node<Worker>(w2, nw3);
            Node<Worker> nw1 = new Node<Worker>(w1, nw2);

            return nw1;
        }
        //פונקציה להצגת תפריט עובדים
        private static void ShowMenuForWorker(Node<Worker> workerList)
        {
            Console.Clear();
            //תפריט עובדים - צבע צהוב
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("This is your list:");
            Console.WriteLine(workerList);
            Console.WriteLine($@"Hello! Select your choice:
1. Find a value in list
2. Check something in list
3. Modify list
4. Work with 2 lists
5. Back");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ShowMenuForFindValueInWorkerList(workerList);
                    break;
                case "2":
                    ShowMenuForCheckSomethingInWorkerList(workerList);
                    break;
                case "3":
                    ShowMenuForModifyWorkerList(workerList);
                    break;
                case "4":
                    Node<Worker> secondWorkerList = CreateSecondWorkerList();
                    ShowMenuFor2WorkerLists(workerList, secondWorkerList);
                    break;
                case "5":
                    ShowMenu();
                    break;
                default:
                    Console.WriteLine("Illegal choice! Try again");
                    choice = Console.ReadLine();
                    break;
            }
        }
        //פונקציה להצגת תפריט שינוי רשימת עובדים
        private static void ShowMenuForModifyWorkerList(Node<Worker> workerList)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Your list is:");
            Console.WriteLine(workerList);
            Console.WriteLine($@"Choose an option:
1. Add worker to start of list
2. Add worker to end of list
3. Add worker in middle of list
4. Delete worker from start of list
5. Delete worker from end of list
6. Delete worker from middle of list
7. Remove duplicates from list
8. Reverse list
9. Sort list
10. Back");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Enter worker name:");
                    workerName = Console.ReadLine();
                    Console.WriteLine("Enter worker salary:");
                    workerSalary = double.Parse(Console.ReadLine());
                    newNode = new Node<Worker>(new Worker(workerName, workerSalary));
                    workerList = AddToStartList<Worker>(workerList, newNode);
                    Console.WriteLine($"Successfully added {workerName} to the start of the list!");
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForModifyWorkerList(workerList);
                    break;
                case "2":
                    Console.WriteLine("Enter worker name:");
                    workerName = Console.ReadLine();
                    Console.WriteLine("Enter worker salary:");
                    workerSalary = double.Parse(Console.ReadLine());
                    AddToEndOfList<Worker>(workerList, new Worker(workerName, workerSalary));
                    Console.WriteLine($"Successfully added {workerName} to the end of the list!");
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForModifyWorkerList(workerList);
                    break;
                case "3":
                    Console.WriteLine("After who do you want to add the new worker?");
                    Console.WriteLine("Name:");
                    prevName = Console.ReadLine();
                    Console.WriteLine("Salary:");
                    prevSalary = double.Parse(Console.ReadLine());
                    w = new Worker(prevName, prevSalary);
                    Console.WriteLine($"Enter worker name to add after {prevName}:");
                    workerName = Console.ReadLine();
                    Console.WriteLine($"Enter worker salary to add after {prevName}");
                    workerSalary = double.Parse(Console.ReadLine());
                    AddAfter<Worker>(GetFirstInstanceOfValue<Worker>(workerList, w), new Worker(workerName, workerSalary));
                    Console.WriteLine($"Successfully added {workerName} after {prevName} of the list!");
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForModifyWorkerList(workerList);
                    break;
                case "4":
                    workerList = DeleteStartList<Worker>(workerList);
                    Console.WriteLine("Successfully deleted the first node of the list!");
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForModifyWorkerList(workerList);
                    break;
                case "5":
                    DeleteLast<Worker>(workerList);
                    Console.WriteLine("Successfully deleted the last node of the list!");
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForModifyWorkerList(workerList);
                    break;
                case "6":
                    Console.WriteLine("After who do you remove a worker?");
                    Console.WriteLine("Name:");
                    prevName = Console.ReadLine();
                    Console.WriteLine("Salary:");
                    prevSalary = double.Parse(Console.ReadLine());
                    w = new Worker(prevName, prevSalary);
                    DeleteAfter<Worker>(GetFirstInstanceOfValue<Worker>(workerList, w));
                    Console.WriteLine($"Successfully deleted node after {prevName}!");
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForModifyWorkerList(workerList);
                    break;
                case "7":
                    workerList = WithoutDuplicate<Worker>(workerList);
                    Console.WriteLine("Successfully removed duplicates from the list!");
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForModifyWorkerList(workerList);
                    break;
                case "8":
                    workerList = ReversList<Worker>(workerList);
                    Console.WriteLine("Successfully reversed the list!");
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForModifyWorkerList(workerList);
                    break;
                case "9":
                    SortList<Worker>(workerList);
                    Console.WriteLine("Successfully sorted the list!");
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForModifyWorkerList(workerList);
                    break;
                case "10":
                    ShowMenuForWorker(workerList);
                    break;
                default:
                    Console.WriteLine("Illegal choice! Try again");
                    choice = Console.ReadLine();
                    break;
            }
        }
        //פונקציה להחזרת המופע הראשון של ערך מסוים ברשימה
        private static Node<T> GetFirstInstanceOfValue<T>(Node<T> list, T value)
        {
            while(list != null)
            {
                if (list.GetValue().Equals(value))
                    return list;
                list = list.GetNext();
            }
            return null;
        }
        //פונקציה להצגת תפריט בדיקות תנאים על רשימת עובדים
        private static void ShowMenuForCheckSomethingInWorkerList(Node<Worker> workerList)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Your list is:");
            Console.WriteLine(workerList);
            Console.WriteLine($@"Choose an option:
1. Check if a value exists in list
2. Check if list is circular
3. Back");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Enter worker name to check");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter salary to check:");
                    double salary = double.Parse(Console.ReadLine());
                    Worker w = new Worker(name, salary);
                    Console.WriteLine($"The value exists in the list: {Contains<Worker>(workerList, w)}");
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForCheckSomethingInWorkerList(workerList);
                    break;
                case "2":
                    Console.WriteLine($"List is circular: {IsCircular<Worker>(workerList)}");
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForCheckSomethingInWorkerList(workerList);
                    break;
                case "3":
                    ShowMenuForWorker(workerList);
                    break;
                default:
                    Console.WriteLine("Illegal choice! Try again");
                    choice = Console.ReadLine();
                    break;

            }
        }
        //פונקציה להצגת תפריט חיפוש ערך ברשימת עובדים
        private static void ShowMenuForFindValueInWorkerList(Node<Worker> workerList)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Your list is:");
            Console.WriteLine(workerList);
            Console.WriteLine($@"Choose an option:
1. Show list length
2. Find first value
3. Find last value
4. Find value by index
5. Back");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine($"The list has {NumberOfNodes<Worker>(workerList)} nodes.");
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForFindValueInWorkerList(workerList);
                    break;
                case "2":
                    Console.WriteLine("First value in list:");
                    Console.WriteLine(GetHeadValue<Worker>(workerList));
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForFindValueInWorkerList(workerList);
                    break;
                case "3":
                    Console.WriteLine("Last value in list:");
                    Console.WriteLine(GetLastNodeValue<Worker>(workerList));
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForFindValueInWorkerList(workerList);
                    break;
                case "4":
                    Console.WriteLine("Enter index:");
                    int index = int.Parse(Console.ReadLine());
                    Console.WriteLine($"Value of {index}'s node: ");
                    Console.WriteLine(GetValueByIndex<Worker>(workerList, index));
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuForFindValueInWorkerList(workerList);
                    break;
                case "5":
                    ShowMenuForWorker(workerList);
                    break;
                default:
                    Console.WriteLine("Illegal choice! Try again");
                    choice = Console.ReadLine();
                    break;

            }
        }
        //פונקציה להצגת תפריט עבודה על 2 רשימות עובדים
        private static void ShowMenuFor2WorkerLists(Node<Worker> workerList, Node<Worker> secondWorkerList)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Your first list is:");
            Console.WriteLine(workerList);
            Console.WriteLine();
            Console.WriteLine("Your second list is:");
            Console.WriteLine(secondWorkerList);
            Console.WriteLine($@"Choose an option:
1. Check if both lists are equal
2. Merge lists
3. Merge lists without duplicates
4. Cross lists
5. Back");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Both lists are equal: ");
                    Console.WriteLine(Equals(workerList, secondWorkerList));
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuFor2WorkerLists(workerList, secondWorkerList);
                    break;
                case "2":
                    Console.WriteLine("List after merging:");
                    Console.WriteLine(MergeList<Worker>(workerList, secondWorkerList));
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuFor2WorkerLists(workerList, secondWorkerList);
                    break;
                case "3":
                    Console.WriteLine("List after merging without duplicates:");
                    Console.WriteLine(MergeListNoDup<Worker>(workerList, secondWorkerList));
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuFor2WorkerLists(workerList, secondWorkerList);
                    break;
                case "4":
                    Console.WriteLine("List after crossing:");
                    Console.WriteLine(CrossLists<Worker>(workerList, secondWorkerList));
                    Console.WriteLine("Clear the screen? Y/N");
                    answer = Console.ReadLine();
                    if (answer == "Y" || answer == "y")
                        Console.Clear();
                    ShowMenuFor2WorkerLists(workerList, secondWorkerList);
                    break;
                case "5":
                    ShowMenuForWorker(workerList);
                    break;
                default:
                    Console.WriteLine("Illegal choice! Try again");
                    choice = Console.ReadLine();
                    break;
            }
        }
        //פונקציה ליצירת רשימה מקושרת שנייה של עובדים
        private static Node<Worker> CreateSecondWorkerList()
        {
            Worker w4 = new Worker("Alon Kochman", 8000);
            Worker w5 = new Worker("Ofek Bublil", 8500);
            Worker w6 = new Worker("Shaked Dahari", 9500);

            Node<Worker> nw6 = new Node<Worker>(w6);
            Node<Worker> nw5 = new Node<Worker>(w5, nw6);
            Node<Worker> nw4 = new Node<Worker>(w4, nw5);

            return nw4;
        }
    }
}