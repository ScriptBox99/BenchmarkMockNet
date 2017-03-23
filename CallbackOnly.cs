﻿using System;
using BenchmarkDotNet.Attributes;
using Moq;
using NSubstitute;

namespace BenchmarkMockNet
{
    public class CallbackOnly : IMockingBenchmark
    {
        private readonly IThingy stub;
        private readonly Mock<IThingy> mock;
        private readonly IThingy sub;

        public CallbackOnly()
        {
            stub = new ThingStub();

            mock = new Mock<IThingy>();
            mock.Setup(m => m.DoSomething()).Callback(() => mock.Object.Called = true);

            sub = Substitute.For<IThingy>();
            sub.When(s => s.DoSomething()).Do(c => sub.Called = true);
        }

        [Benchmark(Baseline = true)]
        public void Stub() => stub.DoSomething();

        [Benchmark]
        public void Moq() => mock.Object.DoSomething();

        [Benchmark]
        public void NSubstitute() => sub.DoSomething();

        public void FakeItEasy() => throw new NotImplementedException("Never completes, probably a memory leak");
    }
}