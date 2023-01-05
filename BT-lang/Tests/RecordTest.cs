using NUnit.Framework;
using UnityEngine;
using Elk.Memory;

namespace RecordTest{
public class RecordTest{

    Record record;

    [Test] public void TestAdd(){
        var f = new Frame(("dog", "Chase", "cat"), 0);
        record.Append(f);
        Assert.That(record.count == 1);
    }

    [Test] public void TestAddTwice(){
        var f = new Frame(("dog", "Chase", "cat"), 0);
        record.Append(f);
        record.Append(f);
        Assert.That(record.count == 1);
    }

    [SetUp] public void Setup()
    => record = new Record("anon");

}}
