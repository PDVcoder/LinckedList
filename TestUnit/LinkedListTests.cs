using System;
using Xunit;
using FluentAssertions;
using Xunit.Abstractions;
using Moq;

using LinkedList;



namespace TestUnit
{
    public class LinkedListTests
    {
        private LinkedList<int> list;
        ITestOutputHelper output;

        public LinkedListTests(ITestOutputHelper output)
        {
            list = new LinkedList<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            this.output = output;
        }

        [Fact]
        public void ConstructorArrayArgument()
        {
            //Arange
            int[] datas = { 1, 2, 3 };
            int expectedCount = 3;

            //Act
            LinkedList<int> test = new LinkedList<int>(datas);

            //Assert
            test.Should()
                .HaveCount(expectedCount).And
                .NotBeNull();
        }

        [Fact]
        public void ConstructorOneArgument()
        {
            //Assert
            int expectedData = 1;

            //Act
            LinkedList<int> test = new LinkedList<int>(1);
            int resultData = test[0];
            
            //Assert
            test.Should().HaveCount(1).And.NotBeNull();
            resultData.Should().Be(expectedData);
         }

        [Fact]
        public void Count_9result()
        {
            //Arange 
            int expected = 9;

            //Act
            int count = list.Count;

            //Assert
            count.Should().Be(expected);
            list.Should().HaveCount(expected);
        }

        [Fact]
        public void OperatorOfIndex()
        {
            //Arange
            int expectedFirst = 0;
            int expected4 = 4;
            int expectedLast = 8;

            //Act
            int resultFirst = list[0];
            int result4 = list[4];
            int resultLast = list[8];

            //Assert
            resultFirst.Should().Be(expectedFirst);
            result4.Should().Be(expected4);
            resultLast.Should().Be(expectedLast);
        }

        [Fact]
        public void OperatorOfIndexSet()
        {
            //Arange
            int expectedFirst = 10;
            int expected4 = 11;
            int expectedLast = 12;

            //Act
            list[0] = 10;
            list[4] = 11;
            list[8] = 12;

            //Assert
            list[0].Should().Be(expectedFirst);
            list[4].Should().Be(expected4);
            list[8].Should().Be(expectedLast);
        }

        [Fact]
        public void OperatorOfIndexOutOfRange()
        {
            //Arange
            //Act
            int x = 0;
            Action act = () => x = list[10];

            //Assert
            act.Should().Throw<IndexOutOfRangeException>();
        }

        [Fact]
        public void AddTest()
        {
            //Arange
            int item = 9;
            int expectedCount = 10;
            int expectedLastValue = 9;

            //Act
            list.Add(item);
            int resultCount = list.Count;
            int resultLastValue = list.Tail.Data;

            //Assert
            resultCount.Should().Be(expectedCount);
            resultLastValue.Should().Be(expectedLastValue);
        }

        [Fact]
        public void RemoveFirst()
        {
            //Arange
            int deletingFirst = 0;
            int expectedCount = 8;

            //Act
            bool result = list.Remove(deletingFirst);
            int resultFirst = list.Head.Data;
            int resultCount = list.Count;

            //Assert
            output.WriteLine("ResultFirst = " + resultFirst.ToString());
            result.Should().BeTrue();
            resultFirst.Should().NotBe(null).And.NotBe(deletingFirst);
            resultCount.Should().Be(expectedCount);
        }

        [Fact]
        public void RemoveInside()
        {
            //Arange
            int deletingInside = 4;
            int expectedCount = 8;

            //Act
            bool result = list.Remove(deletingInside);
            int resultInside = list[4];
            int resultCount = list.Count;

            //Assert
            output.WriteLine("Result list[4] = " + resultInside.ToString());
            result.Should().BeTrue();
            resultInside.Should().NotBe(null).And.NotBe(deletingInside);
            resultCount.Should().Be(expectedCount);
        }

        [Fact]
        public void RemoveLast()
        {
            //Arange
            int deletingLast = 8;
            int expectedCount = 8;

            //Act
            bool result = list.Remove(deletingLast);
            int resultLast = list.Tail.Data;
            int resultCount = list.Count;

            //Assert
            output.WriteLine("ResultLast = " + resultLast.ToString());
            resultLast.Should().NotBe(null).And.NotBe(deletingLast);
            resultCount.Should().Be(expectedCount); 
        }

