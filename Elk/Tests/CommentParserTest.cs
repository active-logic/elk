//using System;
using UnityEngine;
using NUnit.Framework;
using Elk.Basic;
using Elk.Util;

namespace Elk_Test{
public class CommentParserTest{

    [Test] public void TestParseComments(){
        var parser = new CommentParser();
        var @out = parser.Parse("Foo; # bar\n123");
        Assert.That(@out.Format(), Is.EqualTo("Foo; \n123"));
    }

}}
