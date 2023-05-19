using System;
using System.Runtime.InteropServices;

namespace SharpEdif.User
{
    public unsafe class Extension
    {
        public const string ExtensionName = "My new C# extension";
        public const string ExtensionAuthor = "The extension's author";
        public const string ExtensionCopyright = "Copyright © 2023 Extension Author";
        public const string ExtensionComment = "Quick description displayed in the Insert Object dialog box and in the object's About properties.";
        public const string ExtensionHttp = "http://www.authorswebpage.com";

        [Action("Set string", "Set string to %0")]
        public static void ActionExample1(LPRDATA* rdPtr, string exampleString)
        {
            rdPtr->runData.ExampleString = exampleString;
        }
        [Action(
            "Log to console", "Log %0")]
        public static void ActionExample2(LPRDATA* rdPtr, string exampleString)
        {
            Console.WriteLine(exampleString);
        }
        [Condition("String is equal to", "String is equal to %0")]
        public static bool ConditionExample1(LPRDATA* rdPtr,string testString)
        {
            return rdPtr->runData.ExampleString == testString;
        }

        [Expression("Get string", "GetStr$(")]
        public static string ExpressionExample1(LPRDATA* rdPtr)
        {
            return rdPtr->runData.ExampleString;
        }
    }
}