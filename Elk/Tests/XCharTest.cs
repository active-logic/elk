//using UnityEngine;
using System.Linq;
using NUnit.Framework;
using Elk.Util;

namespace Elk_Test{
public class XCharTest{

    [Test] public void TestParseXCharArray(){
        var str = "ab\ncd";
        var z = xchar.ToXChar(str).ToArray();
        Assert.That( z[0] == new xchar('a' , 1, 1) );
        Assert.That( z[1] == new xchar('b' , 1, 2) );
        Assert.That( z[2] == new xchar('\n', 1, 3) );
        Assert.That( z[3] == new xchar('c' , 2, 1) );
        Assert.That( z[4] == new xchar('d' , 2, 2) );
    }

}}
