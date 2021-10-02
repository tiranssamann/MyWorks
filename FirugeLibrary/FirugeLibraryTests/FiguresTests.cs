using Microsoft.VisualStudio.TestTools.UnitTesting;
using FirugeLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirugeLibrary.Tests
{
    [TestClass()]
    public class FiguresTests
    {
        [TestMethod()]
        public void AreaCircleTest()
        {
            double a = 2;
            double expected = 12.566370614359172;

            Assert.AreEqual(expected, Circle.AreaCircle(a));
        }
        [TestMethod()]
        public void AreaFigureTest()
        {
            double a = 2, b = 3;
            double expected = 6;
            Assert.AreEqual(expected, Figure.Area(a, b));
        }
        [TestMethod()]
        public void IsRightTriangleTest()
        {
            double a = 2, b = 3, c = 9;
            bool expected = false;
            Assert.AreEqual(expected, Triangle.IsRightTriangle(a, b,c));
        }
        [TestMethod()]
        public void AreaTriangleTest()
        {
            double a = 2, b = 3;
            double expected = 3;
            Assert.AreEqual(expected, Triangle.AreaTriangle(a, b));
        }
    }
}