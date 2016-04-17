﻿namespace BlockchainSharp.Tests.Compilers
{
    using System;
    using BlockchainSharp.Compilers;
    using BlockchainSharp.Vm;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SimpleCompilerTests
    {
        [TestMethod]
        public void CompileEmptyString()
        {
            var compiler = new SimpleCompiler(string.Empty);

            var result = compiler.Compile();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void CompileNullString()
        {
            var compiler = new SimpleCompiler(null);

            var result = compiler.Compile();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void CompileArithmeticOperations()
        {
            CompileBytecode("add", Bytecodes.Add);
            CompileBytecode("subtract", Bytecodes.Subtract);
            CompileBytecode("multiply", Bytecodes.Multiply);
            CompileBytecode("divide", Bytecodes.Divide);
        }

        [TestMethod]
        public void CompileDup()
        {
            CompileBytecode("dup 1", Bytecodes.Dup1);
            CompileBytecode("dup 2", Bytecodes.Dup2);
            CompileBytecode("dup 3", Bytecodes.Dup3);
            CompileBytecode("dup 4", Bytecodes.Dup4);
            CompileBytecode("dup 5", Bytecodes.Dup5);
            CompileBytecode("dup 6", Bytecodes.Dup6);
            CompileBytecode("dup 7", Bytecodes.Dup7);
            CompileBytecode("dup 8", Bytecodes.Dup8);
            CompileBytecode("dup 9", Bytecodes.Dup9);
            CompileBytecode("dup 10", Bytecodes.Dup10);
            CompileBytecode("dup 11", Bytecodes.Dup11);
            CompileBytecode("dup 12", Bytecodes.Dup12);
            CompileBytecode("dup 13", Bytecodes.Dup13);
            CompileBytecode("dup 14", Bytecodes.Dup14);
            CompileBytecode("dup 15", Bytecodes.Dup15);
            CompileBytecode("dup 16", Bytecodes.Dup16);
        }

        [TestMethod]
        public void CompilePush()
        {
            CompileBytecode("push 0", Bytecodes.Push1, 0);
            CompileBytecode("push 1", Bytecodes.Push1, 1);
            CompileBytecode("push 256", Bytecodes.Push2, 1, 0);
        }

        [TestMethod]
        public void CompileSkippingCommentsAndEmptyLines()
        {
            CompileBytecode("add\n", Bytecodes.Add);
            CompileBytecode("add\r\n", Bytecodes.Add);
            CompileBytecode("add\r", Bytecodes.Add);
            CompileBytecode("# comment\nadd\n", Bytecodes.Add);
            CompileBytecode("\n\nadd # comment\n", Bytecodes.Add);
            CompileBytecode("\r\n\r\nadd # comment\n", Bytecodes.Add);
            CompileBytecode("\r\radd # comment\n", Bytecodes.Add);
        }

        private static void CompileBytecode(string text, Bytecodes bc)
        {
            var compiler = new SimpleCompiler(text);
            var result = compiler.Compile();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual((byte)bc, result[0]);
        }

        private static void CompileBytecode(string text, Bytecodes bc, params byte[] bytes)
        {
            var compiler = new SimpleCompiler(text);
            var result = compiler.Compile();

            Assert.IsNotNull(result);
            Assert.AreEqual(1 + bytes.Length, result.Length);
            Assert.AreEqual((byte)bc, result[0]);

            for (int k = 0; k < bytes.Length; k++)
                Assert.AreEqual(bytes[k], result[k + 1]);
        }
    }
}