        [Fact]
        public void RemoveNotExisting()
        {
            //Arange
            int value = 9;

            //Act
            bool result = list.Remove(value);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsReadOnlyTrue()
        {
            //Arrange
            list.IsReadOnly = true;

            //Act
            Action Adding = () => list.Add(1);
            Action Removing = () => list.Remove(1);
            Action Clearing = () => list.Clear();
            Action Reversing = () => list.Reverse();
            Action SetByIndex = () => list[1] = 1;

            //Assert
            Adding.Should().Throw<NotSupportedException>();
            Removing.Should().Throw<NotSupportedException>();
            Clearing.Should().Throw<NotSupportedException>();
            Reversing.Should().Throw<NotSupportedException>();
            SetByIndex.Should().Throw<NotSupportedException>();
        }

        [Fact]
        public void IsReadOnlyFalse()
        {
            //Arrange
            list.IsReadOnly = false;

            //Act
            Action Adding = () => list.Add(1);
            Action Removing = () => list.Remove(1);
            Action Clearing = () => list.Clear();
            Action Reversing = () => list.Reverse();
            Action SetByIndex = () => list[1] = 1;

            //Assert
            Adding.Should().NotThrow<NotSupportedException>();
            Removing.Should().NotThrow<NotSupportedException>();
            Clearing.Should().NotThrow<NotSupportedException>();
            Reversing.Should().NotThrow<NotSupportedException>();
            SetByIndex.Should().NotThrow<NotSupportedException>();
        }

        [Fact]
        public void TestContains()
        {
            //Arange
            int containsValue = 4;
            int notContainsValue = 9;

            //Act
            bool resultContains = list.Contains(containsValue);
            bool resultNotContains = list.Contains(notContainsValue);

            //Assert
            resultContains.Should().BeTrue();
            resultNotContains.Should().BeFalse();
        }

        [Fact]
        public void TestCopyTo()
        {
            //Arrange
            int[] arr = new int[9];
            int[] expectedArray = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            int expectedCount = expectedArray.Length;

            //Act
            list.CopyTo(arr, 0);

            //Assert
            arr.Should().BeEquivalentTo(expectedArray).And.HaveCount(expectedCount);
        }

        [Fact]
        public void ReverseTest()
        {
            //Arange
            int expextedLength = list.Count;
            int expectedTail = 0;
            int expectedHead = 8;

            //Act
            LinkedList<int> resulList =  list.Reverse();
            int head = resulList.Head.Data;
            int tail = resulList.Tail.Data;

            //Assert
            head.Should().Be(expectedHead);
            tail.Should().Be(expectedTail);
            resulList.Should().HaveCount(expextedLength).And.NotBeNullOrEmpty();
        }

        [Fact]
        public void ClearTest()
        {
            //Arange
            int expectedCount = 0;

            //Act
            list.Clear();
            var resultHead = list.Head;
            var resultTail = list.Tail;

            //Assert
            list.Should().HaveCount(expectedCount);
            resultHead.Should().BeNull();
            resultTail.Should().BeNull();
        } 

        [Fact]
        public void IEnumeratorTest()
        {
            //Arange
            int[] expectedArray = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            var i = 0;
            
            //Act

            //Assert
            foreach(Item<int> item in list)
            {
                item.Data.Should().Be(expectedArray[i]) ;
                i++;
            }
        }

        [Fact]
        public void OnClearEvent()
        {
            //Arrange
            var mock = new Mock<ITestEvent<int>>();
            list.OnClear += mock.Object.TestMethod;
          
            //Act
            list.Clear();

            //Assert
            mock.Verify(m => m.TestMethod((object)list, null));
        }

        [Fact]
        public void OnAddEvent()
        {
            //Arrange
            var mock = new Mock<ITestEvent<int>>();
            int expectedItem = 5;
            list.OnAdd += mock.Object.TestMethod;

            //Act
            list.Add(expectedItem);

            //Assert
            mock.Verify(m => m.TestMethod((object)list, expectedItem));
        }

        [Fact]
        public void OnRemoveEvent()
        {
            //Arrange
            var mock = new Mock<ITestEvent<int>>();
            int expectedItem = 5;
            list.OnRemove += mock.Object.TestMethod;

            //Act
            list.Remove(expectedItem);

            //Assert
            mock.Verify(m => m.TestMethod((object)list, expectedItem));
        }

        [Fact]
        public void Item()
        {
            //Arrang
            Item<int> item;

            //Act
            item = new Item<int>();

            //Assert
            item.Previous.Should().BeNull();
            item.Next.Should().BeNull();
            item.Data.Should().Be(default(int));
        }

        [Fact]
        public void ItemToString()
        {
            //Arange
            string expctedhead = "1";
            Item<int> item = new Item<int>(1);

            //Act
            string head = item.ToString();

            //Assert
            head.Should().BeEquivalentTo(expctedhead);
        }

        [Fact]
        public void ItemGetHashCode()
        {
            //Arange
            int expectedHash = 0;

            //Act
            int hash = list.Head.GetHashCode();

            //Assert
            hash.Should().Be(expectedHash);
        }

        

        //TODO rewrite
        [Fact]
        public void TestHeadTail()
        {
            LinkedList<int> list = new LinkedList<int>();

            for (int i = 1; i <= 10; i++)
            {
                list.Add(i);
            }

            Assert.NotNull(list.Head);
            Assert.NotNull(list.Tail);
            Assert.Equal(1, list.Head.Data);
            Assert.Equal(10, list.Tail.Data);
        }


    }
}
