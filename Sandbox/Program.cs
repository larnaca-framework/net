using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using LCA.Blueprints;
using LCA.Schematics;

namespace Sandbox
{
    public class Blah
    {
        public class Boo
        {

        }
    }
    public class Foo<T>
    {
        public class Bar
        {

        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            var refStr = Describer.Default.GetRef<string>();
            var refInt = Describer.Default.GetRef(10);
            var refList = Describer.Default.GetRef(typeof(List<>));
            var refListString = Describer.Default.GetRef<List<string>>();
            var refDic = Describer.Default.GetRef(typeof(Dictionary<,>));
            var complex = Describer.Default.GetRef(typeof(string[,][][,,][,]));
            var refDicString = Describer.Default.GetRef<Dictionary<string, string[,][][,,][,]>>();
            var docLastParam = refDicString.GenericArguments.Last();
            var ultimate = Describer.Default.GetRef<Foo<Blah.Boo>>();
            var a = "breakpoint";

        }
    }
}
