using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    internal class Node<T>
    {
        T value;
        Node<T> next;
        
        //בנאי רק עם ערך - הנקסט יהיה ריק

        public Node(T value)
        {
            this.value = value;
            this.next = null;
        }
        //בנאי עם ערך ומצביע לנקסט
        public Node(T value, Node<T> next)
        {
            this.value = value;
            this.next = next;
        }
        //החזרת הערך של החוליה
        public T GetValue()
        {
            return value;
        }
        //החזרת החוליה הבאה ברשימה
        public Node<T> GetNext()
        {
            return next;
        }
        //בדיקה האם יש חוליה הבאה ברשימה
        public bool HasNext()
        {
            return next != null;
        }
        //הגדרת ערך לחוליה הנוכחית
        public void SetValue(T value)
        {
            this.value = value;
        }
        //הגדרת מצביע לנקסט של החוליה הנוכחית
        public void SetNext(Node<T> next)
        {
            this.next = next;
        }
        //הדפסת הרשימה החל מהחוליה הנוכחית
        public override string ToString()
        {
            return $"{this.value}-->{this.next}";
        }
        //החזרת מספר החוליות החל מהחוליה הנוכחית
        public int NumberOfNodes()
        {
            //הגדרת מצביע לחוליה הנוכחית
            Node<T> current = this;
            //הגדרת מונה
            int counter = 0;
            //כל עוד החוליה הנוכחית מוגדרת
            while(current != null)
            {
                //העלאת המונה ב1 וקידום הפוינטר לחוליה הבאה
                counter++;
                current = current.next;    
            }
            //לבסוף מחזירים את המונה
            return counter;
        }
      

    }

}
