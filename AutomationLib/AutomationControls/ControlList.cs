using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using AutomationLib.AutomationControls.Controls;

namespace AutomationLib.AutomationControls
{
    public class ControlList<T> : IList<T>
    {
        private readonly IList<T> _list = new List<T>();

        #region Implementation of IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<T>

        public void Add(T item)
        {
            if (item.GetType() == typeof(Control) && (item as Control).ControlType!=typeof(MenuItem))
            {
                if (_list.Count() == 0)
                    _list.Add(item);
                else
                {
                    foreach (Control cl in _list as List<Control>)
                    {
                        Control c = FindControl(cl, (item as Control).ParentHandle);
                        if (c != null)
                        {
                            c.ControlList.Add(item as Control);
                            break;
                        }
                    }
                }
            }
            else
                _list.Add(item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return _list.Remove(item);
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public bool IsReadOnly
        {
            get { return _list.IsReadOnly; }
        }

        #endregion

        

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        public T this[int index]
        {
            get { return _list[index]; }
            set { _list[index] = value; }
        }

        public T this[string text]
        {
            get 
            { 
            
                if(this.GetType() == typeof(ControlList<MenuItem>))
                {
                    foreach(MenuItem m in this._list as List<MenuItem>)
                    {
                        if (m.Text.ToUpper().Contains(text.ToUpper()))
                            return (T)Convert.ChangeType(m, typeof(T));
                    }
                    return default(T);
                }
                return default(T);
            }
            //set { _list[index] = value; }
        }


        

        private Control FindControl(Control c, IntPtr parent)
        {
            if (c.Handle == parent)
                return c;
            foreach (Control cl in c.ControlList)
            {
                c = FindControl(cl, parent);
                if (c.Handle == parent)
                    return c;
            }
            return c;
        }

        
    }

    
}
