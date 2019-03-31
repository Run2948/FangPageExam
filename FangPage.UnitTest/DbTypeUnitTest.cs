using System;
using FangPage.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FangPage.UnitTest
{
    [TestClass]
    public class DbTypeUnitTest
    {
        [TestMethod]
        public void TestConnStr()
        {

            string accessStr = "4l35xhbN+4tsut7S1U+BTHbIa1wPReCnzSj/n3ZFtHCyjdEiALglOcRZHUQy1jMmg+LAw0koq0M5vmm+WSu5zE+VU8swALZs";
            string accessConnStr = DES.Decode(accessStr);
            //Console.WriteLine(accessConnStr);

            // Access=datas/FP_Exam.config;UserId=;PassWord=;DbName=;Prefix=FP_

            string mssqlConnStr = "SqlServer=.;UserId=sa;PassWord=sa1994sa;DbName=FP_Exam;Prefix=FP_";
            string mssqlStr = DES.Encode(mssqlConnStr);
            Console.WriteLine(mssqlStr);

            // Jyy1AGOTMj0I4xmBkMkhocM3ymgxcxvlntp6KSxJt8MOIbrNSFhbOjCRwvQ5SbZwL26WKyCxNCbgLPe3jJl8KOqiAfsBKKdP

        }
    }
}
