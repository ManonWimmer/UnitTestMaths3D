﻿// ----- MANON WIMMER ----- //
using UnitTestMaths3DWimmer;
// ReSharper disable All

namespace Maths_Matrices.Tests
{
    [TestFixture]
    
    public class Tests11_InvertMatricesUsingRowReduction
    {
        [Test, DefaultFloatingPointTolerance(0.001f)] 
        public void TestInvertMatrixInstance()
        {
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 2f, 3f, 8f },
                { 6f, 0f, -3f },
                { -1f, 3f, 2f },
            });

            MatrixFloat mInverted = m.InvertByRowReduction();
            
            Assert.That(mInverted.ToArray2D(), Is.EqualTo(new[,]
            {
                { 0.066f, 0.133f, -0.066f },
                { -0.066f, 0.088f, 0.4f },
                { 0.133f, -0.066f, -0.133f }
            }));
        }
        
        [Test, DefaultFloatingPointTolerance(0.001f)] 
        public void TestInvertMatrixStatic()
        {
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 1f, 2f },
                { 3f, 4f },
            });

            MatrixFloat mInverted = MatrixFloat.InvertByRowReduction(m);
            
            Assert.That(mInverted.ToArray2D(), Is.EqualTo(new[,]
            {
                { -2f, 1f },
                { 1.5f, -0.5f },
            }));
        }
        
        [Test]
        public void TestInvertImpossibleMatrix()
        {
            MatrixFloat m = new MatrixFloat(new[,]
            {
                { 1f, 2f, 3f },
                { 4f, 5f, 6f },
                { 7f, 8f, 9f },
            });

            Assert.Throws<MatrixInvertException>(() =>
            {
                MatrixFloat mInverted = m.InvertByRowReduction();
            });
        }
    }
}