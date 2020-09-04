using System;
using ex2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ex2.test
{
    [TestClass]
    public class UnitTest1
    {

       [TestMethod]
        // [DataRow(1, 1, 2, 3, 4, 5, 6, 7, 8, 9, 21, 3, 2, 5, 1, 7, 160)]
        // [DataRow(1, 2, 4, 3, 4, 5, 6, 7, 8, 9, 4, 3, 2, 5, 1, 7, 280)]
        public void MatrixDeterminantTest()
        {

            Matrix a = new Matrix(new double[] { 1, 1, 2, 3, 4, 5, 6, 7, 8, 9, 21, 3, 2, 5, 1, 7 });
            Matrix b = new Matrix(new double[] { 1, 2, 4, 3, 4, 5, 6, 7, 8, 9, 4, 3, 2, 5, 1, 7 });
            Assert.AreEqual(160, a.Determinant, 0.001);
            Assert.AreEqual(280, b.Determinant, 0.001);
        }
        
        [TestMethod]
        public void DiffMatrices()
        {
            Matrix a = new Matrix(new double[] { 1, 1, 2, 3, 4, 5, 6, 7, 8, 9, 21, 3, 2, 5, 1, 7 });
            Matrix b = new Matrix(new double[] { 1, 2, 4, 3, 4, 5, 6, 7, 8, 9, 4, 3, 2, 5, 1, 7 });
            Assert.AreEqual(false, a==b);
        }
        [TestMethod]
        public void InverseCheck()
        {
            Matrix a = new Matrix(new double[] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 });
            Matrix b = new Matrix(new double[] { 1, 1, 2, 3, 4, 5, 6, 7, 8, 9, 21, 3, 2, 5, 1, 7 });
            Matrix c = new Matrix(new double[] {-1.5, 1.5, -0.25, -0.75, -0.2375, -0.3625, 0.10625, 0.41875, 0.6, -0.4, 0.1, 0.1, 0.5125, -0.1125, -0.01875, 0.04375 });
            Assert.AreEqual(true,a == a.Inverse());
            Assert.AreEqual(c.GetHashCode(), b.Inverse().GetHashCode());

        }
        [TestMethod]
        public void AdditionCheck()
        {
            Matrix a = new Matrix(new double[] { 1, 1, 2, 3, 4, 5, 6, 7, 8, 9, 21, 3, 2, 5, 1, 7 });
            Matrix b = new Matrix(new double[] { 1, 2, 4, 3, 4, 5, 6, 7, 8, 9, 4, 3, 2, 5, 1, 7 });
            Matrix c = new Matrix(new double[] { 2, 3, 6, 6, 8, 10, 12, 14, 16,18,25,6,4,10,2,14});
            Assert.AreEqual(true,c == (b+a));
        }
    }
}
