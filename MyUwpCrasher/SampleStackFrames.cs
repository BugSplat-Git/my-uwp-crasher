using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUwpCrasher
{
    public class Foo
    {
        private Bar _bar;

        public Foo(Bar bar)
        {
            _bar = bar;
        }

        public void Crash()
        {
            _bar.Crash();
        }
    }

    public class Bar
    {
        private Baz _baz;

        public Bar(Baz baz)
        {
            _baz = baz;
        }

        public void Crash()
        {
            _baz.Crash();
        }
    }

    public class Baz
    {
        public void Crash()
        {
            throw new Exception("BugSplat!");
        }
    }
}
