using System;
using System.Collections.Generic;

namespace LinkedList
{
    public interface ITestEvent<T>
    {
        public void TestMethod(Object o, EventArgs e);
        public void TestMethod(Object o, T e);
    }
}
