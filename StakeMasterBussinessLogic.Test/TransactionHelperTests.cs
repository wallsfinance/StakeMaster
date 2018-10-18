﻿namespace StakeMasterBussinessLogic.Test
{
	using System.Diagnostics.CodeAnalysis;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using StakeMaster.BusinessLogic;

	[TestClass]
	[TestCategory("Unit")]
	[ExcludeFromCodeCoverage]
	public sealed class TransactionHelperTests
	{
		[TestMethod]
		public void Constructor_SetFreeByteLimit()
		{
			//Arrange
			const int expectedFree = 985;
			//Act
			var result = new TransactionHelper(1, 1, 1, 985);
			//Assert
			Assert.AreEqual(expectedFree, result.FreeTransactionByteLimit);
		}

		[TestMethod]
		public void Constructor_SetInputSize()
		{
			//Arrange
			const int expectedInputs = 167;
			//Act
			var result = new TransactionHelper(167, 1, 1, 1);
			//Assert
			Assert.AreEqual(expectedInputs, result.InputSize);
		}

		[TestMethod]
		public void Constructor_SetOutputSize()
		{
			//Arrange
			const int expectedOutputs = 289;
			//Act
			var result = new TransactionHelper(1, 289, 1, 1);
			//Assert
			Assert.AreEqual(expectedOutputs, result.OutputSize);
		}

		[TestMethod]
		public void Constructor_SetOverheadSize()
		{
			//Arrange
			const int expectedOverhead = 581;
			//Act
			var result = new TransactionHelper(1, 1, 581, 1);
			//Assert
			Assert.AreEqual(expectedOverhead, result.TransactionOverhead);
		}

		[DataTestMethod]
		[DataRow(23, 3, 62, 200, 3, 5)]
		[DataRow(17, 12, 100, 1200, 20, 50)]
		[DataRow(10, 11, 12, 77, 5, 1)]
		public void GetMaxPossibleInputCountForFreeTransaction_Default(int inputSize, int outputSize, int overhead, int byteLimit, int outputs, int expectedResult)
		{
			//Arrange
			var test = new TransactionHelper(inputSize, outputSize, overhead, byteLimit);
			//Act
			int result = test.GetMaxPossibleInputCountForFreeTransaction(outputs);
			//Assert
			Assert.AreEqual(expectedResult, result);
		}

		[TestMethod]
		[ExpectedException(typeof(FreeTransactionNotPossibleException))]
		public void GetMaxPossibleInputCountForFreeTransaction_NotPossibleException()
		{
			//Arrange
			var test = new TransactionHelper(10, 11, 12, 76);
			const int outputs = 5;
			//Act
			test.GetMaxPossibleInputCountForFreeTransaction(outputs);
			//Assert
			//[ExpectedException(typeof(FreeTransactionNotPossibleException))]
		}

		[DataTestMethod]
		[DataRow(23, 3, 62, 200, 3, 23)]
		[DataRow(17, 12, 100, 1200, 20, 63)]
		[DataRow(10, 11, 12, 73, 5, 1)]
		public void GetMaxPossibleOutputCountForFreeTransaction_Default(int inputSize, int outputSize, int overhead, int byteLimit, int inputs, int expectedResult)
		{
			//Arrange
			var test = new TransactionHelper(inputSize, outputSize, overhead, byteLimit);
			//Act
			int result = test.GetMaxPossibleOutputCountForFreeTransaction(inputs);
			//Assert
			Assert.AreEqual(expectedResult, result);
		}

		[TestMethod]
		[ExpectedException(typeof(FreeTransactionNotPossibleException))]
		public void GetMaxPossibleOutputCountForFreeTransaction_NotPossibleException()
		{
			//Arrange5
			var test = new TransactionHelper(10, 11, 12, 72);
			const int inputs = 5;
			//Act
			test.GetMaxPossibleOutputCountForFreeTransaction(inputs);
			//Assert
			//[ExpectedException(typeof(FreeTransactionNotPossibleException))]
		}

		[TestMethod]
		public void GetTransactionSize_Default()
		{
			//Arrange
			var test = new TransactionHelper(12, 3, 50, 200);
			const int inputs = 5;
			const int outputs = 7;
			const int expectedSize = 131;
			//Act
			int result = test.GetTransactionSize(inputs, outputs);
			//Assert
			Assert.AreEqual(expectedSize, result);
		}
	}
}