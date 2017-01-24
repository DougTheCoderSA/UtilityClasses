using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace UtilityClasses.UnitTests
{
    [TestFixture]
    public class DelimitedListTests
    {
        public DelimitedListTests()
        {
        }

        [Test]
        public void EmptyListShouldProduceEmptyString()
        {
            string Result = "";
            DelimitedList TestList = new DelimitedList();
            Result = TestList.AsString();
            Assert.AreEqual("", Result, "Initialised DelimitedList did not produce an empty string.");
        }

        [Test]
        public void SingleStringItem()
        {
            string Result = "";
            DelimitedList TestList = new DelimitedList();

            TestList.AppendString("Item 1");

            Result = TestList.AsString();
            Assert.AreEqual("Item 1", Result);
        }

        [Test]
        public void TwoStringItems ()
        {
            string Result = "";
            DelimitedList TestList = new DelimitedList();

            TestList.AppendString("Item 1");
            TestList.AppendString("Item 2");

            Result = TestList.AsString();
            Assert.AreEqual("Item 1, Item 2", Result);
        }

        [Test]
        public void ThreeStringItems ()
        {
            string Result = "";
            DelimitedList TestList = new DelimitedList();

            TestList.AppendString("Item 1");
            TestList.AppendString("Item 2");
            TestList.AppendString("Item 3");

            Result = TestList.AsString();
            Assert.AreEqual("Item 1, Item 2, Item 3", Result);
        }

        [Test]
        public void GenerateIfEmptyTrue()
        {
            string Result = "";
            DelimitedList TestList = new DelimitedList(0, "{", "}", true);

            Result = TestList.AsString();
            Assert.AreEqual("{}", Result);
        }

        [Test]
        public void GenerateIfEmptyFalse ()
        {
            string Result = "";
            DelimitedList TestList = new DelimitedList(0, "{", "}", false);

            Result = TestList.AsString();
            Assert.AreEqual("", Result);
        }

        [Test]
        public void SingleSubList ()
        {
            string Result = "";
            DelimitedList TestList = new DelimitedList();

            DelimitedList TestList2 = TestList.AppendDelimitedList();
            TestList2.AppendString("Item 1");
            TestList2.AppendString("Item 2");
            TestList2.AppendString("Item 3");

            Result = TestList.AsString();
            Assert.AreEqual("Item 1, Item 2, Item 3", Result);
        }

        [Test]
        public void TwoSubLists ()
        {
            string Result = "";
            DelimitedList TestList = new DelimitedList();

            DelimitedList TestList2 = TestList.AppendDelimitedList();
            TestList2.AppendString("Item 1");
            TestList2.AppendString("Item 2");
            TestList2.AppendString("Item 3");

            DelimitedList TestList3 = TestList.AppendDelimitedList();
            TestList3.AppendString("Item 4");
            TestList3.AppendString("Item 5");

            Result = TestList.AsString();
            Assert.AreEqual("Item 1, Item 2, Item 3, Item 4, Item 5", Result);
        }

        [Test]
        public void String_List_String ()
        {
            string Result = "";
            DelimitedList TestList = new DelimitedList();

            TestList.AppendString("Item 1");
            DelimitedList TestList2 = TestList.AppendDelimitedList();

            TestList2.AppendString("Item 2");

            TestList.AppendString("Item 3");

            Result = TestList.AsString();
            Assert.AreEqual("Item 1, Item 2, Item 3", Result);
        }

        [Test]
        public void EmptyList_String ()
        {
            string Result = "";
            DelimitedList TestList = new DelimitedList();

            DelimitedList TestList2 = TestList.AppendDelimitedList();
            TestList.AppendString("Item 1");

            Result = TestList.AsString();
            Assert.AreEqual("Item 1", Result);
        }

        [Test]
        public void String_EmptyList ()
        {
            string Result = "";
            DelimitedList TestList = new DelimitedList();

            TestList.AppendString("Item 1");
            DelimitedList TestList2 = TestList.AppendDelimitedList();

            Result = TestList.AsString();
            Assert.AreEqual("Item 1", Result);
        }


        [Test]
        public void EmptyList ()
        {
            string Result = "";
            DelimitedList TestList = new DelimitedList();

            DelimitedList TestList2 = TestList.AppendDelimitedList();

            Result = TestList.AsString();
            Assert.AreEqual("", Result);
        }

        [Test]
        public void TwoEmptyLists ()
        {
            string Result = "";
            DelimitedList TestList = new DelimitedList();

            DelimitedList TestList2 = TestList.AppendDelimitedList();
            DelimitedList TestList3 = TestList.AppendDelimitedList();

            Result = TestList.AsString();
            Assert.AreEqual("", Result);
        }

        [Test]
        public void MultiLevelMonstrosity ()
        {
            string Result = "";
            DelimitedList TestList = new DelimitedList();

            TestList.AppendString("Item 1");
            DelimitedList TestList2 = TestList.AppendDelimitedList();

            TestList2.AppendString("Item 2");
            DelimitedList TestList3 = TestList2.AppendDelimitedList("{", "}");
            TestList3.AppendString("Item 3");
            DelimitedList TestList4 = TestList2.AppendDelimitedList();
            TestList4.AppendString("Item 4");
            TestList2.AppendString("Item 5");

            TestList2.AppendString("Item 6");

            Result = TestList.AsString();
            Assert.AreEqual("Item 1, Item 2, {Item 3}, Item 4, Item 5, Item 6", Result);
        }

    }
}
